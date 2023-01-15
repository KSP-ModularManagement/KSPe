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
using System.IO.IsolatedStorage;
using SIO = System.IO;
using DirectorySecurity = System.Security.AccessControl.DirectorySecurity;
using AccessControlSections =  System.Security.AccessControl.AccessControlSections;

namespace KSPe.IO
{
	public static class Directory
	{
		public static SIO.DirectoryInfo CreateDirectory (string path)	{ return SIO.Directory.CreateDirectory(path); }

		[Obsolete("[MonoLimitation (DirectorySecurity not implemented")]
		public static SIO.DirectoryInfo CreateDirectory (string path, DirectorySecurity directorySecurity)	{ return SIO.Directory.CreateDirectory(path, directorySecurity); }

		public static void Delete (string path)
		{
			if (Multiplatform.FileSystem.IsReparsePoint(path))
				throw new IsolatedStorageException(string.Format("Cannot delete symlinks. Faulty is {0}", path));
			SIO.Directory.Delete(path);
		}
		public static void Delete (string path, bool recursive)
		{
			if (Multiplatform.FileSystem.IsReparsePoint(path))
				throw new IsolatedStorageException(string.Format("Cannot delete symlinks. Faulty is {0}", path));
			SIO.Directory.Delete(path, recursive);
		}

		public static bool Exists (string path)						{ return SIO.Directory.Exists(RealPath(path)); }

		public static DirectorySecurity GetAccessControl (string path)											{ return SIO.Directory.GetAccessControl(RealPath(path)); }
		public static DirectorySecurity GetAccessControl (string path, AccessControlSections includeSections)	{ return SIO.Directory.GetAccessControl(RealPath(path), includeSections); }

		public static DateTime GetCreationTime (string path)		{ return SIO.Directory.GetCreationTime(RealPath(path)); }
		public static DateTime GetCreationTimeUtc (string path)		{ return SIO.Directory.GetCreationTimeUtc(RealPath(path)); }

		public static string GetCurrentDirectory ()																{ return GetCurrentDirectoryInternal(); } // FIXME Gambiarra temporaria enquanto eu nao arrumo a issue #37
		internal static string GetCurrentDirectoryInternal ()													{ return Path.GetFullPathInternal(KSPe.Multiplatform.FileSystem.GetCurrentDirectory(), true); }

		public static string[] GetDirectories (string path)														{ return Path.GetFullPathInternal(SIO.Directory.GetDirectories(RealPath(path)), true); }
		public static string[] GetDirectories (string path, string searchPattern)								{ return Path.GetFullPathInternal(SIO.Directory.GetDirectories(RealPath(path), searchPattern), true); }
		public static string[] GetDirectories (string path, string searchPattern, SIO.SearchOption searchOption) { return Path.GetFullPathInternal(SIO.Directory.GetDirectories(RealPath(path), searchPattern, searchOption), true); }
		public static string GetDirectoryRoot (string path)														{ return Path.GetFullPathInternal(Path.GetFullPath(SIO.Directory.GetDirectoryRoot(RealPath(path))), true); }

		public static string[] GetFiles (string path)															{ return Path.GetFullPathInternal(SIO.Directory.GetFiles(RealPath(path)), false); }
		public static string[] GetFiles (string path, string searchPattern)										{ return Path.GetFullPathInternal(SIO.Directory.GetFiles(RealPath(path), searchPattern), false); }
		public static string[] GetFiles (string path, string searchPattern, SIO.SearchOption searchOption)		{ return Path.GetFullPathInternal(SIO.Directory.GetFiles(RealPath(path), searchPattern, searchOption), false); }

		public static string[] GetFileSystemEntries (string path)												{ return Path.GetFullPathInternal(SIO.Directory.GetFileSystemEntries(RealPath(path)), false); }
		public static string[] GetFileSystemEntries (string path, string searchPattern)							{ return Path.GetFullPathInternal(SIO.Directory.GetFileSystemEntries(RealPath(path), searchPattern), false); }

		public static DateTime GetLastAccessTime (string path)		{ return SIO.Directory.GetLastAccessTime(RealPath(path)); }
		public static DateTime GetLastAccessTimeUtc (string path)	{ return SIO.Directory.GetLastAccessTimeUtc(RealPath(path)); }
		public static DateTime GetLastWriteTime (string path)		{ return SIO.Directory.GetLastWriteTime(RealPath(path)); }
		public static DateTime GetLastWriteTimeUtc (string path)	{ return SIO.Directory.GetLastWriteTimeUtc(RealPath(path));}

		public static string[] GetLogicalDrives ()					{ return Path.GetFullPathInternal(SIO.Directory.GetLogicalDrives(), true); }

		public static SIO.DirectoryInfo GetParent (string path)	{ return SIO.Directory.GetParent(RealPath(path)); }

		public static void Move (string sourceDirName, string destDirName)
		{
			if (Multiplatform.FileSystem.IsReparsePoint(sourceDirName))
				throw new IsolatedStorageException(string.Format("Cannot Move symlinks. Faulty is {0}", sourceDirName));
			SIO.Directory.Move(sourceDirName, destDirName);
		}

		public static void SetAccessControl (string path, DirectorySecurity directorySecurity)	{ SIO.Directory.SetAccessControl(RealPath(path), directorySecurity);}
		public static void SetCreationTime (string path, DateTime creationTime)					{ SIO.Directory.SetCreationTime(RealPath(path), creationTime);}
		public static void SetCreationTimeUtc (string path, DateTime creationTimeUtc)			{ SIO.Directory.SetCreationTimeUtc(RealPath(path), creationTimeUtc);}

		public static void SetCurrentDirectory (string path)									{ throw new IsolatedStorageException("You are now allowed to change the Current Directory."); }

		public static void SetLastAccessTime (string path, DateTime lastAccessTime)				{ SIO.Directory.SetLastAccessTime(RealPath(path), lastAccessTime); }
		public static void SetLastAccessTimeUtc (string path, DateTime lastAccessTimeUtc)		{ SIO.Directory.SetLastAccessTimeUtc(RealPath(path), lastAccessTimeUtc);}
		public static void SetLastWriteTime (string path, DateTime lastWriteTime)				{ SIO.Directory.SetLastWriteTime(RealPath(path), lastWriteTime);}
		public static void SetLastWriteTimeUtc (string path, DateTime lastWriteTimeUtc)			{ SIO.Directory.SetLastWriteTimeUtc(RealPath(path), lastWriteTimeUtc); }

		private static string RealPath(string path)
		{
			// No, we don't use CurrentDir here, we are confined insided the KSP folder structure, rememeber? ;)
			if (!SIO.Path.IsPathRooted(path)) path = SIO.Path.Combine(Hierarchy.ROOTPATH, path);

			path = Multiplatform.FileSystem.GetRealPathname(path);

			// Ow, great. Just great. :(
			// https://github.com/dotnet/corefx/pull/5020
			// So Symlinks to directories are not recognized as a directory. So I need to do the job myself.
			return Multiplatform.FileSystem.IsReparsePoint(path) ? Multiplatform.FileSystem.ReparsePath(path) : path;
		}
	}
}