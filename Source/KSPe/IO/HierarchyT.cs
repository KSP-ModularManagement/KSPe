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
using System.Collections.Generic;
using System.Text.RegularExpressions;

using SIO = System.IO;

namespace KSPe.IO
{
	public class Hierarchy<T> : Hierarchy
	{
		// TODO: Remove on Version 2.6
		[System.Obsolete("KSPe.IO.Hierarchy<T>.ROOT is deprecated, and will be removed next version. Use KSPe.IO.Hierarchy.ROOT instead.")]
		new public static readonly Hierarchy<T> ROOT = new HierarchyCommon<T>(Hierarchy.ROOT);

		new public static readonly Hierarchy<T> GAMEDATA = new HierarchyCommon<T>(Hierarchy.GAMEDATA);
		new public static readonly Hierarchy<T> PLUGINDATA = new HierarchyCommon<T>(Hierarchy.PLUGINDATA);
		new public static readonly Hierarchy<T> LOCALDATA = new HierarchyCommon<T>(Hierarchy.LOCALDATA);
		new public static readonly Hierarchy<T> SCREENSHOT = new HierarchyCommon<T>(Hierarchy.SCREENSHOT);
		new public static readonly Hierarchy<T> SAVE = new HierarchySave<T>(Hierarchy.SAVE);
		new public static readonly Hierarchy<T> THUMB = new HierarchyCommon<T>(Hierarchy.THUMB);

		// FSCKING UNBELIEVABLE!!
		// The loading of this class is not happening atomically, a client is being able to call CalculateTypeRoot indirectly **BEFORE**
		// the runtime is being abble to create the readonly static objects, what **should** be happening after loading the class and before
		// unlocking the class to be used by anyone else!
		//
		// Bellow the original code - thanks God it's a `internal`, will not break the ABI
		//internal static readonly LocalCache<string> CACHE = new LocalCache<string>();
		private static LocalCache<string> __CACHE = null;
		internal static LocalCache<string> CACHE => (__CACHE ?? (__CACHE = new LocalCache<string>()));

		protected readonly Hierarchy hierarchy;
		protected Hierarchy(Hierarchy hierarchy) : base(hierarchy.ToString(), Path.Combine(hierarchy.relativePathName, CalculateTypeRoot()))
		{
			this.hierarchy = hierarchy;
		}
		protected Hierarchy(Hierarchy hierarchy, string dirName) : base(hierarchy.ToString(), dirName)
		{
			this.hierarchy = hierarchy;
		}

		new public String ToString() => string.Format("{0}<{1}>", this.name, CalculateTypeRoot());

		internal static string CalculateTypeRoot()
		{
			Log.debug("CalculateTypeRoot {0} cache {1}", typeof(T), CACHE);
			LocalCache<string>.Dictionary c = CACHE[typeof(T)];
			return c.ContainsKey(".") ? c["."] : (c["."] = calculateTypeRoot());
		}

		private static string calculateTypeRoot() => Util.SystemTools.Reflection.Version<T>.EffectivePathInternal;
	}

	public class HierarchyCommon<T>:Hierarchy<T>
	{
		internal HierarchyCommon(Hierarchy hierarchy) : base(hierarchy) { }

		internal override void CalculateGambiarra(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
			=> HierarchyCommon.Calculate(this.name, this.relativePathName, this.fullPathName, createDirs, fname, out partialPathNameResult, out fullPathNameResult);
	}

	public class HierarchySave<T>:Hierarchy<T>
	{
		new protected HierarchySave hierarchy => (base.hierarchy as HierarchySave);

		private static readonly HashSet<string> MIGRATED_SET = new HashSet<string>();
		private bool HadMigrated => MIGRATED_SET.Contains(this.relativePathName);

		internal HierarchySave(Hierarchy hierarchy) : base(hierarchy, hierarchy.dirName) { }

		internal override void CalculateGambiarra(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
		{
			bool hadMigrated = this.HadMigrated;
			Log.debug("HierarchySave<{0}>.CalculateGambiarra(...) {1} {2} {3}", typeof(T).FullName, fname, this.fullPathName, hadMigrated);
			fname = Path.Combine(CalculateTypeRoot(), fname);
			if (!hadMigrated)
			{
				HierarchySave.Calculate(this.name, this.relativePathName, this.fullPathName, true, fname, out partialPathNameResult, out fullPathNameResult);
				try
				{
					this.Migrate(Directory.GetParent(fullPathNameResult).FullName);
				}
				catch (Exception e)
				{
					Log.error(e, this);
				}
				MIGRATED_SET.Add(this.relativePathName);
				return;
			}
			HierarchySave.Calculate(this.ToString(), this.relativePathName, this.fullPathName, createDirs, fname, out partialPathNameResult, out fullPathNameResult);
		}

		private void Migrate(string targetFullPathName)
		{
			string oldFullPathnameMangled = Regex.Replace( 
						Hierarchy.SAVE.fullPathName
						, Path.DirectorySeparatorRegex + Hierarchy.SAVE.dirName + Path.DirectorySeparatorRegex
						, Path.DirectorySeparatorChar + Hierarchy.SAVE.dirName + Path.DirectorySeparatorChar + SaveGameMonitor.Instance.saveDirName + Path.DirectorySeparatorChar + HierarchySave.ADDONS_DIR + Path.DirectorySeparatorChar
					);

			Log.debug("HierarchySave<{0}>.Migrate() {1} {2}", typeof(T).FullName, oldFullPathname, targetFullPathName);

			oldFullPathname = SIO.Path.GetFullPath(oldFullPathname);
			targetFullPathName = SIO.Path.GetFullPath(targetFullPathName);

			Log.debug("HierarchySave<{0}>.Migrate() normalized {1} {2}", typeof(T).FullName, oldFullPathname, targetFullPathName);

			if (!targetFullPathName.Equals(oldFullPathname))
				// Oh, crap. This is going to hurt.
				//
				// When I initially wrote this code, I thought it would be a good idea to rely on the current savegame's name,
				// completely ignoring that:
				//	1. Someone could rename it later, but KSP will not change it's hosting directory name
				//	2. People using non english characters will have the Game's tittle pretty different from the real directory's
				//
				// So now I will pass the rest of my existance doing migrations to prevent data loss. (sigh)
				try
				{
					Directory.Migrate(oldFullPathname, targetFullPathName);
				}
				catch (Exception e)
				{	// Oukey. Something got very, very wrong.
					// Let's play safe and keep using the old files.
					Log.error(e, "Exception trying to migrate {0} to {1} !", oldFullPathname, targetFullPathName);
				}
		}
	}
}
