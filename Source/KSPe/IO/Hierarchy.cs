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

using SIO = System.IO;

namespace KSPe.IO
{
	public class Hierarchy
	{
		internal static readonly string ROOTPATH = SIO.Path.GetFullPath(KSPUtil.ApplicationRootPath);

		public static Hierarchy ROOT = new Hierarchy("ROOT", ".");
		public static Hierarchy GAMEDATA = new Hierarchy("GAMEDATA", "GameData");
		public static Hierarchy PLUGINDATA = new Hierarchy("PLUGINDATA", "PluginData");
		public static Hierarchy LOCALDATA = new Hierarchy("LOCALDATA", SIO.Path.Combine(GAMEDATA.dirName, "__LOCAL"));
		public static Hierarchy SCREENSHOT = new Hierarchy("SCREENSHOT", "Screenshots");
		public static Hierarchy SAVE = new Hierarchy("SAVE", "saves");
		public static Hierarchy THUMB = new Hierarchy("THUMB", "thumbs");

		private readonly string name;
		internal readonly string dirName;
		internal readonly string fullPathName;
		internal readonly string relativePathName;
		private Hierarchy(string name, string dirName)
		{
			this.name = name;
			this.dirName = dirName;
			this.fullPathName = SIO.Path.Combine(ROOTPATH, dirName);
			this.relativePathName = this.fullPathName.Replace(ROOTPATH, "");
		}
		protected Hierarchy(Hierarchy hierarchy)
		{
			this.name = hierarchy.name;
			this.dirName = hierarchy.dirName;
			this.fullPathName = hierarchy.fullPathName;
			this.relativePathName = hierarchy.relativePathName;
		}

		new public String ToString() { return this.name; }

		public string Solve()
		{
			return this.relativePathName;
		}

		public string Solve(string fname, params string[] fnames)
		{
			return this.Solve(false, fname, fnames);
		}

		public string Solve(bool createDirs, string fname, params string[] fnames)
		{
			string combinedFnames = SIO.Path.Combine(this.relativePathName, fname);
			foreach (string s in fnames)
				combinedFnames = SIO.Path.Combine(combinedFnames, s);

			string partialPathName;
			string fullPathName;

			this.Calculate(createDirs, combinedFnames, out partialPathName, out fullPathName);

			return partialPathName;
		}

		internal string SolveFull(bool createDirs, string fname, params string[] fnames)
		{
			string combinedFnames = SIO.Path.Combine(this.relativePathName, fname);
			foreach (string s in fnames)
				combinedFnames = SIO.Path.Combine(combinedFnames, s);

			string partialPathName;
			string fullPathName;

			this.Calculate(createDirs, combinedFnames, out partialPathName, out fullPathName);

			return fullPathName;
		}

		private void Calculate(bool createDirs, string fname, out string partialPathname, out string fullPathname)
		{
			partialPathname = SIO.Path.Combine(this.relativePathName, fname);

			if (SIO.Path.IsPathRooted(partialPathname))
				throw new IsolatedStorageException(String.Format("partialPathname cannot be a full pathname! [{0}]", partialPathname));

			string fn = SIO.Path.Combine(ROOTPATH, partialPathname);
			fullPathname = SIO.Path.GetFullPath(fn); // Checks against a series of ".." trying to escape the intended sandbox

			if (!fullPathname.StartsWith(this.fullPathName, StringComparison.Ordinal))
				throw new IsolatedStorageException(String.Format("partialPathname cannot have relative paths leading outside the KSP file system! [{0}]", partialPathname));

			if (createDirs)
			{
				string d = System.IO.Path.GetDirectoryName(fullPathname);
				if (!System.IO.Directory.Exists(d))
					System.IO.Directory.CreateDirectory(d);
			}
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
	}

}
