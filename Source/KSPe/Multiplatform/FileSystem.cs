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
using SIO = System.IO;

namespace KSPe.Multiplatform
{
	public static class FileSystem
	{
		private static string realpath = null;
		private static string readlink = null;
		private static readonly string[] posix_paths = {"/opt/local/bin", "/opt/bin", "/usr/local/bin", "/usr/bin", "/bin"};
		static FileSystem()
		{
			foreach (string path in posix_paths)
			{
				if (!SIO.Directory.Exists(path)) continue;
				foreach (string p in SIO.Directory.GetFiles(path, "realpath"))
				{
					realpath = p;
					break;
				}
				foreach (string p in SIO.Directory.GetFiles(path, "readlink"))
				{
					realpath = p;
					break;
				}
				if (null != realpath || null != readlink) break;
			}
		}

		private static string Reparse(string path)
		{
			string cmd = realpath??readlink??null;
			if (null == cmd) return path;
			try
			{
				string r = Shell.command(cmd, "-n " + path);
				return r;
			}
			catch (Shell.Exception e)
			{
				UnityEngine.Debug.LogWarningFormat("Failed to reparse {0}.", path);
				UnityEngine.Debug.LogError(e);
				return path;
			}
		}

		public static string ReparsePath(string path)
		{
			string r = path;
			do {
				r = Reparse(r);
				if (SIO.Path.IsPathRooted(r)) break;
				r = SIO.Path.GetFullPath(SIO.Path.Combine(SIO.Path.GetDirectoryName(path),r));
			} while(true);
			return r;
		}
	}
}
