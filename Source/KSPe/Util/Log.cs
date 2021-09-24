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

namespace KSPe.Util.Log
{

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
		private const string NO_NAMESPACE = "<no namespace>";

		public static Logger CreateForType<T>()
		{
			return (Globals<T>.Log.ThreadSafe)
					? CreateThreadSafeForType<T>(false, 0)
					: CreateThreadUnsafeForType<T>(false, 0)
				;
		}

		public static Logger CreateForType<T>(bool useClassNameToo)
		{
			return (Globals<T>.Log.ThreadSafe)
					? CreateThreadSafeForType<T>(useClassNameToo, 0)
					: CreateThreadUnsafeForType<T>(useClassNameToo, 0)
				;
		}

		public static Logger CreateForType<T>(int skipStackLevels)
		{
			return (Globals<T>.Log.ThreadSafe)
					? CreateThreadSafeForType<T>(false, skipStackLevels)
					: CreateThreadUnsafeForType<T>(false, skipStackLevels)
				;
		}

		public static Logger CreateForType<T>(bool useClassNameToo, int skipStackLevels)
		{
			return (Globals<T>.Log.ThreadSafe)
					? CreateThreadSafeForType<T>(useClassNameToo, skipStackLevels)
					: CreateThreadUnsafeForType<T>(useClassNameToo, skipStackLevels)
				;
		}

		public static Logger CreateThreadSafeForType<T>(bool useClassNameToo = false, int skipStackLevels = 0)
		{
			return useClassNameToo
					? new UnityLogger(typeof(T), typeof(T).Namespace, typeof(T).FullName, skipStackLevels, UnityUiThreadSafeLogDecorator.INSTANCE)
					: new UnityLogger(typeof(T), typeof(T).Namespace, skipStackLevels, UnityUiThreadSafeLogDecorator.INSTANCE)
				;
		}

		public static Logger CreateThreadUnsafeForType<T>(bool useClassNameToo = false, int skipStackLevels = 0)
		{
			return useClassNameToo
					? new UnityLogger(typeof(T), typeof(T).Namespace, typeof(T).FullName, skipStackLevels)
					: new UnityLogger(typeof(T), typeof(T).Namespace, skipStackLevels)
				;
		}

		public static Logger CreateForType<T>(string forceThisNamespace, bool useClassNameToo = false, int skipStackLevels = 0)
		{
			return (Globals<T>.Log.ThreadSafe)
					? CreateThreadSafeForType<T>(forceThisNamespace, useClassNameToo, skipStackLevels)
					: CreateThreadUnsafeForType<T>(forceThisNamespace, useClassNameToo, skipStackLevels)
				;
		}

		public static Logger CreateThreadSafeForType<T>(string forceThisNamespace, bool useClassNameToo = false, int skipStackLevels = 0)
		{
			return useClassNameToo
					? new UnityLogger(typeof(T), forceThisNamespace, typeof(T).FullName, skipStackLevels, UnityUiThreadSafeLogDecorator.INSTANCE)
					: new UnityLogger(typeof(T), forceThisNamespace, skipStackLevels, UnityUiThreadSafeLogDecorator.INSTANCE)
				;
		}

		public static Logger CreateThreadUnsafeForType<T>(string forceThisNamespace, bool useClassNameToo = false, int skipStackLevels = 0)
		{
			return useClassNameToo
					? new UnityLogger(typeof(T), forceThisNamespace, typeof(T).FullName, skipStackLevels)
					: new UnityLogger(typeof(T), forceThisNamespace, skipStackLevels)
				;
		}

		public static Logger CreateForType<T>(string forceThisNamespace, string forceThisClassName, int skipStackLevels = 0)
		{
			return (Globals<T>.Log.ThreadSafe)
					? CreateThreadSafeForType<T>(forceThisNamespace, forceThisClassName, skipStackLevels)
					: CreateThreadUnsafeForType<T>(forceThisNamespace, forceThisClassName, skipStackLevels)
				;
		}

		public static Logger CreateThreadSafeForType<T>(string forceThisNamespace, string forceThisClassName, int skipStackLevels = 0)
		{
			return new UnityLogger(typeof(T), forceThisNamespace, forceThisClassName, skipStackLevels, UnityUiThreadSafeLogDecorator.INSTANCE);
		}

		public static Logger CreateThreadUnsafeForType<T>(string forceThisNamespace, string forceThisClassName, int skipStackLevels = 0)
		{
			return new UnityLogger(typeof(T), forceThisNamespace, forceThisClassName, skipStackLevels);
		}

		protected delegate void LogMethod(string message);
		protected Level _level = Level.DETAIL;

		public Level level
		{
			get => this._level;
			set
			{
				if (this._level == value) return;
				this.force("Log is set to level {0}.", value);
				this._level = value;
			}
		}
		public bool IsLoggable(Level level)
		{
			return level <= this._level;
		}

		internal readonly Type type;
		internal readonly string nameSpace;
		private readonly string prefix;
		protected readonly int skipLevels;

		protected Logger(Type type, int skipLevels)
		{
			this.type = type;
			this.nameSpace = type.Namespace ?? NO_NAMESPACE;
			this.prefix = string.Format("[{0}]", this.nameSpace);
			this._level = Globals.Get(this.type).Log.Level;
			this.skipLevels = skipLevels;
		}

		protected Logger(Type type, string forceThisNamespace, int skipLevels)
		{
			this.type = type;
			this.nameSpace = forceThisNamespace ?? NO_NAMESPACE;
			this.prefix = string.Format("[{0}]", this.nameSpace);
			this._level = Globals.Get(this.type).Log.Level;
			this.skipLevels = skipLevels;
		}

		protected Logger(Type type, string forceThisNamespace, string forceThisClassName, int skipLevels)
		{
			this.type = type;
			this.nameSpace = forceThisNamespace ?? NO_NAMESPACE;
			this.prefix = string.Format("[{0}.{1}]", this.nameSpace, forceThisClassName);
			this._level = Globals.Get(this.type).Log.Level;
			this.skipLevels = skipLevels;
		}

		public virtual void Close() { }

		protected abstract LogMethod select();
		protected abstract void log(string message);
		protected abstract void logException(string message, Exception e);

		public void force(string message, params object[] @params)
		{
			this.select()(this.BuildMessage(message, @params));
		}

		public void stack(System.Object offended, bool forced = false)
		{
			if (!(forced || this.IsLoggable(Level.TRACE))) return;
			StackTrace stacktrace = new StackTrace();
			string message = stacktrace.ToString(); // TODO: Remove this method entry on the stackdump. Respect this.skipLevels
			this.select()(this.BuildMessage(Level.TRACE, message));
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

		[Conditional("DEBUG")]
		internal void debug(string message, params object[] @params)
		{
			this.dbg(message, @params);
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
			this.logException(this.BuildMessage(Level.WARNING, message, @params), e);
		}

		public void error(string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.ERROR)) return;

			this.ParseStack(out string caller, out int line);
			message = string.Format("{0} at {1}:{2}", message, caller, line);
			this.select()(this.BuildMessage(Level.ERROR, message, @params));
		}

		public void error(Exception e, string message, params object[] @params)
		{
			if (!this.IsLoggable(Level.ERROR)) return;

			this.ParseStack(out string caller, out int line);
			message = string.Format("{0} at {1}:{2}", message, caller, line);
			this.logException(this.BuildMessage(Level.ERROR, message, @params), e);
		}

		public void error(System.Object offended, System.Exception e)
		{
			if (!this.IsLoggable(Level.ERROR)) return;

			this.ParseStack(out string caller, out int line);
			this.logException(this.BuildMessage(Level.ERROR, "{0} raised Exception {1} at {2}:{3}", offended.GetType().FullName, e.ToString(), caller, line), e);
		}

		public void fatal(string message, params object[] @params)
		{
			this.ParseStack(out string caller, out int line);
			message = string.Format("{0} at {1}:{2}", message, caller, line);
			this.select()(this.BuildMessage(Level.ERROR, message, @params));
		}

		public void fatal(Exception e, string message, params object[] @params)
		{
			this.ParseStack(out string caller, out int line);
			message = string.Format("{0} at {1}:{2}", message, caller, line);
			this.logException(this.BuildMessage(Level.ERROR, message, @params), e);
		}

		public void fatal(System.Object offended, System.Exception e)
		{
			this.ParseStack(out string caller, out int line);
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

		private void ParseStack(out string caller, out int line)
		{
			StackTrace stacktrace = new StackTrace();
			int stackLevel = 2 + this.skipLevels;
			caller = stacktrace.GetFrame(stackLevel).GetMethod().Name;
			line = stacktrace.GetFrame(stackLevel).GetFileLineNumber();
		}
	}
}
