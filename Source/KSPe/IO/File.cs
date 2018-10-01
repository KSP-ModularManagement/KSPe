/*
    This file is part of KSPe, a component for KSP API Extensions/L
    (C) 2018 Lisias T : http://lisias.net <support@lisias.net>

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
using SIO = System.IO;

namespace KSPe.IO
{
	public static class File<T>
	{
		internal static string FullPathName(string filename, string subdir, string hierarchy, bool createDirs = false)
		{
			string fn = SIO.Path.Combine(KSPUtil.ApplicationRootPath, hierarchy);
			fn = SIO.Path.Combine(fn, subdir);
			fn = SIO.Path.Combine(fn, SIO.Path.GetFileName(filename));
			if (createDirs)
			{
				string d = System.IO.Path.GetDirectoryName(fn);
				if (!System.IO.Directory.Exists(d))
					System.IO.Directory.CreateDirectory(d);
			}
			return fn;
		}

		internal static string FullPathName(string filename, string hierarchy, bool createDirs = false)
		{
			Type target = typeof(T);
			string subdir = target.Namespace;
			{
				var t = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
						 from tt in assembly.GetTypes()
						 where tt.Name == "Version" && tt.GetMembers().Any(m => m.Name == "Vendor")
						 select tt).FirstOrDefault();

				if (null != t)
					subdir = SIO.Path.Combine(subdir, t.GetMember("Vendor").GetValue(0).ToString());
			}
			return FullPathName(filename, subdir, hierarchy, createDirs);
		}

		public static void AppendAllText(string path, string contents) { throw new NotImplementedException("KSPe.IO.File.AppendAllText"); }

		public static void AppendAllText(string path, string contents, System.Text.Encoding encoding)  { throw new NotImplementedException("KSPe.IO.File.AppendAllText"); }

		public static StreamWriter AppendText(string path)  { throw new NotImplementedException("KSPe.IO.File.AppendText"); }

		public static void Copy(string sourceFileName, string destFileName)  { throw new NotImplementedException("KSPe.IO.File.Copy"); }

		public static void Copy(string sourceFileName, string destFileName, bool overwrite)  { throw new NotImplementedException("KSPe.IO.File.Copy"); }

		public static FileStream Create(string path)  { throw new NotImplementedException("KSPe.IO.File.Create"); }

		public static FileStream Create(string path, int bufferSize)  { throw new NotImplementedException("KSPe.IO.File.Create"); }

		public static FileStream Create(string path, int bufferSize, SIO.FileOptions options)  { throw new NotImplementedException("KSPe.IO.File.Create"); }

		public static FileStream Create(string path, int bufferSize, SIO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity)  { throw new NotImplementedException("KSPe.IO.File.Create"); }

		public static StreamWriter CreateText(string path)  { throw new NotImplementedException("KSPe.IO.File.CreateText"); }

		public static void Decrypt(string path)  { throw new NotImplementedException("KSPe.IO.File.Decrypt"); }

		public static void Delete(string path)  { throw new NotImplementedException("KSPe.IO.File.Delete"); }

		public static void Encrypt(string path)  { throw new NotImplementedException("KSPe.IO.File.Encrypt"); }

		public static bool Exists(string path)  { throw new NotImplementedException("KSPe.IO.File.Exists"); }

		public static System.Security.AccessControl.FileSecurity GetAccessControl(string path)  { throw new NotImplementedException("KSPe.IO.File.GetAccessControl"); }

		public static System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections)  { throw new NotImplementedException("KSPe.IO.File.GetAccessControl"); }

		public static SIO.FileAttributes GetAttributes(string path)  { throw new NotImplementedException("KSPe.IO.File.GetAttributes"); }

		public static DateTime GetCreationTime(string path)  { throw new NotImplementedException("KSPe.IO.File.GetCreationTime"); }

		public static DateTime GetCreationTimeUtc(string path)  { throw new NotImplementedException("KSPe.IO.File.GetCreationTimeUtc"); }

		public static DateTime GetLastAccessTime(string path)  { throw new NotImplementedException("KSPe.IO.File.GetLastAccessTime"); }

		public static DateTime GetLastAccessTimeUtc(string path)  { throw new NotImplementedException("KSPe.IO.File.GetLastAccessTimeUtc"); }

		public static DateTime GetLastWriteTime(string path)  { throw new NotImplementedException("KSPe.IO.File.GetLastWriteTime"); }

		public static DateTime GetLastWriteTimeUtc(string path)  { throw new NotImplementedException("KSPe.IO.File.GetLastWriteTimeUtc"); }

		public static void Move(string sourceFileName, string destFileName)  { throw new NotImplementedException("KSPe.IO.File.Move"); }

		public static FileStream Open(string path, SIO.FileMode mode)  { throw new NotImplementedException("KSPe.IO.File.Open"); }

		public static FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access)  { throw new NotImplementedException("KSPe.IO.File.Open"); }

		public static FileStream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share)  { throw new NotImplementedException("KSPe.IO.File.Open"); }

		public static FileStream OpenRead(string path)  { throw new NotImplementedException("KSPe.IO.File.OpenRead"); }

		public static StreamReader OpenText(string path)  { throw new NotImplementedException("KSPe.IO.File.OpenText"); }

		public static FileStream OpenWrite(string path)  { throw new NotImplementedException("KSPe.IO.File.OpenWrite"); }

		public static byte[] ReadAllBytes(string path)  { throw new NotImplementedException("KSPe.IO.File.ReadAllBytes"); }

		public static string[] ReadAllLines(string path)  { throw new NotImplementedException("KSPe.IO.File.ReadAllLines"); }

		public static string[] ReadAllLines(string path, System.Text.Encoding encoding)  { throw new NotImplementedException("KSPe.IO.File.ReadAllLines"); }

		public static string ReadAllText(string path)  { throw new NotImplementedException("KSPe.IO.File.ReadAllText"); }

		public static string ReadAllText(string path, System.Text.Encoding encoding)  { throw new NotImplementedException("KSPe.IO.File.ReadAllText"); }

		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)  { throw new NotImplementedException("KSPe.IO.File.Replace"); }

		public static void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)  { throw new NotImplementedException("KSPe.IO.File.Replace"); }

		public static void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity)  { throw new NotImplementedException("KSPe.IO.File.SetAttributes"); }

		public static void SetAttributes(string path, SIO.FileAttributes fileAttributes)  { throw new NotImplementedException("KSPe.IO.File.SetAttributes"); }

		public static void SetCreationTime(string path, DateTime creationTime)  { throw new NotImplementedException("KSPe.IO.File.SetCreationTime"); }

		public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)  { throw new NotImplementedException("KSPe.IO.File.SetCreationTimeUtc"); }

		public static void SetLastAccessTime(string path, DateTime lastAccessTime)  { throw new NotImplementedException("KSPe.IO.File.SetLastAccessTime"); }

		public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)  { throw new NotImplementedException("KSPe.IO.File.SetLastAccessTimeUtc"); }

		public static void SetLastWriteTime(string path, DateTime lastWriteTime)  { throw new NotImplementedException("KSPe.IO.File.SetLastWriteTime"); }

		public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)  { throw new NotImplementedException("KSPe.IO.File.SetLastWriteTimeUtc"); }

		public static void WriteAllBytes(string path, byte[] bytes)  { throw new NotImplementedException("KSPe.IO.File.WriteAllBytes"); }

		public static void WriteAllLines(string path, string[] contents)  { throw new NotImplementedException("KSPe.IO.File.WriteAllLines"); }

		public static void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding)  { throw new NotImplementedException("KSPe.IO.File.WriteAllLines"); }

		public static void WriteAllText(string path, string contents)  { throw new NotImplementedException("KSPe.IO.File.WriteAllText"); }

		public static void WriteAllText(string path, string contents, System.Text.Encoding encoding)  { throw new NotImplementedException("KSPe.IO.File.WriteAllText"); }
	}
}
