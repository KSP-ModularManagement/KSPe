/*
    This file is part of KSPe, a component for KSP API Extensions/L
    (C) 2018 Lisias T : http://lisias.net <support@lisias.net>

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
using UnityEngine;
using System.Runtime.CompilerServices;

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
			return useClassNameToo
					? new UnityLogger(typeof(T).Namespace, typeof(T).FullName)
					: new UnityLogger(typeof(T).Namespace)
					;
		}

		public static Logger CreateForType<T>(string forceThisNamespace, bool useClassNameToo = false)
		{
			return useClassNameToo
					? new UnityLogger(forceThisNamespace, typeof(T).FullName)
					: new UnityLogger(forceThisNamespace)
					;
		}
		
		public static Logger CreateForType<T>(string forceThisNamespace, string forceThisClassName)
		{
			return new UnityLogger(forceThisNamespace, forceThisClassName);
		}

		public Level level =
#if DEBUG
            Level.TRACE;
#else
			Level.ERROR;
#endif
		private readonly String prefix;

		protected Logger(string forceThisNamespace)
		{
			this.prefix = string.Format("[{0}]", forceThisNamespace);
		}

		protected Logger(string forceThisNamespace, string forceThisClassName)
		{
			this.prefix = string.Format("[{0}-{1}]", forceThisNamespace, forceThisClassName);
		}

		protected abstract void log(string message, params object[] @params);
		protected abstract void log(Level level, Exception e, string message, params object[] @params);

		public void force(string message, params object[] @params)
		{
			this.log(message, @params);
		}

		public void trace(string message, params object[] @params)
		{
			if (!this.IsLoggable(level)) return;
			this.log(Level.TRACE, null, message, @params);
		}

		[ConditionalAttribute("DEBUG")]
		public void dbg(string message, params object[] @params)
		{
			this.log(Level.TRACE, null, message, @params);
		}

		public void info(string message, params object[] @params)
		{
			if (!this.IsLoggable(level)) return;
			this.log(Level.INFO, null, message, @params);
		}

		public void warn(string message, params object[] @params)
		{
			if (!this.IsLoggable(level)) return;
			this.log(Level.WARNING, null, message, @params);
		}

		public void warn(Exception e, string message, params object[] @params)
		{
			if (!this.IsLoggable(level)) return;
			this.log(Level.WARNING, e, message, @params);
		}

		public void error(string message, params object[] @params)
		{
			if (!this.IsLoggable(level)) return;
			this.log(Level.ERROR, null, message, @params);
		}

		public void error(Exception e, string message, params object[] @params)
		{
			if (!this.IsLoggable(level)) return;
			this.log(Level.ERROR, e, message, @params);
		}

		public void error(System.Object offended, System.Exception e)
		{
			if (!this.IsLoggable(level)) return;
			this.log(Level.ERROR, e, "{0} raised Exception {1}", offended.GetType().FullName, e.ToString());
		}

		protected bool IsLoggable(Level level)
		{
			return level <= this.level;
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

	public class UnityLogger : Logger
	{
		public UnityLogger(string forceThisNamespace) : base(forceThisNamespace) { }
		public UnityLogger(string forceThisNamespace, string forceThisClassName) : base(forceThisNamespace, forceThisClassName) { }

		private delegate void LogMethod(string message);

		protected override void log(string message, params object[] @params)
		{
			UnityEngine.Debug.Log(this.BuildMessage(message, @params));
		}
		
		protected override void log(Level level, Exception e, string message, params object[] @params)
		{
			LogMethod logMethod;
			switch (level)
			{
				case Level.OFF:
					return;
					
				case Level.TRACE:
					goto case Level.INFO;
				case Level.DETAIL:
					goto case Level.INFO;
				case Level.INFO:
					logMethod = UnityEngine.Debug.Log;
					break;

				case Level.WARNING:
					logMethod = UnityEngine.Debug.LogWarning;
					break;

				case Level.ERROR:
					logMethod = UnityEngine.Debug.LogError;
					break;

				default:
					throw new ArgumentException("unknown log level: " + level);
			}

			logMethod(this.BuildMessage(level, message, @params));
			if (e != null)
			{
				UnityEngine.Debug.LogException(e);
			}
		}
	}
}
