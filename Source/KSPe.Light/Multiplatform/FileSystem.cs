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

using SIO = System.IO;

/**
 * Stub to simulate the original services.
 */
namespace KSPe.Multiplatform
{
	[System.Obsolete("This class will be made internal on Version 2.5. **DO NOT** use it outside KSPe.dll.")]
	public static class FileSystem
	{
		public static bool IsReparsePoint(string path) => false;

		public static string[] GetDirectories(string path, string searchPattern, SIO.SearchOption searchOption)
		{
			return SIO.Directory.GetDirectories(path, searchPattern, searchOption);
		}

		internal static string ReparsePath(string path)
		{
			return path;
		}
	}
}
