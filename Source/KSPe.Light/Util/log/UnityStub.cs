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

namespace KSPe.Util.Log {

	public class UnityLogger : Logger
	{
		public UnityLogger(Type type) : base(type) { }
		public UnityLogger(Type type, string forceThisNamespace) : base(type, forceThisNamespace) { }
		public UnityLogger(Type type, string forceThisNamespace, string forceThisClassName) : base(type, forceThisNamespace, forceThisClassName) { }

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
