/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-21 Lisias T : http://lisias.net <support@lisias.net>

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
using System.Text;
using SIO = System.IO;

namespace KSPe.Util.Log {

	public class FileChainUnityLogger<T> : Logger
	{
		private static readonly Encoding ENCODING = new UTF8Encoding(false);
		private readonly SIO.FileStream fs;

		// TODO: Remove this on Version 2.4
		[Obsolete("This method is deprecated. Use FileChainUnityLogger(loogname, skipStackLevels) instead.")]
		public FileChainUnityLogger(string logname) : base(typeof(T), 0)
		{
			string fullpathname = IO.Path.Combine(KSPe.IO.File<T>.Data.Solve(logname + ".log"));
			this.fs = SIO.File.OpenWrite(fullpathname);
		}

		public FileChainUnityLogger(string logname, int skipStackLevels = 0) : base(typeof(T), skipStackLevels)
		{
			string fullpathname = IO.Path.Combine(KSPe.IO.File<T>.Data.Solve(logname + ".log"));
			this.fs = SIO.File.OpenWrite(fullpathname);
		}

		public override void Close()
		{
			base.Close();
			this.fs.Close();
		}

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
			this.write(message);
		}

		protected void logWarning(string message)
		{
			UnityEngine.Debug.Log(message);
			this.write(message);
		}

		protected void logError(string message)
		{
			UnityEngine.Debug.Log(message);
			this.write(message);
		}

		protected override void logException(string message, Exception e)
		{
			UnityEngine.Debug.LogError(message);
			this.write(message);
			if (e != null)
			{
				UnityEngine.Debug.LogException(e);
				this.write(e.Message);
				this.write(e.StackTrace);
			}
		}

		private void write(string message)
		{
			Byte[] bytes = ENCODING.GetBytes(message + "\n");
			this.fs.Write(bytes, 0, bytes.Length);
		}
	}
}
