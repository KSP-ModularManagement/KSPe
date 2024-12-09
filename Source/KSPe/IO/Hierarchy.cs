﻿/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP Enhanced /L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using System.IO.IsolatedStorage;

using SIO = System.IO;

namespace KSPe.IO
{
	public class Hierarchy
	{
		internal static readonly string ROOTPATH = Path.AppRoot();

		public static readonly Hierarchy ROOT = new HierarchyCommon("ROOT");
		public static readonly Hierarchy GAMEDATA = new HierarchyCommon("GAMEDATA", "GameData");
		public static readonly Hierarchy PLUGINDATA = new HierarchyCommon("PLUGINDATA", "PluginData");
		public static readonly Hierarchy LOCALDATA = new HierarchyCommon("LOCALDATA", Path.Combine(GAMEDATA.relativePathName, "__LOCAL"));
		public static readonly Hierarchy SCREENSHOT = new HierarchyCommon("SCREENSHOT", "Screenshots");
		public static readonly Hierarchy SAVE = new HierarchySave("SAVE", "saves");
		public static readonly Hierarchy THUMB = new HierarchyCommon("THUMB", "thumbs");

		internal readonly string name;
		internal readonly string dirName;
		internal readonly string fullPathName;
		internal readonly string relativePathName;

		protected Hierarchy(string name)
		{
			this.name = name;
			this.dirName = null;
			this.relativePathName = Path.EnsureTrailingSeparatorOnDir(".", true);
			this.fullPathName = Path.EnsureTrailingSeparatorOnDir(
				Path.GetFullPath(Path.Combine(ROOTPATH, relativePathName)) // Ensures any path shenanigans are resolved.
				, true);
		}
		protected Hierarchy(string name, string dirName)
		{
			this.name = name;
			this.dirName = dirName;
			this.fullPathName = Path.EnsureTrailingSeparatorOnDir(
				Path.GetFullPath(Path.Combine(ROOTPATH, dirName)) // Ensures any path shenanigans are resolved.
				, true);
			this.relativePathName = Path.EnsureTrailingSeparatorOnDir(
				this.fullPathName.Replace(ROOTPATH, "")
				, true);
		}

		new public String ToString() => this.name;

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
			string combinedFnames = fname;
			foreach (string s in fnames)
				combinedFnames = Path.Combine(combinedFnames, s);

			string resultRelativePathName, resultFullPathName;

			this.Calculate(createDirs, combinedFnames, out resultRelativePathName, out resultFullPathName);

			return resultRelativePathName;
		}

		internal string SolveFull(bool createDirs, string fname, params string[] fnames)
		{
			string combinedFnames = fname;
			foreach (string s in fnames)
				combinedFnames = Path.Combine(combinedFnames, s);

			string resultRelativePathName, resultFullPathName;

			this.Calculate(createDirs, combinedFnames, out resultRelativePathName, out resultFullPathName);

			return resultFullPathName;
		}

		// I can't turn this into abstract, because it would break existing binaries!
		protected void Calculate(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
			=> this.CalculateGambiarra(createDirs, fname, out partialPathNameResult, out fullPathNameResult);

		// This indirection was necessary to avoid changing the ABI, as I can't turn this class into `abstract` without screwing
		// current compiled binaries. So, yeah... Another gambiarra. :(
		internal virtual void CalculateGambiarra(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
			=> throw new NotImplementedException("Hierarchy.CalculateGambiarra");

		internal static string CalculateRelativePath(string fullDestinationPath, string rootPath)
		{
			Log.debug("CalculateRelativePath: {0} {1}", fullDestinationPath, rootPath);

			// from https://social.msdn.microsoft.com/Forums/vstudio/en-US/954346c8-cbe8-448c-80d0-d3fc27796e9c - Wednesday, May 20, 2009 3:37 PM
			string[] startPathParts = Path.GetFullPath(rootPath).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
			string[] destinationPathParts = Path.GetFullPath(fullDestinationPath).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);

			int i = 0; // Finds the first difference on both paths (if any)
			int max = Math.Min(startPathParts.Length, destinationPathParts.Length);
			while ((i < max) && startPathParts[i].Equals(destinationPathParts[i], StringComparison.Ordinal))
				++i;

			if (0 == i) return fullDestinationPath;

			Boolean imSureItsDir = fullDestinationPath.EndsWith(IO.Path.DirectorySeparatorStr);
			System.Text.StringBuilder relativePath = new System.Text.StringBuilder();

			if (i >= startPathParts.Length)
				relativePath.Append(".").Append(Path.DirectorySeparatorChar); // Just for the LULZ.
			else
				for (int j = i; j < startPathParts.Length; j++) // Adds how many ".." as necessary
					relativePath.Append("..").Append(Path.DirectorySeparatorChar);

			for (int j = i; j < destinationPathParts.Length; j++) // And now feeds the remaning directories
				relativePath.Append(destinationPathParts[j]).Append(Path.DirectorySeparatorChar);

			relativePath.Length--; // Gets rid of the trailig "/" that is always appended

			Log.debug("relativePath: {0}", relativePath.ToString());

			//From now on, we **want** the trailling "/" on every pathname that it's a directory.
			return Path.EnsureTrailingSeparatorOnDir(relativePath.ToString(), imSureItsDir);
		}
	}

	public class HierarchyCommon : Hierarchy
	{
		internal HierarchyCommon(string name) : base(name) { }
		internal HierarchyCommon(string name, string dirName) : base(name, dirName) { }

		internal override void CalculateGambiarra(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
		{
			Calculate(this.name, this.relativePathName, this.fullPathName, createDirs, fname, out partialPathNameResult, out fullPathNameResult);
		}

		internal static void Calculate(string name, string relativePathName, string fullPathName, bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
		{
			Log.debug("HierarchyCommon.Calculate()");
			partialPathNameResult = Path.Combine(relativePathName, fname);

			if (Path.IsPathRooted(partialPathNameResult))
				throw new IsolatedStorageException(String.Format("partialPathname cannot be a full pathname! [{0}]", partialPathNameResult));

			partialPathNameResult = Path.EnsureTrailingSeparatorOnDir(partialPathNameResult, false);
			fullPathNameResult = Path.Combine(fullPathName, fname);

			{  // Checks against a series of ".." trying to escape the intended sandbox
				string normalizedFullPathNameResult = Path.GetFullPath(fullPathNameResult);
				string normalizedFullPathName = Path.GetFullPath(fullPathName);

				if (!normalizedFullPathNameResult.StartsWith(normalizedFullPathName, StringComparison.Ordinal))
					throw new IsolatedStorageException(String.Format("partialPathname cannot have relative paths leading outside the sandboxed file system! [{0}]", partialPathNameResult));
			}
			if (createDirs)
			{
				string d = System.IO.Path.GetDirectoryName(fullPathNameResult);
				if (!Directory.Exists(d))
					System.IO.Directory.CreateDirectory(d);
			}
			Log.debug("Hierarchy Calculate {0} {1} {2}", name, partialPathNameResult, fullPathNameResult);
		}
	}

	public class HierarchySave: Hierarchy
	{
		internal HierarchySave(string name, string dirName) : base(name, dirName) { }
		internal static readonly string ADDONS_DIR = "AddOns";

		internal override void CalculateGambiarra(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
		{
			Calculate(this.name, this.relativePathName, this.fullPathName, createDirs, fname, out partialPathNameResult, out fullPathNameResult);
		}

		internal static void Calculate(string name, string relativePathName, string fullPathName, bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
		{
			Log.debug("HierarchySave.Calculate({0}, {1}, {2}, {3}, {4})", name, relativePathName, fullPathName, createDirs, fname);
			if (!SaveGameMonitor.Instance.IsValid)
				throw new IsolatedStorageException(String.Format("Savegames can only be solved after loading or creating a game!!"));

			partialPathNameResult = Path.Combine(relativePathName, SaveGameMonitor.Instance.saveDirName, ADDONS_DIR, fname);

			if (Path.IsPathRooted(partialPathNameResult))
				throw new IsolatedStorageException(String.Format("partialPathname cannot be a full pathname! [{0}]", partialPathNameResult));

			fullPathNameResult = Path.Combine(fullPathName, SaveGameMonitor.Instance.saveDirName, ADDONS_DIR, fname);

			// Checks against a series of ".." trying to escape the intended sandbox{
			{
				Log.debug("IsolatedStorageCheck full {0} ; mangled {1}", fullPathNameResult, fullPathNameResult);
				string normalizedFullPathname = Path.GetFullPath(fullPathNameResult);
				string normalizedFullPathNameMangled = Path.GetFullPath(fullPathNameResult);
				Log.debug("IsolatedStorageCheck Normalized full {0} ; mangled {1}", normalizedFullPathname, normalizedFullPathNameMangled);
				if (!normalizedFullPathname.StartsWith(normalizedFullPathNameMangled, StringComparison.Ordinal))
					throw new IsolatedStorageException(String.Format("partialPathname cannot have relative paths leading outside the sandboxed file system! [{0}]", partialPathNameResult));
			}

			if (createDirs)
			{
				string d = System.IO.Path.GetDirectoryName(fullPathNameResult);
				if (!Directory.Exists(d))
					SIO.Directory.CreateDirectory(d);
			}
			Log.debug("HierarchySave Calculate restul {0} {1} {2}", name, partialPathNameResult, fullPathNameResult);
		}
	}
}
