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
using System.IO.IsolatedStorage;
using System.Reflection;

using SIO = System.IO;

namespace KSPe.IO
{
	public static class File<T>
	{
		public static readonly string[] ASSET = { "PluginData", "Assets" };     // ReadOnly data on <KSP_ROOT>/GameData/<plugin_name>/Plugin/{PluginData|Assets|null}/ or whatever the DLL is.
		private static readonly string RANDOM_TEMP_DIR = SIO.Path.GetRandomFileName();

		[System.Obsolete("KSPe.IO.File<T>.CalculateRelativePath is deprecated. There shuold be no need for this anymore.")]
		public static string CalculateRelativePath(string fullDestinationPath)
		{
			return Hierarchy<T>.CalculateRelativePath(fullDestinationPath, SIO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location)); //FIXME: This only works when KSPe is on the GameData/ !!
		}

		internal static string TempPathName(string filename = null)
		{
			filename = filename ?? SIO.Path.GetRandomFileName();
			if (!string.IsNullOrEmpty(SIO.Path.GetDirectoryName(filename)))
				throw new IsolatedStorageException(String.Format("filename cannot have subdirectories! [{0}]", filename));

			string fn = SIO.Path.GetTempPath();
			fn = SIO.Path.Combine(fn, "ksp");
			fn = SIO.Path.Combine(fn, RANDOM_TEMP_DIR);
			fn = SIO.Path.Combine(fn, Hierarchy<T>.CalculateTypeRoot());
			fn = SIO.Path.Combine(fn, SIO.Path.GetFileName(filename));
			{
				string d = SIO.Path.GetDirectoryName(fn);
				if (!SIO.Directory.Exists(d))
					SIO.Directory.CreateDirectory(d);
			}
			return SIO.Path.GetFullPath(fn);
		}

		public static class Asset
		{
			private static string solveRoot()
			{
				// Better coping with the current way of things

				{   // First, let's try the PluginData that should be in the same dir level that the DLL.
					string fn = SIO.Path.GetDirectoryName(typeof(T).Assembly.Location);
					for (int i = ASSET.Length;--i >= 0;)
					{
						string t = SIO.Path.Combine(fn, ASSET[i]);
						if (SIO.Directory.Exists(t))
							return t;
					}
				}

				{   // Just now we search for them on the KSPe cannonical hierarchy.
					for (int i = ASSET.Length;--i >= 0;)
					{
						string t = Hierarchy<T>.GAMEDATA.Solve(false, ASSET[i]);
						if (SIO.Directory.Exists(t))
							return t;
					}
				}
				throw new IsolatedStorageException(String.Format("Assembly {0} didn't resolved to a KSPe Asset location!", typeof(T).Assembly.GetName()));
			}

			internal static string SolveRoot()
			{
				LocalCache<string>.Dictionary c = Hierarchy<T>.CACHE[typeof(T)];
				return c.ContainsKey(Hierarchy.GAMEDATA.ToString()) ? c[Hierarchy.GAMEDATA.ToString()] : (c[Hierarchy.GAMEDATA.ToString()] = solveRoot());
			}

			private static string FullPathName(string partialPathname)
			{
				if (SIO.Path.IsPathRooted(partialPathname))
					throw new IsolatedStorageException(String.Format("partialPathname cannot be a full pathname! [{0}]", partialPathname));

				string fn = SIO.Path.Combine(SolveRoot(), partialPathname);
				fn = SIO.Path.GetFullPath(fn);
				return fn;
			}

			internal static string FullPathName(string fn, params string[] fns)
			{
				string path = fn;
				foreach (string s in fns)
					path = SIO.Path.Combine(fn, s);

				return FullPathName(path);
			}

			public static string Solve(string fn)
			{
				return FullPathName(fn).Replace(Hierarchy.ROOTPATH, "");
			}

			public static string Solve(string fn, params string[] fns)
			{
				return FullPathName(fn, fns).Replace(Hierarchy.ROOTPATH, "");
			}

			public static string Solve(LocalCache<string> cache, string fn)
			{
				LocalCache<string>.Dictionary c = cache[typeof(T)];
				return c.ContainsKey(fn) ? c[fn] : (c[fn] = Solve(fn));
			}

			public static string Solve(LocalCache<string> cache, string fn, params string[] fns)
			{
				LocalCache<string>.Dictionary c = cache[typeof(T)];
				string path = fn;
				foreach (string s in fns)
					path = SIO.Path.Combine(path, s);
				return c.ContainsKey(path) ? c[path] : (c[path] = Solve(path));
			}

			[System.Obsolete("KSPe.IO.File<T>.Asset.Solve(string, LocalCache) is deprecated, please use Solve(LocalCache, string) instead.")]
			public static string Solve(string fn, LocalCache<string> cache)
			{
				return Solve(cache, fn);
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string subdir = null)
			{
				return File.List(SIO.Path.Combine(SolveRoot(), subdir ?? "."), mask, include_subdirs);
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string fn = null, params string[] fns)
			{
				if (null == fn) return List(mask, include_subdirs);

				string subdir = Solve(fn, fns);
				return File.List(subdir, mask, include_subdirs);
			}

			public static void CopyToData(string sourceFileName, string destDataFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Asset.CopyToData"); }
			public static void CopyToLocal(string sourceFileName, string destLocalFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Asset.CopyToLocal"); }
			public static void CopyToTemp(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Asset.CopyToTemp"); }

			public static void Decrypt(string path) { throw new NotImplementedException("KSPe.IO.File.Asset.Decrypt"); }
			public static void Decrypt(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Asset.Decrypt"); }

			public static bool Exists(string path)
			{
				path = FullPathName(path);
				return SIO.File.Exists(path);
			}

			public static bool Exists(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.Exists(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetAccessControl(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetAccessControl(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
			{
				path = FullPathName(path);
				return SIO.File.GetAccessControl(path, includeSections);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(System.Security.AccessControl.AccessControlSections includeSections, string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetAccessControl(path, includeSections);
			}

			public static SIO.FileAttributes GetAttributes(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetAttributes(path);
			}

			public static SIO.FileAttributes GetAttributes(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetAttributes(path);
			}

			public static DateTime GetCreationTime(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetCreationTime(path);
			}

			public static DateTime GetCreationTime(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetCreationTime(path);
			}

			public static DateTime GetCreationTimeUtc(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetCreationTimeUtc(path);
			}

			public static DateTime GetCreationTimeUtc(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetCreationTimeUtc(path);
			}

			public static DateTime GetLastAccessTime(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastAccessTime(path);
			}

			public static DateTime GetLastAccessTime(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetLastAccessTime(path);
			}

			public static DateTime GetLastAccessTimeUtc(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastAccessTimeUtc(path);
			}

			public static DateTime GetLastAccessTimeUtc(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetLastAccessTimeUtc(path);
			}

			public static DateTime GetLastWriteTime(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastWriteTime(path);
			}

			public static DateTime GetLastWriteTime(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetLastWriteTime(path);
			}

			public static DateTime GetLastWriteTimeUtc(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastWriteTimeUtc(path);
			}

			public static DateTime GetLastWriteTimeUtc(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.GetLastWriteTimeUtc(path);
			}

			public static IO.Asset<T>.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset<T>.FileStream Open(SIO.FileMode mode, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset<T>.FileStream Open(SIO.FileMode mode, SIO.FileAccess access, string fn, string[] fns) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset<T>.FileStream Open(SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset<T>.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Asset.OpenRead"); }
			public static IO.Asset<T>.FileStream OpenRead(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Asset.OpenRead"); }
			public static IO.Asset<T>.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Asset.OpenText"); }
			public static IO.Asset<T>.StreamReader OpenText(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Asset.OpenText"); }

			public static byte[] ReadAllBytes(string path)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllBytes(path);
			}

			public static byte[] ReadAllBytes(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.ReadAllBytes(path);
			}

			public static string[] ReadAllLines(string path)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllLines(path);
			}

			public static string[] ReadAllLines(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.ReadAllLines(path);
			}

			public static string[] ReadAllLines(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllLines(path, encoding);
			}

			public static string[] ReadAllLines(System.Text.Encoding encoding, string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.ReadAllLines(path, encoding);
			}

			public static string ReadAllText(string path)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllText(path);
			}

			public static string ReadAllText(string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.ReadAllText(path);
			}

			public static string ReadAllText(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static string ReadAllText(System.Text.Encoding encoding, string fn, params string[] fns)
			{
				string path = FullPathName(fn, fns);
				return SIO.File.ReadAllText(path, encoding);
			}
		}

		public static class Data
		{
			internal static string FullPathName(bool createDirs, string fn, params string[] fns)
			{
				return Hierarchy<T>.PLUGINDATA.SolveFull(createDirs, fn, fns);
			}

			public static string Solve(string fn)
			{
				return Hierarchy<T>.PLUGINDATA.Solve(false, fn);
			}

			public static string Solve(string fn, params string[] fns)
			{
				return Hierarchy<T>.PLUGINDATA.Solve(false, fn, fns);
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string subdir = null)
			{
				return File.List(SIO.Path.Combine(FullPathName(false, "."), subdir??"."), mask, include_subdirs);
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string fn = null, params string[] fns)
			{
				if (null == fn) return List(mask, include_subdirs);

				string subdir = Solve(fn, fns);
				return File.List(subdir, mask, include_subdirs);
			}

			public static void AppendAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Data.AppendAllText"); }
			public static void AppendAllText(string contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.AppendAllText"); }
			public static void AppendAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Data.AppendAllText"); }
			public static void AppendAllText(string contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.AppendAllText"); }

			public static IO.Data<T>.StreamWriter AppendText(string path) { throw new NotImplementedException("KSPe.IO.File.Data.AppendText"); }
			public static IO.Data<T>.StreamWriter AppendText(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.AppendText"); }

			public static void Copy(string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Data.Copy"); }
			public static void CopyToLocal(string sourceFileName, string destLocalFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Data.CopyToLocal"); }
			public static void CopyToTemp(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Data.CopyToTemp"); }

			public static IO.Data<T>.FileStream Create(string path) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data<T>.FileStream Create(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data<T>.FileStream Create(string path, int bufferSize) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data<T>.FileStream Create(int bufferSize, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data<T>.FileStream Create(string path, int bufferSize, SIO.FileOptions options) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data<T>.FileStream Create(int bufferSize, SIO.FileOptions options, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data<T>.FileStream Create(string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data<T>.FileStream Create(int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }

			public static IO.Data<T>.StreamWriter CreateText(string path)
			{
				path = FullPathName(true, path);
				var t = SIO.File.CreateText(path);			// Does the magic
				t.Close();									// TODO: Get rid of this stunt.             
				return new IO.Data<T>.StreamWriter(path);	// Reopens the stream as our own type.
			}

			public static IO.Data<T>.StreamWriter CreateText(string fn, params string[] fns)
			{
				string path = FullPathName(true, fn, fns);
				var t = SIO.File.CreateText(path);			// Does the magic
				t.Close();									// TODO: Get rid of this stunt.             
				return new IO.Data<T>.StreamWriter(path);	// Reopens the stream as our own type.
			}

			public static void Decrypt(string path) { throw new NotImplementedException("KSPe.IO.File.Data.Decrypt"); }
			public static void Decrypt(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Decrypt"); }

			public static void Delete(string path)
			{
				path = FullPathName(false, path);
				SIO.File.Delete(path);
			}

			public static void Delete(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				SIO.File.Delete(path);
			}

			public static void Encrypt(string path)  { throw new NotImplementedException("KSPe.IO.File.Data.Encrypt"); }
			public static void Encrypt(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Encrypt"); }

			public static bool Exists(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.Exists(path);
			}

			public static bool Exists(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.Exists(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetAccessControl(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.GetAccessControl(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
			{
				path = FullPathName(false, path);
				return SIO.File.GetAccessControl(path, includeSections);
			}

			public static SIO.FileAttributes GetAttributes(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetAttributes(path);
			}

			public static DateTime GetCreationTime(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetCreationTime(path);
			}

			public static DateTime GetCreationTimeUtc(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetCreationTimeUtc(path);
			}

			public static DateTime GetLastAccessTime(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastAccessTime(path);
			}

			public static DateTime GetLastAccessTimeUtc(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastAccessTimeUtc(path);
			}

			public static DateTime GetLastWriteTime(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastWriteTime(path);
			}

			public static DateTime GetLastWriteTimeUtc(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastWriteTimeUtc(path);
			}

			public static void Move(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Data.Move"); }
			public static void MoveToLocal(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Data.Move"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException("KSPe.IO.File.Data.Replace"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException("KSPe.IO.File.Data.Replace"); }

			public static IO.Data<T>.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data<T>.FileStream Open(SIO.FileMode mode, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data<T>.FileStream Open(SIO.FileMode mode, SIO.FileAccess access, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data<T>.FileStream Open(SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data<T>.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Data.OpenRead"); }
			public static IO.Data<T>.FileStream OpenRead(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.OpenRead"); }
			public static IO.Data<T>.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Data.OpenText"); }
			public static IO.Data<T>.StreamReader OpenText(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.OpenText"); }
			public static IO.Data<T>.FileStream OpenWrite(string path) { throw new NotImplementedException("KSPe.IO.File.Data.OpenWrite"); }
			public static IO.Data<T>.FileStream OpenWrite(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.OpenWrite"); }

			public static byte[] ReadAllBytes(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllBytes(path);
			}

			public static byte[] ReadAllBytes(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllBytes(path);
			}

			public static string[] ReadAllLines(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllLines(path);
			}

			public static string[] ReadAllLines(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllLines(path);
			}

			public static string[] ReadAllLines(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllLines(path, encoding);
			}

			public static string[] ReadAllLines(System.Text.Encoding encoding, string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllLines(path, encoding);
			}

			public static string ReadAllText(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllText(path);
			}

			public static string ReadAllText(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllText(path);
			}

			public static string ReadAllText(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static string ReadAllText(System.Text.Encoding encoding, string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Data.SetAttributes"); }
			public static void SetAccessControl(System.Security.AccessControl.FileSecurity fileSecurity, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetAttributes"); }

			public static void SetAttributes(string path, SIO.FileAttributes fileAttributes) { throw new NotImplementedException("KSPe.IO.File.Data.SetAttributes"); }
			public static void SetAttributes(SIO.FileAttributes fileAttributes, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetAttributes"); }

			public static void SetCreationTime(string path, DateTime creationTime) { throw new NotImplementedException("KSPe.IO.File.Data.SetCreationTime"); }
			public static void SetCreationTime(DateTime creationTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetCreationTime"); }
			public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Data.SetCreationTimeUtc"); }
			public static void SetCreationTimeUtc(DateTime creationTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetCreationTimeUtc"); }

			public static void SetLastAccessTime(string path, DateTime lastAccessTime) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastAccessTime"); }
			public static void SetLastAccessTime(DateTime lastAccessTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastAccessTime"); }
			public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastAccessTimeUtc"); }
			public static void SetLastAccessTimeUtc(DateTime lastAccessTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastAccessTimeUtc"); }

			public static void SetLastWriteTime(string path, DateTime lastWriteTime) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastWriteTime"); }
			public static void SetLastWriteTime(DateTime lastWriteTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastWriteTime"); }
			public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastWriteTimeUtc"); }
			public static void SetLastWriteTimeUtc(DateTime lastWriteTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastWriteTimeUtc"); }

			public static void WriteAllBytes(string path, byte[] bytes) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllBytes"); }
			public static void WriteAllBytes(byte[] bytes, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllBytes"); }

			public static void WriteAllLines(string path, string[] contents) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllLines"); }
			public static void WriteAllLines(string[] contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllLines"); }
			public static void WriteAllLines(string[] contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllLines"); }
			public static void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllLines"); }

			public static void WriteAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllText"); }
			public static void WriteAllText(string contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllText"); }
			public static void WriteAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllText"); }
			public static void WriteAllText(string contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllText"); }
		}

		public static class Local
		{
			internal static string FullPathName(bool createDirs, string fn, params string[] fns)
			{
				return Hierarchy<T>.LOCALDATA.SolveFull(createDirs, fn, fns);
			}

			public static string Solve(string fn)
			{
				string r = Hierarchy<T>.LOCALDATA.SolveFull(false, fn);
				return r.Substring(r.IndexOf(Hierarchy.GAMEDATA.relativePathName + SIO.Path.DirectorySeparatorChar, StringComparison.Ordinal) + 1 + Hierarchy.GAMEDATA.relativePathName.Length);
			}

			public static string Solve(string fn, params string[] fns)
			{
				string r = Hierarchy<T>.LOCALDATA.SolveFull(false, fn);
				return r.Substring(r.IndexOf(Hierarchy.GAMEDATA.relativePathName + SIO.Path.DirectorySeparatorChar, StringComparison.Ordinal) + 1 + Hierarchy.GAMEDATA.relativePathName.Length);
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string subdir = null)
			{
				return File.List(SIO.Path.Combine(FullPathName(false, "."), subdir ?? "."), mask, include_subdirs);
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string fn = null, params string[] fns)
			{
				if (null == fn) return List(mask, include_subdirs);

				string subdir = Solve(fn, fns);
				return File.List(subdir, mask, include_subdirs);
			}

			public static void AppendAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Local.AppendAllText"); }
			public static void AppendAllText(string contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.AppendAllText"); }
			public static void AppendAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Local.AppendAllText"); }
			public static void AppendAllText(string contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.AppendAllText"); }

			public static IO.Local<T>.StreamWriter AppendText(string path) { throw new NotImplementedException("KSPe.IO.File.Local.AppendText"); }
			public static IO.Local<T>.StreamWriter AppendText(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.AppendText"); }

			public static void Copy(string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Local.Copy"); }
			public static void CopyToLocal(string sourceFileName, string destLocalFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Local.CopyToLocal"); }
			public static void CopyToTemp(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Local.CopyToTemp"); }

			public static IO.Local<T>.FileStream Create(string path) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }
			public static IO.Local<T>.FileStream Create(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }
			public static IO.Local<T>.FileStream Create(string path, int bufferSize) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }
			public static IO.Local<T>.FileStream Create(int bufferSize, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }
			public static IO.Local<T>.FileStream Create(string path, int bufferSize, SIO.FileOptions options) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }
			public static IO.Local<T>.FileStream Create(int bufferSize, SIO.FileOptions options, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }
			public static IO.Local<T>.FileStream Create(string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }
			public static IO.Local<T>.FileStream Create(int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Create"); }

			public static IO.Local<T>.StreamWriter CreateText(string path)
			{
				path = FullPathName(true, path);
				var t = SIO.File.CreateText(path);          // Does the magic
				t.Close();                                  // TODO: Get rid of this stunt.             
				return new IO.Local<T>.StreamWriter(path);  // Reopens the stream as our own type.
			}

			public static IO.Local<T>.StreamWriter CreateText(string fn, params string[] fns)
			{
				string path = FullPathName(true, fn, fns);
				var t = SIO.File.CreateText(path);          // Does the magic
				t.Close();                                  // TODO: Get rid of this stunt.             
				return new IO.Local<T>.StreamWriter(path);  // Reopens the stream as our own type.
			}

			public static void Decrypt(string path) { throw new NotImplementedException("KSPe.IO.File.Local.Decrypt"); }
			public static void Decrypt(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Decrypt"); }

			public static void Delete(string path)
			{
				path = FullPathName(false, path);
				SIO.File.Delete(path);
			}

			public static void Delete(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				SIO.File.Delete(path);
			}

			public static void Encrypt(string path) { throw new NotImplementedException("KSPe.IO.File.Local.Encrypt"); }
			public static void Encrypt(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Encrypt"); }

			public static bool Exists(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.Exists(path);
			}

			public static bool Exists(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.Exists(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetAccessControl(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.GetAccessControl(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
			{
				path = FullPathName(false, path);
				return SIO.File.GetAccessControl(path, includeSections);
			}

			public static SIO.FileAttributes GetAttributes(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetAttributes(path);
			}

			public static DateTime GetCreationTime(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetCreationTime(path);
			}

			public static DateTime GetCreationTimeUtc(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetCreationTimeUtc(path);
			}

			public static DateTime GetLastAccessTime(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastAccessTime(path);
			}

			public static DateTime GetLastAccessTimeUtc(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastAccessTimeUtc(path);
			}

			public static DateTime GetLastWriteTime(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastWriteTime(path);
			}

			public static DateTime GetLastWriteTimeUtc(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.GetLastWriteTimeUtc(path);
			}

			public static void Move(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Local.Move"); }
			public static void MoveToLocal(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Local.Move"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException("KSPe.IO.File.Local.Replace"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException("KSPe.IO.File.Local.Replace"); }

			public static IO.Local<T>.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local<T>.FileStream Open(SIO.FileMode mode, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local<T>.FileStream Open(SIO.FileMode mode, SIO.FileAccess access, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local<T>.FileStream Open(SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local<T>.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Local.OpenRead"); }
			public static IO.Local<T>.FileStream OpenRead(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.OpenRead"); }
			public static IO.Local<T>.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Local.OpenText"); }
			public static IO.Local<T>.StreamReader OpenText(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.OpenText"); }
			public static IO.Local<T>.FileStream OpenWrite(string path) { throw new NotImplementedException("KSPe.IO.File.Local.OpenWrite"); }
			public static IO.Local<T>.FileStream OpenWrite(string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.OpenWrite"); }

			public static byte[] ReadAllBytes(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllBytes(path);
			}

			public static byte[] ReadAllBytes(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllBytes(path);
			}

			public static string[] ReadAllLines(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllLines(path);
			}

			public static string[] ReadAllLines(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllLines(path);
			}

			public static string[] ReadAllLines(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllLines(path, encoding);
			}

			public static string[] ReadAllLines(System.Text.Encoding encoding, string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllLines(path, encoding);
			}

			public static string ReadAllText(string path)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllText(path);
			}

			public static string ReadAllText(string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllText(path);
			}

			public static string ReadAllText(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(false, path);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static string ReadAllText(System.Text.Encoding encoding, string fn, params string[] fns)
			{
				string path = FullPathName(false, fn, fns);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Local.SetAttributes"); }
			public static void SetAccessControl(System.Security.AccessControl.FileSecurity fileSecurity, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetAttributes"); }

			public static void SetAttributes(string path, SIO.FileAttributes fileAttributes) { throw new NotImplementedException("KSPe.IO.File.Local.SetAttributes"); }
			public static void SetAttributes(SIO.FileAttributes fileAttributes, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetAttributes"); }

			public static void SetCreationTime(string path, DateTime creationTime) { throw new NotImplementedException("KSPe.IO.File.Local.SetCreationTime"); }
			public static void SetCreationTime(DateTime creationTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetCreationTime"); }
			public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Local.SetCreationTimeUtc"); }
			public static void SetCreationTimeUtc(DateTime creationTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetCreationTimeUtc"); }

			public static void SetLastAccessTime(string path, DateTime lastAccessTime) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastAccessTime"); }
			public static void SetLastAccessTime(DateTime lastAccessTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastAccessTime"); }
			public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastAccessTimeUtc"); }
			public static void SetLastAccessTimeUtc(DateTime lastAccessTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastAccessTimeUtc"); }

			public static void SetLastWriteTime(string path, DateTime lastWriteTime) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastWriteTime"); }
			public static void SetLastWriteTime(DateTime lastWriteTime, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastWriteTime"); }
			public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastWriteTimeUtc"); }
			public static void SetLastWriteTimeUtc(DateTime lastWriteTimeUtc, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastWriteTimeUtc"); }

			public static void WriteAllBytes(string path, byte[] bytes) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllBytes"); }
			public static void WriteAllBytes(byte[] bytes, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllBytes"); }

			public static void WriteAllLines(string path, string[] contents) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllLines"); }
			public static void WriteAllLines(string[] contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllLines"); }
			public static void WriteAllLines(string[] contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllLines"); }
			public static void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllLines"); }

			public static void WriteAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllText"); }
			public static void WriteAllText(string contents, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllText"); }
			public static void WriteAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllText"); }
			public static void WriteAllText(string contents, System.Text.Encoding encoding, string fn, params string[] fns) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllText"); }
		}

		public static class Temp
		{
			internal static string FullPathName(string path)
			{
				return File<T>.TempPathName(path);
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string subdir = null)
			{
				return File.List(SIO.Path.Combine(FullPathName("."), subdir??"."), mask, include_subdirs);
			}

			public static void AppendAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Temp.AppendAllText"); }	
			public static void AppendAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Temp.AppendAllText"); }
			public static IO.Temp<T>.StreamWriter AppendText(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.AppendText"); }
			public static void Copy(string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Temp.Copy"); }
			public static void CopyToData(string sourceFileName, string destDataFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Temp.CopyToData"); }
			public static void CopyToLocal(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Temp.CopyToTemp"); }
			public static IO.Temp<T>.FileStream Create(string path) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Temp<T>.FileStream Create(string path, int bufferSize) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Temp<T>.FileStream Create(string path, int bufferSize, SIO.FileOptions options) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Temp<T>.FileStream Create(string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Create"); }

			public static IO.Temp<T>.StreamWriter CreateText(string path)
			{
				path = FullPathName(path);
				var t = SIO.File.CreateText(path);      // Does the magic
				t.Close();                              // TODO: Get rid of this stunt.             
				return new IO.Temp<T>.StreamWriter(path);  // Reopens the stream as our own type.
			}
			
			public static void Decrypt(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.Decrypt"); }

			public static void Delete(string path)
			{
				path = FullPathName(path);
				SIO.File.Delete(path);
			}

			public static void Encrypt(string path)  { throw new NotImplementedException("KSPe.IO.File.Temp.Encrypt"); }
			
			public static bool Exists(string path)
			{
				path = FullPathName(path);
				return SIO.File.Exists(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetAccessControl(path);
			}
			
			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
			{
				path = FullPathName(path);
				return SIO.File.GetAccessControl(path, includeSections);
			}

			public static SIO.FileAttributes GetAttributes(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetAttributes(path);
			}

			public static DateTime GetCreationTime(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetCreationTime(path);
			}

			public static DateTime GetCreationTimeUtc(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetCreationTimeUtc(path);
			}

			public static DateTime GetLastAccessTime(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastAccessTime(path);
			}

			public static DateTime GetLastAccessTimeUtc(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastAccessTimeUtc(path);
			}

			public static DateTime GetLastWriteTime(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastWriteTime(path);
			}

			public static DateTime GetLastWriteTimeUtc(string path)
			{
				path = FullPathName(path);
				return SIO.File.GetLastWriteTimeUtc(path);
			}

			public static void MoveToData(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.Temp.MoveToData"); }
			public static void MoveToLocal(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.Temp.MoveToLocal"); }
			public static IO.Temp<T>.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Temp.Open"); }
			public static IO.Temp<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Temp.Open"); }
			public static IO.Temp<T>.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Temp.Open"); }
			public static IO.Temp<T>.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.OpenRead"); }
			public static IO.Temp<T>.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.OpenText"); }
			public static IO.Temp<T>.FileStream OpenWrite(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.OpenWrite"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException("KSPe.IO.File.Temp.Replace"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException("KSPe.IO.File.Temp.Replace"); }

			public static byte[] ReadAllBytes(string path)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllBytes(path);
			}

			public static string[] ReadAllLines(string path)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllLines(path);
			}

			public static string[] ReadAllLines(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllLines(path, encoding);
			}

			public static string ReadAllText(string path)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllText(path);
			}

			public static string ReadAllText(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Temp.SetAttributes"); }
			public static void SetAttributes(string path, SIO.FileAttributes fileAttributes) { throw new NotImplementedException("KSPe.IO.File.Temp.SetAttributes"); }
			public static void SetCreationTime(string path, DateTime creationTime) { throw new NotImplementedException("KSPe.IO.File.Temp.SetCreationTime"); }
			public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Temp.SetCreationTimeUtc"); }
			public static void SetLastAccessTime(string path, DateTime lastAccessTime) { throw new NotImplementedException("KSPe.IO.File.Temp.SetLastAccessTime"); }
			public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Temp.SetLastAccessTimeUtc"); }
			public static void SetLastWriteTime(string path, DateTime lastWriteTime) { throw new NotImplementedException("KSPe.IO.File.Temp.SetLastWriteTime"); }
			public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Temp.SetLastWriteTimeUtc"); }
			public static void WriteAllBytes(string path, byte[] bytes) { throw new NotImplementedException("KSPe.IO.File.Temp.WriteAllBytes"); }
			public static void WriteAllLines(string path, string[] contents) { throw new NotImplementedException("KSPe.IO.File.Temp.WriteAllLines"); }
			public static void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Temp.WriteAllLines"); }
			public static void WriteAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Temp.WriteAllText"); }
			public static void WriteAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Temp.WriteAllText"); }
		}
	}
}
