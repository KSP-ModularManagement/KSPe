/*
    This file is part of KSPe, a component for KSP API Extensions/L
    (C) 2018-19 Lisias T : http://lisias.net <support@lisias.net>

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
using System.IO.IsolatedStorage;

using SIO = System.IO;
using System.Reflection;

namespace KSPe.IO
{
	public static class File
    {
		internal static readonly string KSP_ROOTPATH = SIO.Path.GetFullPath(KSPUtil.ApplicationRootPath);
        public const string GAMEDATA = "GameData";
		public const string PLUGINDATA = "PluginData";                                // Writeable data on <KSP_ROOT>/PluginData/<plugin_name>/
		public const string LOCALDATA = "GameData/__LOCAL";                           // Custom runtime generated parts on <KSP_ROO>/GameData/__LOCAL/<plugin_name> (specially made for UbioWeldingLtd)

        public static string CalculateRelativePath(string fullDestinationPath)
        {
			return CalculateRelativePath(fullDestinationPath, SIO.Path.GetDirectoryName(Assembly.GetCallingAssembly().Location));
        }

		internal static string CalculateRelativePath(string fullDestinationPath, string rootPath)
        {
#if DEBUG
            UnityEngine.Debug.Log(fullDestinationPath);
            UnityEngine.Debug.Log(rootPath);
#endif
            // from https://social.msdn.microsoft.com/Forums/vstudio/en-US/954346c8-cbe8-448c-80d0-d3fc27796e9c - Wednesday, May 20, 2009 3:37 PM
            string[] startPathParts = SIO.Path.GetFullPath(rootPath).Trim(SIO.Path.DirectorySeparatorChar).Split(SIO.Path.DirectorySeparatorChar);
            string[] destinationPathParts = SIO.Path.GetFullPath(fullDestinationPath).Trim(SIO.Path.DirectorySeparatorChar).Split(SIO.Path.DirectorySeparatorChar);

            int i = 0; // Finds the first difference on both paths (if any)
            int max = Math.Min(startPathParts.Length, destinationPathParts.Length);
            while ((i < max) && startPathParts[i].Equals(destinationPathParts[i], StringComparison.Ordinal))
                ++i;

            if (0 == i) return fullDestinationPath;

            System.Text.StringBuilder relativePath = new System.Text.StringBuilder();
            
            if (i >= startPathParts.Length)
                relativePath.Append(".").Append(SIO.Path.DirectorySeparatorChar); // Just for the LULZ.
            else
	            for (int j = i; j < startPathParts.Length; j++) // Adds how many ".." as necessary
	                relativePath.Append("..").Append(SIO.Path.DirectorySeparatorChar);

            for (int j = i; j < destinationPathParts.Length; j++) // And now feeds the remaning directories
                relativePath.Append(destinationPathParts[j]).Append(SIO.Path.DirectorySeparatorChar);

            relativePath.Length--; // Gets rid of the trailig "/" that is always appended

#if DEBUG
            UnityEngine.Debug.Log(relativePath.ToString());
#endif
            return relativePath.ToString();
        }
        
		internal static string FullPathName(string partialPathname, string rootDir, string hierarchy, bool createDirs = false)
		{
			if (SIO.Path.IsPathRooted(partialPathname))
				throw new IsolatedStorageException(String.Format("partialPathname cannot be a full pathname! [{0}]", partialPathname));

			string fn = SIO.Path.Combine(KSP_ROOTPATH, hierarchy);
			rootDir = SIO.Path.GetFullPath(SIO.Path.Combine(fn, rootDir));
			fn = SIO.Path.GetFullPath(SIO.Path.Combine(rootDir, partialPathname));

			if (!fn.StartsWith(rootDir, StringComparison.Ordinal))
				throw new IsolatedStorageException(String.Format("partialPathname cannot have relative paths leading outside the designed file hierarchy! [{0}]", partialPathname));

			if (createDirs)
			{
				string d = System.IO.Path.GetDirectoryName(fn);
				if (!System.IO.Directory.Exists(d))
					System.IO.Directory.CreateDirectory(d);
			}
			return fn;
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
//          Array.Sort(files, StringComparer.CurrentCulture);   // This will sort 1, 10, 12, 2
			
			for (int i = files.Length; --i >= 0;)
				files[i] = files[i].Substring(files[i].IndexOf(rawdir, StringComparison.Ordinal) + rawdir.Length + 1); // +1 to get rid of the trailling "/"

	        return files;
		}
	}
	
    public static class File<T>
	{
#pragma warning disable RECS0108 // Warns about static fields in generic types
        public static readonly string[] ASSET = { "PluginData", "Assets" };     // ReadOnly data on <KSP_ROOT>/GameData/<plugin_name>/Plugin/{PluginData|Assets|null}/ or whatever the DLL is.
#pragma warning restore RECS0108 // Warns about static fields in generic types

        public static string CalculateRelativePath(string fullDestinationPath)
        {
			return File.CalculateRelativePath(fullDestinationPath, SIO.Path.GetDirectoryName(typeof(T).Assembly.Location));
        }

        internal static string TempPathName(string filename)
		{
			if (!string.IsNullOrEmpty(SIO.Path.GetDirectoryName(filename)))
				throw new IsolatedStorageException(String.Format("filename cannot have subdirectories! [{0}]", filename));

			string fn = SIO.Path.GetTempPath();
			fn = SIO.Path.Combine(fn, "ksp");
			fn = SIO.Path.Combine(fn, CalculateRoot());
			fn = SIO.Path.Combine(fn, SIO.Path.GetFileName(filename));
			{
				string d = SIO.Path.GetDirectoryName(fn);
				if (!SIO.Directory.Exists(d))
					SIO.Directory.CreateDirectory(d);
			}
			return SIO.Path.GetFullPath(fn);
		}

#pragma warning disable RECS0108 // Warns about static fields in generic types
        private static readonly LocalCache<string> HIERARCHY_CACHE = new LocalCache<string>();
#pragma warning restore RECS0108 // Warns about static fields in generic types
        private static string calculateRoot()
        {
			string rootDir = typeof(T).Namespace;
			{
                Type t = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
						 from tt in assembly.GetTypes()
						 where tt.Namespace == typeof(T).Namespace && tt.Name == "Version" && tt.GetMembers().Any(m => m.Name == "Namespace")
						 select tt).FirstOrDefault();

				rootDir = (null == t)
					? rootDir
					: t.GetField("Namespace").GetValue(null).ToString();
			}
			{
                Type t = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
						 from tt in assembly.GetTypes()
						 where tt.Namespace == typeof(T).Namespace && tt.Name == "Version" && tt.GetMembers().Any(m => m.Name == "Vendor")
						 select tt).FirstOrDefault();

				rootDir = (null == t)
					? rootDir
					: SIO.Path.Combine(t.GetField("Vendor").GetValue(null).ToString(), rootDir);
			}
			return rootDir;
        }
        internal static string CalculateRoot()
        {
            LocalCache<string>.Dictionary c = HIERARCHY_CACHE[typeof(T)];
            return c.ContainsKey(".") ? c["."] : (c["."] = calculateRoot()) ;
        }

        internal static string FullPathName(string partialPathname, string hierarchy, bool createDirs = false)
		{
			string rootDir = CalculateRoot();
			return File.FullPathName(partialPathname, rootDir, hierarchy, createDirs);
		}

		public static class Asset
		{
            private static string solveRoot()
			{
                {
					for (int i = ASSET.Length; --i >= 0;)
					{
						string t = File<T>.FullPathName(ASSET[i], File.GAMEDATA);
						if (SIO.Directory.Exists(t))
							return t;
					}
                }
                {
	                string fn = SIO.Path.GetDirectoryName(typeof(T).Assembly.Location);
					for (int i = ASSET.Length; --i >= 0;)
					{
						string t = SIO.Path.Combine(fn, ASSET[i]);
						if (SIO.Directory.Exists(t))
							return t;
					}
                }
				throw new IsolatedStorageException(String.Format("Assembly {0} doesn't resolved to a KSPe Asset location!", typeof(T).Assembly.FullName));
			}

            internal static string SolveRoot()
            {
	            LocalCache<string>.Dictionary c = HIERARCHY_CACHE[typeof(T)];
	            return c.ContainsKey(File.GAMEDATA) ? c[File.GAMEDATA] : (c[File.GAMEDATA] = solveRoot()) ;
            }

#pragma warning disable RECS0146 // Member hides static member from outer class
            internal static string FullPathName(string partialPathname)
            {
                if (SIO.Path.IsPathRooted(partialPathname))
					throw new IsolatedStorageException(String.Format("partialPathname cannot be a full pathname! [{0}]", partialPathname));

				string fn = SIO.Path.Combine(SolveRoot(), partialPathname);
				fn = SIO.Path.GetFullPath(fn);
				return fn;
			}
#pragma warning restore RECS0146 // Member hides static member from outer class
			
			public static string Solve(string fn)
			{
				return FullPathName(fn).Replace(File.KSP_ROOTPATH, "");
			}
			
			public static string Solve(string fn, LocalCache<string> cache)
			{
	            LocalCache<string>.Dictionary c = cache[typeof(T)];
	            return c.ContainsKey(fn) ? c[fn] : (c[fn] = Solve(fn)) ;
			}
			
			public static string[] List(string mask = "*", bool include_subdirs = false, string subdir = null)
			{
				return File.List(SIO.Path.Combine(SolveRoot(), subdir??"."), mask, include_subdirs);
			}

			public static void CopyToData(string sourceFileName, string destDataFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Asset.CopyToData"); }
			public static void CopyToLocal(string sourceFileName, string destLocalFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Asset.CopyToLocal"); }
			public static void CopyToTemp(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Asset.CopyToTemp"); }

			public static void Decrypt(string path) { throw new NotImplementedException("KSPe.IO.File.Asset.Decrypt"); }
	
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

			public static IO.Asset.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Asset.Open"); }
			public static IO.Asset.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Asset.OpenRead"); }
			public static IO.Asset.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Asset.OpenText"); }

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
		}
		
		public static class Data
		{
#pragma warning disable RECS0146 // Member hides static member from outer class
            internal static string FullPathName(string path, bool createDirs)
            {
                return File<T>.FullPathName(path, File.PLUGINDATA, createDirs);
			}
#pragma warning restore RECS0146 // Member hides static member from outer class
			
			public static string Solve(string fn)
			{
				return FullPathName(fn, false).Replace(File.KSP_ROOTPATH, "");
			}

			public static string Solve(string fn, LocalCache<string> cache)
			{
	            LocalCache<string>.Dictionary c = cache[typeof(T)];
	            return c.ContainsKey(fn) ? c[fn] : (c[fn] = Solve(fn)) ;
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string subdir = null)
			{
				return File.List(SIO.Path.Combine(FullPathName(".", false), subdir??"."), mask, include_subdirs);
			}

			public static void AppendAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Data.AppendAllText"); }
			public static void AppendAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Data.AppendAllText"); }
			public static IO.Data.StreamWriter AppendText(string path) { throw new NotImplementedException("KSPe.IO.File.Data.AppendText"); }
			public static void Copy(string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Data.Copy"); }
			public static void CopyToLocal(string sourceFileName, string destLocalFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Data.CopyToLocal"); }
			public static void CopyToTemp(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Data.CopyToTemp"); }
			public static IO.Data.FileStream Create(string path) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data.FileStream Create(string path, int bufferSize) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data.FileStream Create(string path, int bufferSize, SIO.FileOptions options) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }
			public static IO.Data.FileStream Create(string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Data.Create"); }

			public static IO.Data.StreamWriter CreateText(string path)
			{
				path = FullPathName(path, true);
				var t = SIO.File.CreateText(path);      // Does the magic
				t.Close();                              // TODO: Get rid of this stunt.             
				return new IO.Data.StreamWriter(path);  // Reopens the stream as our own type.
			}
			
			public static void Decrypt(string path) { throw new NotImplementedException("KSPe.IO.File.Data.Decrypt"); }

			public static void Delete(string path)
			{
				path = FullPathName(path, false);
				SIO.File.Delete(path);
			}
	
			public static void Encrypt(string path)  { throw new NotImplementedException("KSPe.IO.File.Data.Encrypt"); }

			public static bool Exists(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.Exists(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetAccessControl(path);
			}
			
			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
			{
				path = FullPathName(path, false);
				return SIO.File.GetAccessControl(path, includeSections);
			}

			public static SIO.FileAttributes GetAttributes(string path)
						{
				path = FullPathName(path, false);
				return SIO.File.GetAttributes(path);
			}

			public static DateTime GetCreationTime(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetCreationTime(path);
			}

			public static DateTime GetCreationTimeUtc(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetCreationTimeUtc(path);
			}

			public static DateTime GetLastAccessTime(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastAccessTime(path);
			}

			public static DateTime GetLastAccessTimeUtc(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastAccessTimeUtc(path);
			}

			public static DateTime GetLastWriteTime(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastWriteTime(path);
			}

			public static DateTime GetLastWriteTimeUtc(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastWriteTimeUtc(path);
			}

			public static void Move(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Data.Move"); }
			public static void MoveToLocal(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.File.Data.Move"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException("KSPe.IO.File.Data.Replace"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException("KSPe.IO.File.Data.Replace"); }
			public static IO.Data.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Data.Open"); }
			public static IO.Data.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Data.OpenRead"); }
			public static IO.Data.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Data.OpenText"); }
			public static IO.Data.FileStream OpenWrite(string path) { throw new NotImplementedException("KSPe.IO.File.Data.OpenWrite"); }

			public static byte[] ReadAllBytes(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllBytes(path);
			}
			
			public static string[] ReadAllLines(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllLines(path);
			}
			
			public static string[] ReadAllLines(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllLines(path, encoding);
			}
			
			public static string ReadAllText(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllText(path);
			}
			
			public static string ReadAllText(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Data.SetAttributes"); }
			public static void SetAttributes(string path, SIO.FileAttributes fileAttributes) { throw new NotImplementedException("KSPe.IO.File.Data.SetAttributes"); }
			public static void SetCreationTime(string path, DateTime creationTime) { throw new NotImplementedException("KSPe.IO.File.Data.SetCreationTime"); }
			public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Data.SetCreationTimeUtc"); }
			public static void SetLastAccessTime(string path, DateTime lastAccessTime) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastAccessTime"); }
			public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastAccessTimeUtc"); }
			public static void SetLastWriteTime(string path, DateTime lastWriteTime) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastWriteTime"); }
			public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Data.SetLastWriteTimeUtc"); }
			public static void WriteAllBytes(string path, byte[] bytes) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllBytes"); }
			public static void WriteAllLines(string path, string[] contents) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllLines"); }
			public static void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllLines"); }
			public static void WriteAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllText"); }
			public static void WriteAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Data.WriteAllText"); }
		}
			
		public static class Local
		{
#pragma warning disable RECS0146 // Member hides static member from outer class
            internal static string FullPathName(string path, bool createDirs)
            {
                return File<T>.FullPathName(path, File.LOCALDATA, createDirs);
			}
#pragma warning restore RECS0146 // Member hides static member from outer class

			public static string Solve(string fn)
			{
				string r = FullPathName(fn, false).Replace(File.KSP_ROOTPATH, "");
				return r.Substring(r.IndexOf("GameData/", StringComparison.Ordinal) + 9);
			}
	        
			public static string Solve(string fn, LocalCache<string> cache)
			{
	            LocalCache<string>.Dictionary c = cache[typeof(T)];
	            return c.ContainsKey(fn) ? c[fn] : (c[fn] = Solve(fn)) ;
			}

			public static string[] List(string mask = "*", bool include_subdirs = false, string subdir = null)
			{
				return File.List(SIO.Path.Combine(FullPathName(".", false), subdir??"."), mask, include_subdirs);
			}

			public static void AppendAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Local.AppendAllText"); }	
			public static void AppendAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Local.AppendAllText"); }
			public static IO.Local.StreamWriter AppendText(string path) { throw new NotImplementedException("KSPe.IO.File.Local.AppendText"); }
			public static void Copy(string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Local.Copy"); }
			public static void CopyToData(string sourceFileName, string destDataFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Local.CopyToData"); }
			public static void CopyToTemp(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Local.CopyToTemp"); }
			public static IO.Local.FileStream Create(string path) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Local.FileStream Create(string path, int bufferSize) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Local.FileStream Create(string path, int bufferSize, SIO.FileOptions options) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Local.FileStream Create(string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Create"); }

			public static IO.Local.StreamWriter CreateText(string path)
			{
				path = FullPathName(path, true);
				var t = SIO.File.CreateText(path);      // Does the magic
				t.Close();                              // TODO: Get rid of this stunt.             
				return new IO.Local.StreamWriter(path);  // Reopens the stream as our own type.
			}
			
			public static void Decrypt(string path)  { throw new NotImplementedException("KSPe.IO.File.Local.Decrypt"); }

			public static void Delete(string path)
			{
				path = FullPathName(path, false);
				SIO.File.Delete(path);
			}

			public static void Encrypt(string path)  { throw new NotImplementedException("KSPe.IO.File.Local.Encrypt"); }
			
			public static bool Exists(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.Exists(path);
			}

			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetAccessControl(path);
			}
			
			public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)
			{
				path = FullPathName(path, false);
				return SIO.File.GetAccessControl(path, includeSections);
			}

			public static SIO.FileAttributes GetAttributes(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetAttributes(path);
			}

			public static DateTime GetCreationTime(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetCreationTime(path);
			}

			public static DateTime GetCreationTimeUtc(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetCreationTimeUtc(path);
			}

			public static DateTime GetLastAccessTime(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastAccessTime(path);
			}

			public static DateTime GetLastAccessTimeUtc(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastAccessTimeUtc(path);
			}

			public static DateTime GetLastWriteTime(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastWriteTime(path);
			}

			public static DateTime GetLastWriteTimeUtc(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.GetLastWriteTimeUtc(path);
			}
			
			public static void Move(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.Local.Move"); }
			public static void MoveToData(string sourceFileName, string destFileName) { throw new NotImplementedException("KSPe.IO.Local.Move"); }
			public static IO.Local.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Local.Open"); }
			public static IO.Local.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Local.OpenRead"); }
			public static IO.Local.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Local.OpenText"); }
			public static IO.Local.FileStream OpenWrite(string path) { throw new NotImplementedException("KSPe.IO.File.Local.OpenWrite"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) { throw new NotImplementedException("KSPe.IO.File.Local.Replace"); }
			public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) { throw new NotImplementedException("KSPe.IO.File.Local.Replace"); }

			public static byte[] ReadAllBytes(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllBytes(path);
			}
			
			public static string[] ReadAllLines(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllLines(path);
			}
			
			public static string[] ReadAllLines(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllLines(path, encoding);
			}
			
			public static string ReadAllText(string path)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllText(path);
			}
			
			public static string ReadAllText(string path, System.Text.Encoding encoding)
			{
				path = FullPathName(path, false);
				return SIO.File.ReadAllText(path, encoding);
			}

			public static void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Local.SetAttributes"); }
			public static void SetAttributes(string path, SIO.FileAttributes fileAttributes) { throw new NotImplementedException("KSPe.IO.File.Local.SetAttributes"); }
			public static void SetCreationTime(string path, DateTime creationTime) { throw new NotImplementedException("KSPe.IO.File.Local.SetCreationTime"); }
			public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Local.SetCreationTimeUtc"); }
			public static void SetLastAccessTime(string path, DateTime lastAccessTime) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastAccessTime"); }
			public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastAccessTimeUtc"); }
			public static void SetLastWriteTime(string path, DateTime lastWriteTime) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastWriteTime"); }
			public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) { throw new NotImplementedException("KSPe.IO.File.Local.SetLastWriteTimeUtc"); }
			public static void WriteAllBytes(string path, byte[] bytes) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllBytes"); }
			public static void WriteAllLines(string path, string[] contents) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllLines"); }
			public static void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllLines"); }
			public static void WriteAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllText"); }
			public static void WriteAllText(string path, string contents, System.Text.Encoding encoding) { throw new NotImplementedException("KSPe.IO.File.Local.WriteAllText"); }
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
			public static IO.Temp.StreamWriter AppendText(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.AppendText"); }
			public static void Copy(string sourceFileName, string destFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Temp.Copy"); }
			public static void CopyToData(string sourceFileName, string destDataFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Temp.CopyToData"); }
			public static void CopyToLocal(string sourceFileName, string destTempFileName, bool overwrite) { throw new NotImplementedException("KSPe.IO.File.Temp.CopyToTemp"); }
			public static IO.Temp.FileStream Create(string path) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Temp.FileStream Create(string path, int bufferSize) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Temp.FileStream Create(string path, int bufferSize, SIO.FileOptions options) { throw new NotImplementedException("KSPe.IO.File.Create"); }
			public static IO.Temp.FileStream Create(string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity) { throw new NotImplementedException("KSPe.IO.File.Create"); }

			public static IO.Temp.StreamWriter CreateText(string path)
			{
				path = FullPathName(path);
				var t = SIO.File.CreateText(path);      // Does the magic
				t.Close();                              // TODO: Get rid of this stunt.             
				return new IO.Temp.StreamWriter(path);  // Reopens the stream as our own type.
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
			public static IO.Temp.FileStream Open(string path, SIO.FileMode mode) { throw new NotImplementedException("KSPe.IO.File.Temp.Open"); }
			public static IO.Temp.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access) { throw new NotImplementedException("KSPe.IO.File.Temp.Open"); }
			public static IO.Temp.FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) { throw new NotImplementedException("KSPe.IO.File.Temp.Open"); }
			public static IO.Temp.FileStream OpenRead(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.OpenRead"); }
			public static IO.Temp.StreamReader OpenText(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.OpenText"); }
			public static IO.Temp.FileStream OpenWrite(string path) { throw new NotImplementedException("KSPe.IO.File.Temp.OpenWrite"); }
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
