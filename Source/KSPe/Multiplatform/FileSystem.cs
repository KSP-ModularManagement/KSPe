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
using System.Collections.Generic;
using SIO = System.IO;

namespace KSPe.Multiplatform
{
	public static class FileSystem
	{
		private static string realpath = null;
		private static string readlink = null;
		private static readonly string[] posix_paths = {
			"/opt/local/libexec/gnubin", // Used on my rig (MacOS with MacPorts).
			"/opt/local/bin", "/opt/bin", "/usr/local/bin", "/usr/bin", "/bin"
		};
		static FileSystem()
		{
			foreach (string path in posix_paths)
			{
				if (!SIO.Directory.Exists(path)) continue;
				if (null == realpath) foreach (string p in SIO.Directory.GetFiles(path, "realpath"))
				{
					realpath = p;
					break;
				}
				if (null == readlink) foreach (string p in SIO.Directory.GetFiles(path, "readlink"))
				{
					readlink = p;
					break;
				}
				if (null != realpath && null != readlink) break;
			}
		}

		private static string Reparse_realpath(string path)
		{
			try
			{
				string rl = Shell.command(realpath, "-eLz " + path);
				return rl;
			}
			catch (System.Exception e)
			{
				UnityEngine.Debug.LogWarningFormat("Failed to reparse {0}.", path);
				UnityEngine.Debug.LogError(e);
				throw e;
			}
		}

		private static string Reparse_readlink(string path)
		{
			List<string> parcels = new List<string>();

			do {
				try
				{
					string rl = Shell.command(readlink, "-n " + path);
					parcels.Add(rl);
				}
				catch (Shell.Exception e)
				{
					if (1 != e.exitCode)
					{
						UnityEngine.Debug.LogWarningFormat("Failed to reparse {0}.", path);
						UnityEngine.Debug.LogError(e);
						return path;	// May God help the caller. :)
					}
					parcels.Add(IO.Path.GetFileName(path));
				}
				finally
				{
					path = SIO.Path.GetDirectoryName(path);
				}
			} while (null != path);

			parcels.Reverse();
			string r = ""+SIO.Path.DirectorySeparatorChar;
			foreach (string p in parcels) r = SIO.Path.Combine(r, p);
			return SIO.Path.GetFullPath(r);
		}

		public static string ReparsePath(string path)
		{
			//try
			//{
			//	if (null != realpath) return Reparse_realpath(path);
			//} catch (System.Exception) { } // If anything goes wrong, just try readlink.
			if (null != readlink) return Reparse_readlink(path);
			return path;
		}

		public static bool IsReparsePoint(string path)
		{
			SIO.FileInfo pathInfo = new SIO.FileInfo(path);
			return 0 != (pathInfo.Attributes & SIO.FileAttributes.ReparsePoint);
		}
	}
}
