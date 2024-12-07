/*
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
using System.Text.RegularExpressions;

namespace KSPe.IO
{
	public class Hierarchy
	{
		internal static readonly string ROOTPATH = Path.AppRoot();

		public static readonly Hierarchy ROOT = new Hierarchy("ROOT");
		public static readonly Hierarchy GAMEDATA = new Hierarchy("GAMEDATA", "GameData");
		public static readonly Hierarchy PLUGINDATA = new Hierarchy("PLUGINDATA", "PluginData");
		public static readonly Hierarchy LOCALDATA = new Hierarchy("LOCALDATA", Path.Combine(GAMEDATA.relativePathName, "__LOCAL"));
		public static readonly Hierarchy SCREENSHOT = new Hierarchy("SCREENSHOT", "Screenshots");
		public static readonly Hierarchy SAVE = new Hierarchy("SAVE", "saves");
		public static readonly Hierarchy THUMB = new Hierarchy("THUMB", "thumbs");

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
			string combinedFnames = fname;
			foreach (string s in fnames)
				combinedFnames = Path.Combine(combinedFnames, s);

			string resultRelativePathName,resultFullPathName;

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

		private const string ADDONS_FOLDER = "AddOns";
		private void Calculate(bool createDirs, string fname, out string partialPathname, out string fullPathname)
		{
			if (SAVE.name == this.name && null == HighLogic.CurrentGame)
				throw new IsolatedStorageException(String.Format("Savegames can only be solved after loading or creating a game!!"));

			partialPathname = Path.Combine(this.relativePathName, fname);
			if (SAVE.name == this.name) // Tremenda gambiarra dos infernos... Caracas... :P
					partialPathname = Regex.Replace(
						partialPathname
						, "^" + SAVE.dirName + Path.DirectorySeparatorRegex
						, SAVE.dirName + Path.DirectorySeparatorChar + HighLogic.CurrentGame.Title.Replace(" (SANDBOX)","").Replace(" (CAREER)", "").Replace(" (SCIENCE_SANDBOX)", "") + Path.DirectorySeparatorChar + ADDONS_FOLDER + Path.DirectorySeparatorChar
					)
				;

			if (Path.IsPathRooted(partialPathname))
				throw new IsolatedStorageException(String.Format("partialPathname cannot be a full pathname! [{0}]", partialPathname));

			partialPathname = IO.Path.EnsureTrailingSeparatorOnDir(partialPathname, false);

			string this_fullPathNameMangled = this.fullPathName;
			if (SAVE.name == this.name) // Gambiarras, gambiarras, gambiarras everywhere!! :P (using the voice of Hermes from Disney's Hercules
				this_fullPathNameMangled = Regex.Replace( 
							this.fullPathName
							, Path.DirectorySeparatorChar + SAVE.dirName + Path.DirectorySeparatorRegex
							, Path.DirectorySeparatorChar + SAVE.dirName + Path.DirectorySeparatorChar + HighLogic.CurrentGame.Title.Replace(" (SANDBOX)","").Replace(" (CAREER)", "").Replace(" (SCIENCE)", "") + Path.DirectorySeparatorChar + ADDONS_FOLDER + Path.DirectorySeparatorChar
						);

			string fn = Path.Combine(this_fullPathNameMangled, fname);
			fullPathname = Path.GetFullPath(fn); // Checks against a series of ".." trying to escape the intended sandbox

			Log.debug("IsolatedStorageCheck full {0} ; mangled {1}", fullPathname, this_fullPathNameMangled);
			if (!fullPathname.StartsWith(this_fullPathNameMangled, StringComparison.Ordinal))
				throw new IsolatedStorageException(String.Format("partialPathname cannot have relative paths leading outside the sandboxed file system! [{0}]", partialPathname));

			if (createDirs)
			{
				string d = System.IO.Path.GetDirectoryName(fullPathname);
				if (!Directory.Exists(d))
					System.IO.Directory.CreateDirectory(d);
			}
			Log.debug("Hierarchy Calculate {0} {1} {2}", this.name, partialPathname, fullPathname);
			Log.debug("CurrentGame: {0}", null == HighLogic.CurrentGame ? "NULL" : HighLogic.CurrentGame.linkURL);
		}

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
}
