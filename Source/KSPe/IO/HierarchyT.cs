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

namespace KSPe.IO
{
	public class Hierarchy<T> : Hierarchy
	{
		internal static readonly LocalCache<string> CACHE = new LocalCache<string>();

		// TODO: Remove on Version 2.6
		[System.Obsolete("KSPe.IO.Hierarchy<T>.ROOT is deprecated, and will be removed next version. Use KSPe.IO.Hierarchy.ROOT instead.")]
		new public static readonly Hierarchy<T> ROOT = new Hierarchy<T>(Hierarchy.ROOT);

		new public static readonly Hierarchy<T> GAMEDATA = new HierarchyCommon<T>(Hierarchy.GAMEDATA);
		new public static readonly Hierarchy<T> PLUGINDATA = new HierarchyCommon<T>(Hierarchy.PLUGINDATA);
		new public static readonly Hierarchy<T> LOCALDATA = new HierarchyCommon<T>(Hierarchy.LOCALDATA);
		new public static readonly Hierarchy<T> SCREENSHOT = new HierarchyCommon<T>(Hierarchy.SCREENSHOT);
		new public static readonly Hierarchy<T> SAVE = new HierarchySave<T>(Hierarchy.SAVE);
		new public static readonly Hierarchy<T> THUMB = new HierarchyCommon<T>(Hierarchy.THUMB);

		protected Hierarchy hierarchy;
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
			LocalCache<string>.Dictionary c = CACHE[typeof(T)];
			return c.ContainsKey(".") ? c["."] : (c["."] = calculateTypeRoot());
		}

		private static string calculateTypeRoot() => Util.SystemTools.Reflection.Version<T>.EffectivePathInternal;
	}

	public class HierarchyCommon<T>:Hierarchy<T>
	{
		new protected HierarchyCommon hierarchy => (base.hierarchy as HierarchyCommon);
		public HierarchyCommon(Hierarchy hierarchy) : base(hierarchy)
		{
		}

		internal override void CalculateGambiarra(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
		{
			HierarchyCommon.Calculate(this.name, this.relativePathName, this.fullPathName, createDirs, fname, out partialPathNameResult, out fullPathNameResult);
		}
	}

	public class HierarchySave<T>:Hierarchy<T>
	{
		new protected HierarchySave hierarchy => (base.hierarchy as HierarchySave);
		internal HierarchySave(Hierarchy hierarchy) : base(hierarchy, hierarchy.dirName) { }

		internal override void CalculateGambiarra(bool createDirs, string fname, out string partialPathNameResult, out string fullPathNameResult)
		{
			HierarchySave.Calculate(this.name, this.relativePathName, this.fullPathName, createDirs, fname, out partialPathNameResult, out fullPathNameResult);
		}
	}

}
