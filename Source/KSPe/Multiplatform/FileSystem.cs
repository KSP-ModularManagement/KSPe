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
using System.Collections.Generic;
using System.ComponentModel;

using SIO = System.IO;

namespace KSPe.Multiplatform
{
	[System.Obsolete("This class will be made internal on Version 2.5. **DO NOT** use it outside KSPe.dll.")]
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
			if (KSPe.Multiplatform.LowLevelTools.Unix.IsThisUnix) foreach (string path in posix_paths)
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
			Log.debug("Multiplatform.FileSystem: realpath found on {0}", realpath??"NOT FOUND!");
			Log.debug("Multiplatform.FileSystem: readlink found on {0}", readlink??"NOT FOUND!");
		}

		private static string Reparse_realpath(string path)
		{
			try
			{
				string rl = Shell.command(realpath, string.Format("-eLz \"{0}\"",path));
				return rl;
			}
			catch (System.Exception e)
			{
				Log.error(e, "Failed to reparse {0}.", path);
				throw e;
			}
		}

		private static string Reparse_readlink(string path)
		{
			List<string> parcels = new List<string>();

			do {
				try
				{
					string rl = Shell.command(readlink, string.Format("-n \"{0}\"", path));
					parcels.Add(rl);
				}
				catch (Shell.Exception e)
				{
					if (1 != e.exitCode)
					{
						Log.error(e, "Failed to reparse {0}.", path);
						return path;	// May God help the caller. :)
					}
					parcels.Add(IO.Path.GetFileName(path));
				}
				catch (Win32Exception e)
				{
					// This happens here when the System is running out of File Handlers. 
					// The Shell.Command *WAS* being used on a IDisposable structure, but unfortunately
					// the apparently **IDIOTS** that coded that crap apparently was not closing the
					// file handlers on the IDisposable implementation.
					// 
					// This is also a hint that I should push KSPe to mainstream the fastest I can, and start to phase out
					// the KSPe.Light program - I'm getting a huge surface of exposition to MS/Mono/Whoever crap.
					Log.warn("Reparse_readlink got a {0} ", e.Message);
					return path;	// May God help the caller. :)
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

		private static string Reparse_windows(string path)
		{
			try
			{ 
				string r = LowLevelTools.Windows32.GetFinalPathName(path).Replace("\\\\?\\","").Replace("\\\\.\\",""); // Gets rid of the UNC stunt. Why, Microsoft? Why?
				return SIO.Path.GetFullPath(r);
			}
			catch (System.ComponentModel.Win32Exception e)
			{
				Log.debug("Failed to reparse {0} due {1}.", path, e.Message);
				return SIO.Path.GetFullPath(path);
			}
		}

		public static string ReparsePath(string path)
		{
			Log.debug("Reparsing {0}", path);

			if (LowLevelTools.Windows.IsThisWindows) return Reparse_windows(path);
			//try
			//{
			//	if (null != realpath) return Reparse_realpath(path);
			//} catch (System.Exception) { } // If anything goes wrong, just try readlink.
			if (null != readlink) return Reparse_readlink(path);

			// If everything else fails, oh well...
			return path;
		}

		public static bool IsReparsePoint(string path)
		{
			SIO.FileInfo pathInfo = new SIO.FileInfo(path);

			// This is prone to failure. A SymLink is only one of the possible
			// ReparsePoints on Windows (and, so, C#). I need to inspect the
			// ReparsePoint and check if it's a symbolic link or something else.
			// https://coderedirect.com/questions/136750/check-if-a-file-is-real-or-a-symbolic-link
			// Problem: I don't know if this will work on MacOS or Linux.
			return 0 != (pathInfo.Attributes & (SIO.FileAttributes.ReparsePoint));
		}

		public static string[] GetDirectories(string path, string searchPattern, SIO.SearchOption searchOption)
        {
			return SIO.Directory.GetDirectories(path, searchPattern, searchOption);
        }
	}
}
