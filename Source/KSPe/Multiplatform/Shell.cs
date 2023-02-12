/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP Enhanced /L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using System.Diagnostics;

namespace KSPe.Multiplatform
{
	internal static class Shell
	{
		public class Exception : System.Exception
		{
			public readonly int exitCode;
			public readonly string command;
			public readonly string stderr;

			public override string ToString()
			{
				return String.Format("{0} returned {1} ({2})", command, exitCode, stderr??"no stderr");
			}

			private Exception(string message, string command, int exitCode, string stderr) : base(message)
			{
				this.exitCode = exitCode;
				this.command = command;
				this.stderr = stderr;
			}

			internal static Exception create(string command, int exitCode, string stderr = "")
			{
				string msg = string.Format("Command [{0}] returned {1}", command, exitCode);
				return new Exception(msg, command, exitCode, stderr);
			}

			internal static void raise(string command, int exitCode, string stderr = "")
			{
				Exception e = create(command, exitCode, stderr);
				throw e;
			}
		}

		public static string run(string program, string[] text)
		{
			/*
			 * This is absolutely f*cking unbeliable!!! 
			 * They didn't called Close on the IDisposable implementation, so every
			 * shell command I launched was leaking at least 3 file handlers (stdin, stdout and stderr)!!
			 * 
			 * By swithing using (Process cmd = new Process()) by this try-finally structure, I solved the
			 * avalanche of Win32Exceptions being thrown when I have enought KSP.Lights being loaded.
			 * 
			 * GOD KNOWS how many users were royally screwed by this crap!!!
			 * 
			 * DAMN YOU MONO AND MICROSOFT "DEVELOPERS".
			 */
			Process cmd = null;
			try
			{
				cmd = new Process();
				cmd.StartInfo.FileName = program;
				cmd.StartInfo.RedirectStandardInput = true;
				cmd.StartInfo.RedirectStandardOutput = true;
				cmd.StartInfo.RedirectStandardError = true;
				cmd.StartInfo.CreateNoWindow = false;
				cmd.StartInfo.UseShellExecute = false;
				cmd.Start();

				foreach (string t in text) cmd.StandardInput.WriteLine(t);
				cmd.StandardInput.Flush();
				cmd.StandardInput.Close();

				cmd.WaitForExit();
				if (0 != cmd.ExitCode) Exception.raise(program, cmd.ExitCode, cmd.StandardError.ReadToEnd());
				return cmd.StandardOutput.ReadToEnd();
			}
			finally
			{
				cmd?.Close();
			}
		}

		public static string command(string command, string commandline)
		{
			// See the remarks on `run` method. DAMN!!
			Process cmd = null;
			try 
			{
				cmd = new Process();

				cmd.StartInfo.FileName = command;
				cmd.StartInfo.Arguments = commandline;
				cmd.StartInfo.RedirectStandardInput = false;
				cmd.StartInfo.RedirectStandardOutput = true;
				cmd.StartInfo.RedirectStandardError = true;
				cmd.StartInfo.CreateNoWindow = false;
				cmd.StartInfo.UseShellExecute = false;
				cmd.Start();

				cmd.WaitForExit();
				if (0 != cmd.ExitCode) Exception.raise(command, cmd.ExitCode, cmd.StandardError.ReadToEnd());
				return cmd.StandardOutput.ReadToEnd();
			}
			finally
			{
				cmd?.Close();
			}
		}
	}
}
