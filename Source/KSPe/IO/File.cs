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
using System.Linq;
using System.Reflection;

using SIO = System.IO;

namespace KSPe.IO
{
	public static class File
	{
		// TODO: Get rid of deprecated artifacts on the next major release.
		[System.Obsolete("KSPe.IO.File.GAMEDATA is deprecated, please use KSPe.IO.Hierarchy.GAMEDATA instead.")]
		public const string GAMEDATA = "GameData";
		[System.Obsolete("KSPe.IO.File.GAMEDATA is deprecated, please use KSPe.IO.Hierarchy.PLUGINDATA instead.")]
		public const string PLUGINDATA = "PluginData";                                // Writeable data on <KSP_ROOT>/PluginData/<plugin_name>/
		[System.Obsolete("KSPe.IO.File.GAMEDATA is deprecated, please use KSPe.IO.Hierarchy.LOCALDATA instead.")]
		public static string LOCALDATA => SIO.Path.Combine(GAMEDATA, "__LOCAL");      // Custom runtime generated parts on <KSP_ROO>/GameData/__LOCAL/<plugin_name> (specially made for UbioWeldingLtd)

		[System.Obsolete("KSPe.IO.File.CalculateKspPath is deprecated, please use KSPe.IO.Hierarchy.ROOT.Solve instead.")]
		public static string CalculateKspPath(string fname, params string[] fnames)
		{
			return Hierarchy.ROOT.Solve(fname, fnames);
		}

		[System.Obsolete("KSPe.IO.File.CalculateRelativePath is deprecated. There shuold be no need for this anymore.")]
		public static string CalculateRelativePath(string fullDestinationPath)
		{
			return Hierarchy.CalculateRelativePath(fullDestinationPath, SIO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)); //FIXME: This only works when KSPe is on the GameData/ !!
		}

		internal static string[] List(string rawdir, string mask = "*", bool include_subdirs = false)
		{
			if (!SIO.Directory.Exists(rawdir))
				throw new SIO.FileNotFoundException(rawdir);

			string[] files = SIO.Directory.GetFiles(
									rawdir,
									mask,
									include_subdirs ? SIO.SearchOption.AllDirectories : SIO.SearchOption.TopDirectoryOnly
								);
			files = files.OrderBy(x => x).ToArray();            // This will sort 1, 2, 10, 12 
																//			Array.Sort(files, StringComparer.CurrentCulture);   // This will sort 1, 10, 12, 2

			for (int i = files.Length; --i >= 0;)
				files[i] = files[i].Substring(files[i].IndexOf(rawdir, StringComparison.Ordinal) + rawdir.Length + 1); // +1 to get rid of the trailling "/"

			return files;
		}
	}
}
