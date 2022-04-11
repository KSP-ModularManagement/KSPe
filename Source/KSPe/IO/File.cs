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
using System.Linq;
using System.Reflection;

using SIO = System.IO;

namespace KSPe.IO
{
	// TODO: Make the whole thing **Internal** as soon as possible.
	// NOTE: Ignore the Obsolete Warning inside this project until there.
	[System.Obsolete("KSPe.IO.File is deprecated, please use KSPe.IO.File<T> instead.")]
	public static class File
	{
		// TODO: Get rid of deprecated artifacts on the next major release.
		[System.Obsolete("KSPe.IO.File.GAMEDATA is deprecated, please use KSPe.IO.Hierarchy.GAMEDATA instead.")]
		public const string GAMEDATA = "GameData";
		[System.Obsolete("KSPe.IO.File.GAMEDATA is deprecated, please use KSPe.IO.Hierarchy.PLUGINDATA instead.")]
		public const string PLUGINDATA = "PluginData";                                // Writeable data on <KSP_ROOT>/PluginData/<plugin_name>/
		[System.Obsolete("KSPe.IO.File.GAMEDATA is deprecated, please use KSPe.IO.Hierarchy.LOCALDATA instead.")]
		public static string LOCALDATA => Path.Combine(GAMEDATA, "__LOCAL");      // Custom runtime generated parts on <KSP_ROO>/GameData/__LOCAL/<plugin_name> (specially made for UbioWeldingLtd)

		[System.Obsolete("KSPe.IO.File.CalculateKspPath is deprecated, please use KSPe.IO.Hierarchy.ROOT.Solve instead.")]
		public static string CalculateKspPath(string fname, params string[] fnames)
		{
			return Hierarchy.ROOT.Solve(fname, fnames);
		}

		[System.Obsolete("KSPe.IO.File.CalculateRelativePath is deprecated. There shuold be no need for this anymore.")]
		public static string CalculateRelativePath(string fullDestinationPath)
		{
			return Hierarchy.CalculateRelativePath(fullDestinationPath, Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)); //FIXME: This only works when KSPe is on the GameData/ !!
		}

		internal static string[] List(string rawdir, string mask = "*", bool include_subdirs = false)
		{
			if (!Directory.Exists(rawdir))
				throw new SIO.FileNotFoundException(rawdir);

			string[] files = SIO.Directory.GetFiles(
									rawdir,
									mask,
									include_subdirs ? SIO.SearchOption.AllDirectories : SIO.SearchOption.TopDirectoryOnly
								);
			files = files.OrderBy(x => x).ToArray();            // This will sort 1, 2, 10, 12 
//			Array.Sort(files, StringComparer.CurrentCulture);   // This will sort 1, 10, 12, 2

			for (int i = files.Length; --i >= 0;)
				files[i] = files[i].Substring(files[i].IndexOf(rawdir, StringComparison.Ordinal) + rawdir.Length);

			return files;
		}

		public static string[] List(Hierarchy hierarchy, string mask = "*", bool include_subdirs = false, string subdir = null)
		{
			return List(hierarchy.SolveFull(false, subdir ?? "."), mask, include_subdirs);
		}

		public static string[] List(Hierarchy hierarchy, string mask = "*", bool include_subdirs = false, string fn = null, params string[] fns)
		{
			return List(hierarchy.SolveFull(false, fn ?? ".", fns), mask, include_subdirs);
		}

		public static void AppendAllText(Hierarchy hierarchy, string path, string contents) { throw new NotImplementedException("KSPe.IO.File.AppendAllText"); }
		public static void AppendAllText(Hierarchy hierarchy, string contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.AppendAllText"); }
		public static void AppendAllText(Hierarchy hierarchy, string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.AppendAllText"); }
		public static void AppendAllText(Hierarchy hierarchy, string contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.AppendAllText"); }

		public static SIO.StreamWriter AppendText(Hierarchy hierarchy, string path) { throw new NotImplementedException("KSPe.IO.File.AppendText"); }
		public static SIO.StreamWriter AppendText(Hierarchy hierarchy, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.AppendText"); }

		public static void Copy(Hierarchy sourceHierarchy, string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Copy"); }
		public static void Copy(Hierarchy SourceHierarchy, string sourceFileName, Hierarchy destHierarchy, string destLocalFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.CopyToLocal"); }

		public static SIO.FileStream Create(Hierarchy hierarchy, string path) { throw new NotImplementedException("KSPe.IO.File.Create"); }
		public static SIO.FileStream Create(Hierarchy hierarchy, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Create"); }
		public static SIO.FileStream Create(Hierarchy hierarchy, string path, int bufferSize) { throw new NotImplementedException("KSPe.IO.File.Create"); }
		public static SIO.FileStream Create(Hierarchy hierarchy, int bufferSize, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Create"); }
		public static SIO.FileStream Create(Hierarchy hierarchy, string path, int bufferSize, SIO.FileOptions options) { throw new NotImplementedException("KSPe.IO.File.Create"); }
		public static SIO.FileStream Create(Hierarchy hierarchy, int bufferSize, SIO.FileOptions options, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Create"); }
		public static SIO.FileStream Create(Hierarchy hierarchy, string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Create"); }
		public static SIO.FileStream Create(Hierarchy hierarchy, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Create"); }

		public static SIO.StreamWriter CreateText(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(true, path);
			var t = SIO.File.CreateText(path);          // Does the magic
			t.Close();                                  // TODO: Get rid of this stunt.             
			return new SIO.StreamWriter(path);			// Reopens the stream as our own type.
		}

		public static SIO.StreamWriter CreateText(Hierarchy hierarchy, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(true, fn, fns);
			var t = SIO.File.CreateText(path);          // Does the magic
			t.Close();                                  // TODO: Get rid of this stunt.             
			return new SIO.StreamWriter(path);			// Reopens the stream as our own type.
		}

		public static void Decrypt(Hierarchy hierarchy, string path) { throw new NotImplementedException("KSPe.IO.File.Decrypt"); }
		public static void Decrypt(Hierarchy hierarchy, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Decrypt"); }

		public static void Delete(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			SIO.File.Delete(path);
		}

		public static void Delete(Hierarchy hierarchy, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			SIO.File.Delete(path);
		}

		public static void Encrypt(Hierarchy hierarchy, string path) { throw new NotImplementedException("KSPe.IO.File.Encrypt"); }
		public static void Encrypt(Hierarchy hierarchy, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Encrypt"); }

		public static bool Exists(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.Exists(path);
		}

		public static bool Exists(Hierarchy hierarchy, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			return SIO.File.Exists(path);
		}

		public static System.Security.AccessControl.FileSecurity GetAccessControl(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetAccessControl(path);
		}

		public static System.Security.AccessControl.FileSecurity GetAccessControl(Hierarchy hierarchy, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			return SIO.File.GetAccessControl(path);
		}

		public static System.Security.AccessControl.FileSecurity GetAccessControl(Hierarchy hierarchy, string path, System.Security.AccessControl.AccessControlSections includeSections)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetAccessControl(path, includeSections);
		}

		public static SIO.FileAttributes GetAttributes(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetAttributes(path);
		}

		public static DateTime GetCreationTime(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetCreationTime(path);
		}

		public static DateTime GetCreationTimeUtc(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetCreationTimeUtc(path);
		}

		public static DateTime GetLastAccessTime(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetLastAccessTime(path);
		}

		public static DateTime GetLastAccessTimeUtc(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetLastAccessTimeUtc(path);
		}

		public static DateTime GetLastWriteTime(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetLastWriteTime(path);
		}

		public static DateTime GetLastWriteTimeUtc(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.GetLastWriteTimeUtc(path);
		}

		public static void Move(Hierarchy hierarchy, string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Move"); }
		public static void Movel(Hierarchy sourceHierarchy, string sourceFileName, Hierarchy destHierarchy, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Move"); }
		public static void Replace(Hierarchy hierarchy, string sourceFileName, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException("KSPe.IO.File.Replace"); }
		public static void Replace(Hierarchy hierarchy, string sourceFileName, Hierarchy destHierarchy, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException("KSPe.IO.File.Replace"); }
		public static void Replace(Hierarchy hierarchy, string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException("KSPe.IO.File.Replace"); }
		public static void Replace(Hierarchy hierarchy, string sourceFileName, Hierarchy destHierarchy, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException("KSPe.IO.File.Replace"); }

		public static SIO.FileStream Open(Hierarchy hierarchy, string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Open"); }
		public static SIO.FileStream Open(Hierarchy hierarchy, SIO.FileMode mode, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Open"); }
		public static SIO.FileStream Open(Hierarchy hierarchy, string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Open"); }
		public static SIO.FileStream Open(Hierarchy hierarchy, SIO.FileMode mode, SIO.FileAccess access, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Open"); }
		public static SIO.FileStream Open(Hierarchy hierarchy, string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Open"); }
		public static SIO.FileStream Open(Hierarchy hierarchy, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Open"); }
		public static SIO.FileStream OpenRead(Hierarchy hierarchy, string path) { throw new NotImplementedException("KSPe.IO.File.OpenRead"); }
		public static SIO.FileStream OpenRead(Hierarchy hierarchy, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.OpenRead"); }
		public static SIO.StreamReader OpenText(Hierarchy hierarchy, string path) { throw new NotImplementedException("KSPe.IO.File.OpenText"); }
		public static SIO.StreamReader OpenText(Hierarchy hierarchy, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.OpenText"); }
		public static SIO.FileStream OpenWrite(Hierarchy hierarchy, string path) { throw new NotImplementedException("KSPe.IO.File.OpenWrite"); }
		public static SIO.FileStream OpenWrite(Hierarchy hierarchy, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.OpenWrite"); }

		public static byte[] ReadAllBytes(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.ReadAllBytes(path);
		}

		public static byte[] ReadAllBytes(Hierarchy hierarchy, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			return SIO.File.ReadAllBytes(path);
		}

		public static string[] ReadAllLines(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.ReadAllLines(path);
		}

		public static string[] ReadAllLines(Hierarchy hierarchy, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			return SIO.File.ReadAllLines(path);
		}

		public static string[] ReadAllLines(Hierarchy hierarchy, string path, System.Text.Encoding encoding)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.ReadAllLines(path, encoding);
		}

		public static string[] ReadAllLines(Hierarchy hierarchy, System.Text.Encoding encoding, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			return SIO.File.ReadAllLines(path, encoding);
		}

		public static string ReadAllText(Hierarchy hierarchy, string path)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.ReadAllText(path);
		}

		public static string ReadAllText(Hierarchy hierarchy, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			return SIO.File.ReadAllText(path);
		}

		public static string ReadAllText(Hierarchy hierarchy, string path, System.Text.Encoding encoding)
		{
			path = hierarchy.SolveFull(false, path);
			return SIO.File.ReadAllText(path, encoding);
		}

		public static string ReadAllText(Hierarchy hierarchy, System.Text.Encoding encoding, string fn, params string[] fns)
		{
			string path = hierarchy.SolveFull(false, fn, fns);
			return SIO.File.ReadAllText(path, encoding);
		}

		public static void SetAccessControl(Hierarchy hierarchy, string path, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.SetAttributes"); }
		public static void SetAccessControl(Hierarchy hierarchy, System.Security.AccessControl.FileSecurity fileSecurity, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetAttributes"); }

		public static void SetAttributes(Hierarchy hierarchy, string path, SIO.FileAttributes fileAttributes) { throw new NotImplementedException("KSPe.IO.File.SetAttributes"); }
		public static void SetAttributes(Hierarchy hierarchy, SIO.FileAttributes fileAttributes, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetAttributes"); }

		public static void SetCreationTime(Hierarchy hierarchy, string path, DateTime creationTime) { throw new NotImplementedException("KSPe.IO.File.SetCreationTime"); }
		public static void SetCreationTime(Hierarchy hierarchy, DateTime creationTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetCreationTime"); }
		public static void SetCreationTimeUtc(Hierarchy hierarchy, string path, DateTime creationTimeUtc) { throw new NotImplementedException("KSPe.IO.File.SetCreationTimeUtc"); }
		public static void SetCreationTimeUtc(Hierarchy hierarchy, DateTime creationTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetCreationTimeUtc"); }

		public static void SetLastAccessTime(Hierarchy hierarchy, string path, DateTime lastAccessTime) { throw new NotImplementedException("KSPe.IO.File.SetLastAccessTime"); }
		public static void SetLastAccessTime(Hierarchy hierarchy, DateTime lastAccessTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetLastAccessTime"); }
		public static void SetLastAccessTimeUtc(Hierarchy hierarchy, string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException("KSPe.IO.File.SetLastAccessTimeUtc"); }
		public static void SetLastAccessTimeUtc(Hierarchy hierarchy, DateTime lastAccessTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetLastAccessTimeUtc"); }

		public static void SetLastWriteTime(Hierarchy hierarchy, string path, DateTime lastWriteTime) { throw new NotImplementedException("KSPe.IO.File.SetLastWriteTime"); }
		public static void SetLastWriteTime(Hierarchy hierarchy, DateTime lastWriteTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetLastWriteTime"); }
		public static void SetLastWriteTimeUtc(Hierarchy hierarchy, string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException("KSPe.IO.File.SetLastWriteTimeUtc"); }
		public static void SetLastWriteTimeUtc(Hierarchy hierarchy, DateTime lastWriteTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.SetLastWriteTimeUtc"); }

		public static void WriteAllBytes(Hierarchy hierarchy, string path, byte[] bytes) { throw new NotImplementedException("KSPe.IO.File.WriteAllBytes"); }
		public static void WriteAllBytes(Hierarchy hierarchy, byte[] bytes, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.WriteAllBytes"); }

		public static void WriteAllLines(Hierarchy hierarchy, string path, string[] contents) { throw new NotImplementedException("KSPe.IO.File.WriteAllLines"); }
		public static void WriteAllLines(Hierarchy hierarchy, string[] contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.WriteAllLines"); }
		public static void WriteAllLines(Hierarchy hierarchy, string[] contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.WriteAllLines"); }
		public static void WriteAllLines(Hierarchy hierarchy, string path, string[] contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.WriteAllLines"); }

		public static void WriteAllText(Hierarchy hierarchy, string path, string contents) { throw new NotImplementedException("KSPe.IO.File.WriteAllText"); }
		public static void WriteAllText(Hierarchy hierarchy, string contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.WriteAllText"); }
		public static void WriteAllText(Hierarchy hierarchy, string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.WriteAllText"); }
		public static void WriteAllText(Hierarchy hierarchy, string contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.WriteAllText"); }

	}
}
