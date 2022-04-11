/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-22 LisiasT : http://lisias.net <support@lisias.net>

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

namespace KSPe.Util.Log {

	public class UnityLogger : Logger
	{
		internal UnityLogger(Type type, int skipStackLevels, UnityEngine.ILogHandler logHandler = null) : base(type, skipStackLevels)
		{
			this.unityLog = logHandler ?? UnityLogDecorator.INSTANCE;
#if DEBUG
			UnityEngine.Debug.LogFormat("Instantiating Unity Logger for {0} with {1}", type, this.unityLog.GetType().Name);
#endif
		}

		internal UnityLogger(Type type, string forceThisNamespace, int skipStackLevels, UnityEngine.ILogHandler logHandler = null) : base(type, forceThisNamespace, skipStackLevels)
		{
			this.unityLog = logHandler ?? UnityLogDecorator.INSTANCE;
#if DEBUG
			UnityEngine.Debug.LogFormat("Instantiating Unity Logger for {0} with {1}", forceThisNamespace, this.unityLog.GetType().Name);
#endif
		}

		internal UnityLogger(Type type, string forceThisNamespace, string forceThisClassName, int skipStackLevels, UnityEngine.ILogHandler logHandler = null) : base(type, forceThisNamespace, forceThisClassName, skipStackLevels)
		{
			this.unityLog = logHandler ?? UnityLogDecorator.INSTANCE;
#if DEBUG
			UnityEngine.Debug.LogFormat("Instantiating Unity Logger for {0}.{1} with {1}", forceThisNamespace, forceThisClassName, this.unityLog.GetType().Name);
#endif
		}

		private readonly UnityEngine.ILogHandler unityLog;

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
}
