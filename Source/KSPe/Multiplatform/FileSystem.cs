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
		internal static readonly bool CASE_SENSITIVE_FILESYSTEM = GetCaseSensitiveFileSystem();

		private static string realpath = null;
		private static string readlink = null;
		private static readonly string[] posix_paths = {
			"/opt/local/libexec/gnubin", // Used on my rig (MacOS with MacPorts).
			"/opt/local/bin", "/opt/bin", "/usr/local/bin", "/usr/bin", "/bin"
		};
		private static readonly Dictionary<string,string> UNREPARSE_CACHE = new Dictionary<string, string>();
		private const int UNREPARSE_CACHE_TIMER_INTERVAL = 1000 * 5 * 60; // 5 minutes
		private static readonly System.Timers.Timer UNREPARSE_CACHE_TIMER = new System.Timers.Timer(UNREPARSE_CACHE_TIMER_INTERVAL);
		private static readonly Dictionary<string,string> REALPATH_CACHE = new Dictionary<string, string>();
		private const int REALPATH_CACHE_TIMER_INTERVAL = 1000 * 5 * 60; // 5 minutes
		private static readonly System.Timers.Timer REALPATH_CACHE_TIMER = new System.Timers.Timer(UNREPARSE_CACHE_TIMER_INTERVAL);

		static FileSystem()
		{
			UNREPARSE_CACHE_TIMER.AutoReset = true;
			UNREPARSE_CACHE_TIMER.Enabled = false;
			UNREPARSE_CACHE_TIMER.Elapsed += (source, evenArgs) => { Log.debug("FileSystem is clearing the unreparsing cache..."); UNREPARSE_CACHE.Clear(); };

			REALPATH_CACHE_TIMER.AutoReset = true;
			REALPATH_CACHE_TIMER.Enabled = false;
			REALPATH_CACHE_TIMER.Elapsed += (source, evenArgs) => { Log.debug("FileSystem is clearing the real path cache..."); UNREPARSE_CACHE.Clear(); };

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

		private static bool GetCaseSensitiveFileSystem()
		{
			string path = typeof(FileSystem).Assembly.Location;
			string mangledpath = path.ToLower();
			bool a = SIO.File.Exists(path);
			bool b = SIO.File.Exists(mangledpath);
			Log.debug("GetCaseInsensitiveFileSystem {0} {1}", path, a);
			Log.debug("GetCaseInsensitiveFileSystem {0} {1}", mangledpath, b);
			if (!(a || b))
			{
				Log.error("Something unexpected and serious happened - no version of the test file were found! Assuming worst case!");
				return true;
			}
			bool r = !(a && b) ;
			Log.detail("KSPe is running from a Case {0} File System.", r ? "Sensitive" : "Insensitive");
			return r;
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
			UNREPARSE_CACHE_TIMER.Stop();
			UNREPARSE_CACHE_TIMER.Interval = UNREPARSE_CACHE_TIMER_INTERVAL;
			UNREPARSE_CACHE_TIMER.Start();
			if (!UNREPARSE_CACHE.ContainsKey(path))
				UNREPARSE_CACHE[path] = IsReparsePoint(path) ? reparsePath(path) : path;
			return UNREPARSE_CACHE[path];
		}
		private static string reparsePath(string path)
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

		public static string GetCurrentDirectory()
		{
			string path = SIO.Directory.GetCurrentDirectory();
			path = GetRealPathname(path);
			return path;
		}

		// FIXME: Must be a smarter way to do this.
		// CMD, PowerShell and even CygWin does it, right?
		// See https://github.com/net-lisias-ksp/KSPe/issues/37
		public static string GetRealPathname(string path)
		{
			if (CASE_SENSITIVE_FILESYSTEM) return path;

			REALPATH_CACHE_TIMER.Stop();
			REALPATH_CACHE_TIMER.Interval = REALPATH_CACHE_TIMER_INTERVAL;
			REALPATH_CACHE_TIMER.Start();
			if (!REALPATH_CACHE.ContainsKey(path))
				REALPATH_CACHE[path] = getRealPathname(path);
			string r = REALPATH_CACHE[path];
			Log.debug("GetRealPathName({0}) => {1}", path, r);
			return r;
		}
		private static string getRealPathname(string path)
		{
			path = SIO.Path.GetFullPath(path);
			if (!(SIO.File.Exists(path) || SIO.Directory.Exists(path))) return path;

			string[] parcels = path.Split(SIO.Path.DirectorySeparatorChar);
			string r = "/";
			if (LowLevelTools.Windows.IsThisWindows)
			{
				Log.debug("getRealPathname on Windows");
				r = parcels[0] + KSPe.IO.Path.DirectorySeparatorStr;
				foreach (SIO.DriveInfo di in SIO.DriveInfo.GetDrives()) if (di.Name.Equals(r, System.StringComparison.OrdinalIgnoreCase))
				{
					r = di.Name;
					break;
				}
			}
			for (int i = 1; i < parcels.Length; ++i)
			{
				Log.debug("Probing {0} {1}", r, parcels[i]);
				string[] e = SIO.Directory.GetFileSystemEntries(r, parcels[i]);
				if (1 != e.Length)
				{
					Log.error("Something weird happened while handling getRealPathname({0}). Returning original path and hoping for the best.", path);
					return path;
				}
				r = e[0];
			}

			return r;
		}
	}
}
