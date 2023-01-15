/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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

namespace kspofflinecheck
{
	static class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("KSPe's Console Tool for KSP v{0}", KSPe.Version.Text);
			if (0 == args.Length) Print_Help_and_Exit(0);
			switch (args[0])
			{
				case "DLL": Checks.DLL.Do(GetParms(args)); break;
				default:
					Console.Error.WriteLine("Unrecognized command {0}!", args[0]);
					Print_Help_and_Exit(-1);
					break;
			}
		}

		private static void Print_Help_and_Exit(int exitCode)
		{
			// TODO
			System.Environment.Exit(exitCode);
		}

		private static string[] GetParms(string[] args)
		{
			string[] r = new string[args.Length-1];
			Array.Copy(args, 1, r, 0, r.Length);
			return r;
		}

	}
}
