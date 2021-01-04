/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-21 Lisias T : http://lisias.net <support@lisias.net>

	KSPe API Extensions/L is double licensed, as follows:

	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe API Extensions/L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe API Extensions/L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSPe API Extensions/L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using System.Diagnostics;

namespace KSPe.Util.Log {

	public enum Level
	{
		OFF = 0,
		ERROR = 1,
		WARNING = 2,
		INFO = 3,
		DETAIL = 4,
		TRACE = 5
	};

	public abstract class Logger
	{
		public static Logger CreateForType<T>(bool useClassNameToo = false)
		{
			if (Globals<T>.Log.ThreadSafe)
				return useClassNameToo
						? new UnityThreadSafeLogger(typeof(T), typeof(T).Namespace, typeof(T).FullName)
						: new UnityThreadSafeLogger(typeof(T), typeof(T).Namespace)
					;
			return CreateThreadUnsafeForType<T>(useClassNameToo);
		}

		public static Logger CreateThreadUnsafeForType<T>(bool useClassNameToo = false)
		{
			return useClassNameToo
					? new UnityLogger(typeof(T), typeof(T).Namespace, typeof(T).FullName)
					: new UnityLogger(typeof(T), typeof(T).Namespace)
				;
		}

		public static Logger CreateForType<T>(string forceThisNamespace, bool useClassNameToo = true)
		{
			if (Globals<T>.Log.ThreadSafe)
				return useClassNameToo
						? new UnityThreadSafeLogger(typeof(T), forceThisNamespace, typeof(T).FullName)
						: new UnityThreadSafeLogger(typeof(T), forceThisNamespace)
					;
			return CreateThreadUnsafeForType<T>(forceThisNamespace, useClassNameToo);
		}

		public static Logger CreateThreadUnsafeForType<T>(string forceThisNamespace, bool useClassNameToo = false)
		{
			return useClassNameToo
					? new UnityLogger(typeof(T), forceThisNamespace, typeof(T).FullName)
					: new UnityLogger(typeof(T), forceThisNamespace)
				;
		}

		public static Logger CreateForType<T>(string forceThisNamespace, string forceThisClassName)
		{
			if (Globals<T>.Log.ThreadSafe)
				return new UnityThreadSafeLogger(typeof(T), forceThisNamespace, forceThisClassName);
			return CreateThreadUnsafeForType<T>(forceThisNamespace, forceThisClassName);
		}

		public static Logger CreateThreadUnsafeForType<T>(string forceThisNamespace, string forceThisClassName)
		{
			return new UnityLogger(typeof(T), forceThisNamespace, forceThisClassName);
		}

		protected delegate void LogMethod(string message);
		protected Level _level = Level.DETAIL;

		public Level level
		{
			get => this._level;
			set
			{
				if (this._level != value)
					this.force("Log is set to level {0}.", value);
				this._level = value;
			}
		}
		public bool IsLoggable(Level level)
		{
			return level <= this._level;
		}

		internal readonly Type type;
		internal readonly String nameSpace;
		private readonly String prefix;

		protected Logger(Type type)
		{
			this.type = type;
			this.nameSpace = type.Namespace;
			this.prefix = string.Format("[{0}]", this.nameSpace);
		}

		protected Logger(Type type, string forceThisNamespace)
		{
			this.type = type;
			this.nameSpace = forceThisNamespace;
			this.prefix = string.Format("[{0}]", forceThisNamespace);
			this._level = Globals.Get(this.type).Log.Level;
		}

		protected Logger(Type type, string forceThisNamespace, string forceThisClassName)
		{
			this.type = type;
			this.nameSpace = forceThisNamespace;
			this.prefix = string.Format("[{0}-{1}]", forceThisNamespace, forceThisClassName);
			this._level = Globals.Get(this.type).Log.Level;
		}
		
		protected abstract LogMethod select();
		protected abstract void log(string message);
		protected abstract void logException(string message, Exception e);

		public void force(string message, params object[] @params)
		{
			this.select()(this.BuildMessage(message, @params));
		}

		public void trace(string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.TRACE)) return;
			this.select()(this.BuildMessage(Level.TRACE, message, @params));
		}

		public void detail(string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.DETAIL)) return;
			this.select()(this.BuildMessage(Level.DETAIL, message, @params));
		}

		public void dbg(string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.DETAIL)) return;
			this.select()(this.BuildMessage(Level.TRACE, message, @params));
		}

		public void info(string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.INFO)) return;
			this.select()(this.BuildMessage(Level.INFO, message, @params));
		}

		public void warn(string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.WARNING)) return;
			this.select()(this.BuildMessage(Level.WARNING, message, @params));
		}

		public void warn(Exception e, string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.WARNING)) return;
			this.logException(this.BuildMessage(Level.WARNING, message, @params),e);
		}

		public void error(string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.ERROR)) return;

			StackTrace stacktrace = new StackTrace();
			String caller = stacktrace.GetFrame(1).GetMethod().Name;
			int line = stacktrace.GetFrame(1).GetFileLineNumber();
			message = string.Format("{0} at {1}:{2}", message, caller, line);
			this.select()(this.BuildMessage(Level.ERROR, message, @params));
		}

		public void error(Exception e, string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.ERROR)) return;

			StackTrace stacktrace = new StackTrace();
			String caller = stacktrace.GetFrame(1).GetMethod().Name;
			int line = stacktrace.GetFrame(1).GetFileLineNumber();
			message = string.Format("{0} at {1}:{2}", message, caller, line);
			this.logException(this.BuildMessage(Level.ERROR, message, @params), e);
		}

		public void error(System.Object offended, System.Exception e)
		{
			if (!this.IsLoggable(Level.ERROR)) return;

			StackTrace stacktrace = new StackTrace();
			String caller = stacktrace.GetFrame(1).GetMethod().Name;
			int line = stacktrace.GetFrame(1).GetFileLineNumber();
			this.logException(this.BuildMessage(Level.ERROR, "{0} raised Exception {1} at {2}:{3}", offended.GetType().FullName, e.ToString(), caller, line), e);
		}

		protected string BuildMessage(string message, params object[] @params)
		{
			return string.Format("{0} {1}", this.prefix, this.FormatMessage(message, @params));
		}

		protected string BuildMessage(Level level, string message, params object[] @params)
		{
			return string.Format("{0} {1}: {2}", this.prefix, level, this.FormatMessage(message, @params));
		}

		private string FormatMessage(string message, params object[] @params)
		{
			return ((@params != null) && (@params.Length > 0)) ? string.Format(message, @params) : message;
		}
	}
}
