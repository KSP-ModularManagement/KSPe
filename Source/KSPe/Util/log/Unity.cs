/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-20 Lisias T : http://lisias.net <support@lisias.net>

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
using UnityEngine;

namespace KSPe.Util.Log {

	public class UnityLogger : Logger
	{
		public UnityLogger(string forceThisNamespace) : base(forceThisNamespace) { }
		public UnityLogger(string forceThisNamespace, string forceThisClassName) : base(forceThisNamespace, forceThisClassName) { }

#pragma warning disable IDE0052 // Remove unread private members
		private readonly UnityLogDecorator unityLog = UnityLogDecorator.INSTANCE; // Just to instantiate the damned thing.
#pragma warning restore IDE0052 // Remove unread private members

		protected override LogMethod select()
		{
			switch (this._level)
			{
				case Level.OFF:
					return dummy;
					
				case Level.TRACE:
					goto case Level.INFO;
				case Level.DETAIL:
					goto case Level.INFO;
				case Level.INFO:
					return this.log;

				case Level.WARNING:
					return this.logWarning;

				case Level.ERROR:
					return this.logError;

				default:
					throw new ArgumentException("unknown log level: " + level);
			}
		}

		protected void dummy(string message) {}

		protected override void log(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		protected void logWarning(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		protected void logError(string message)
		{
			UnityEngine.Debug.Log(message);
		}

		protected override void logException(string message, Exception e)
		{
			UnityEngine.Debug.LogError(message);
			if (e != null)
			{
				UnityEngine.Debug.LogException(e);
			}
		}
	}

	internal class UnityLogDecorator : UnityEngine.ILogHandler
	{
		internal static UnityLogDecorator instance;
		internal static UnityLogDecorator INSTANCE {
			get {
				if (null == instance) instance = new UnityLogDecorator();
				return instance;
			}
		}

		private readonly UnityEngine.ILogHandler upstream;
		private static readonly object MUTEX = new object();

		internal UnityLogDecorator()
		{
			this.upstream = UnityEngine.Debug.logger.logHandler;
			UnityEngine.Debug.logger.logHandler = this;
		}

		void ILogHandler.LogException(Exception exception, UnityEngine.Object context)
		{
			lock (MUTEX)
			{
				this.upstream.LogException(exception, context);
			}
		}

		void ILogHandler.LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
			lock (MUTEX)
			{
				this.upstream.LogFormat(logType, context, format, args);
			}
		}
	}
}
