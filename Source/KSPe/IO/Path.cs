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
using System.Linq;
using SIO = System.IO;

namespace KSPe.IO
{
	// So Microsoft, in their infinite Wisdom, choose to resolve symlinks on the GetFullPath, rendering
	// symlinks useless on building virtual game instalments.
	// See https://forum.kerbalspaceprogram.com/index.php?/topic/192048-ksp-18-ksp-recall-0040-2020-0815/&do=findComment&comment=3838913
	//
	// So, why keeping the suffering? If it's brokem, rewrite it.
	// I will reimplement the whole fucking System if needed. :/
	public static class Path
	{
		public static readonly char AltDirectorySeparatorChar = SIO.Path.AltDirectorySeparatorChar;
		public static readonly char DirectorySeparatorChar = SIO.Path.DirectorySeparatorChar;
		public static readonly string AltDirectorySeparatorStr = ""+SIO.Path.AltDirectorySeparatorChar;
		public static readonly string DirectorySeparatorStr = ""+SIO.Path.DirectorySeparatorChar;

		[System.Obsolete ("see GetInvalidPathChars and GetInvalidFileNameChars methods.")]
		public static readonly char[] InvalidPathChars = SIO.Path.InvalidPathChars;

		public static readonly char PathSeparator = SIO.Path.PathSeparator;
		public static readonly char VolumeSeparatorChar = SIO.Path.VolumeSeparatorChar;

		public static string EnsureTrailingSeparatorOnDir(string path, bool blindlyAppend = false)
		{
			if (path.EndsWith(DirectorySeparatorStr) || path.EndsWith(AltDirectorySeparatorStr))
				return path;
			if (blindlyAppend) return path + DirectorySeparatorChar;

			return Directory.Exists(path)
					? path + DirectorySeparatorChar
					: path
				;
		}

		public static string ChangeExtension(string path, string extension) { return SIO.Path.ChangeExtension(path, extension); }

		public static string Combine(string path1, string path2)			{ return EnsureTrailingSeparatorOnDir(SIO.Path.Combine(path1, path2)); }
		public static string Combine(string path, params string[] paths) // Since we are here, why not backport some niceties from 4.x?
		{
			string r = path;
			foreach (string p in paths) r = Combine(r, p);
			return EnsureTrailingSeparatorOnDir(r);
		}

		public static string GetDirectoryName(string path)				{ return EnsureTrailingSeparatorOnDir(SIO.Path.GetDirectoryName(path), true); }
		public static string GetExtension(string path)					{ return SIO.Path.GetExtension(path); }
		public static string GetFileName(string path)					{ return SIO.Path.GetFileName(path); }
		public static string GetFileNameWithoutExtension(string path)	{ return SIO.Path.GetFileNameWithoutExtension(path); }

		public static string GetFullPath(string path)
		{
			return GetFullPath(path, false);
		}

		public static string GetFullPath(string path, bool iKnowItsDir)
		{
			if (!SIO.Path.IsPathRooted(path)) return GetFullPath(Combine(SIO.Directory.GetCurrentDirectory(), path), iKnowItsDir);
			string r = GetAbsolutePath(path);
			foreach (string k in UNREPARSE_KEYS)
			{
				if (r.StartsWith(k))
				{
					r = r.Replace(k, UNREPARSE[k]);
					break;
				}
			}
			return EnsureTrailingSeparatorOnDir(r, iKnowItsDir);
		}

		public static string GetAbsolutePath(string path)
		{
			return GetAbsolutePath(path, false);
		}

		public static string GetAbsolutePath(string path, bool iKnowItsDir)
		{
			return EnsureTrailingSeparatorOnDir(SIO.Path.GetFullPath(path), iKnowItsDir);
		}

		public static char[] GetInvalidFileNameChars()					{ return SIO.Path.GetInvalidFileNameChars(); }
		public static char[] GetInvalidPathChars()						{ return SIO.Path.GetInvalidPathChars(); }
		public static string GetPathRoot(string path)					{ return EnsureTrailingSeparatorOnDir(SIO.Path.GetPathRoot(path)); }
		public static string GetRandomFileName()						{ return SIO.Path.GetRandomFileName(); }

		[System.Security.Permissions.FileIOPermission(System.Security.Permissions.SecurityAction.Assert, Unrestricted = true)]
		public static string GetTempFileName()							{ return SIO.Path.GetTempFileName(); }

		[System.Security.Permissions.EnvironmentPermission(System.Security.Permissions.SecurityAction.Demand, Unrestricted = true)]
		public static string GetTempPath()								{ return EnsureTrailingSeparatorOnDir(SIO.Path.GetTempPath()); }

		public static bool HasExtension(string path)					{ return SIO.Path.HasExtension(path); }
		public static bool IsPathRooted(string path)					{ return SIO.Path.IsPathRooted(path); }

		private static string root = null;
		private static readonly Dictionary<string,string> UNREPARSE = new Dictionary<string, string>();
		private static readonly List<string> UNREPARSE_KEYS = new List<string>();
		[System.Obsolete("I'm unconfortable on exporting this call. Don't expect it on the next release, I can change my mind at any time!")]
		public static string Origin()
		{
			if (null != root) return root;	// Preventing accidents. No reentrant.

			Log.debug("Calculating Origin for {0}", typeof(KSPUtil).Assembly.Location);

			// Look for the GameData folder. This is our root.

			// this one doesn't works, Mono reparse the damned thing and we lose the original pathname the user used to launch the thing!
			//string path = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

			string assemblypath = typeof(KSPUtil).Assembly.Location;
			assemblypath = SIO.Path.GetDirectoryName(assemblypath);
			string path = null;

			{	// Let's try the quick & dirty way first!
				path = SIO.Path.GetDirectoryName(assemblypath)
					.Replace("KSP_x64_Data", ".")						// Win64 versions
					.Replace("KSP_Data", ".")							// Linux and Win32 versions
					.Replace("KSP.app/Contents/Resources/Data", ".")	// Mac
					.Replace("Managed", ".")							// Everybody
				;
				path = GetDirectoryName(GetAbsolutePath(path));
				if (!Directory.Exists(SIO.Path.Combine(path, "GameData"))) path = null;
			}

			if (null == path)
			{	// Oukey, not a standard rig, perhaps a debug/development one?
				Log.debug("GameData not found the easy way. Trying the harder path. #TumDumTsss");
				path = SIO.Path.GetDirectoryName(assemblypath);
				while (path.Length > 4) // The smaller relevant path for us is C:\ on Windows. Less than it, we are toasted, no GameData found!
				{
					Log.debug("Trying {0}", path);
					if (Directory.Exists(SIO.Path.Combine(path, "GameData"))) break;
					path = SIO.Path.GetDirectoryName(path);
				}
				path = (path.Length < 4) ? null : GetAbsolutePath(path);
			}

			if (null == path) KSPe.FatalErrors.NoGameDataFound.Show();

			Log.debug("Normalized path {0}", path);

			string currentDir = EnsureTrailingSeparatorOnDir(SIO.Directory.GetCurrentDirectory());
			process_dir(path, currentDir);

			// SYMLINKS are returned as is, ie, relative symlinks returns "../../something".
			// Realpaths returns itselves, is, /somedir/somefile
			foreach(string dir in Multiplatform.FileSystem.GetDirectories(currentDir, "*", SIO.SearchOption.AllDirectories))
				process_dir(currentDir, dir);

			root = EnsureTrailingSeparatorOnDir(path);  // Note: it should end with a DSC because I do fast string manipulations everywhere, and they depends on it.

			if (!currentDir.Equals(root)) // The PWD is reparsed by mono on MacOS. On Linux, **it's not**! Damn you, Microsoft!
				register_alias(currentDir, root);

			UNREPARSE_KEYS.AddRange(UNREPARSE.Keys.OrderByDescending( x => x.Length));

			Log.debug("Origin is {0}", root);
			Log.debug("PWD    is {0}", currentDir);
			return root;
		}

		private static void process_dir(string path, string dir)
		{
			if (path.Equals(dir)) return;	// The foreach that feeds this thing gives us the "." file, normalized, too!
			Log.debug("process_dir: path is {0}", path);
			Log.debug("process_dir: dir  is {0}", dir);
			if (Multiplatform.FileSystem.IsReparsePoint(dir))
			{
				string reparsed = Multiplatform.FileSystem.ReparsePath(
					IsPathRooted(dir)
						? dir
						: Combine(path, dir)
				);
				register_alias(reparsed, dir);
			}
		}

		private static void register_alias(string reparsed, string dir)
		{
			reparsed = EnsureTrailingSeparatorOnDir(reparsed);
			dir = EnsureTrailingSeparatorOnDir(dir);
			UNREPARSE[reparsed] = dir;
			Log.debug("UNREPARSE {0} <- {1}", reparsed, UNREPARSE[reparsed]);
		}

		private static readonly KSPe.Util.Log.Logger Log = KSPe.Util.Log.Logger.CreateForType<Startup>("KSPe.IO", "Path", 0);
	}
}
