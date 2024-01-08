/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

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
using System.IO;

namespace kspofflinecheck.util
{
	internal class KSP
	{
		internal static bool Found_in(string path)
		{
			if (Directory.Exists(Path.Combine(path, "KSP.app"))) return true;
			if (File.Exists(Path.Combine(path, "KSP.x86"))) return true;
			if (File.Exists(Path.Combine(path, "KSP.x86_64"))) return true;
			if (File.Exists(Path.Combine(path, "KSP.exe"))) return true;
			if (File.Exists(Path.Combine(path, "KSP_x64.exe"))) return true;
			return false;
		}

		private static readonly string[] PATH_CANDIDATES = {
			"KSP.app/Contents/Resources/Data/Managed",
			"KSP_Data/Managed",
			"KSP_x64_Data/Managed/"
		};
		internal static string Find_Managed(string path)
		{
			foreach (string p in PATH_CANDIDATES)
			{
				string full = Path.Combine(path, p);
				if (Directory.Exists(full)) return full;
			}
			throw new FileNotFoundException(string.Format("No KSP found in {0}", path));
		}
	}
}
