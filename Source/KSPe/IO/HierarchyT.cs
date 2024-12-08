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
		private Hierarchy(Hierarchy hierarchy) : base(hierarchy.ToString(), Path.Combine(hierarchy.relativePathName, CalculateTypeRoot())) {}

		// TODO: Remove on Version 2.6
		[System.Obsolete("KSPe.IO.Hierarchy<T>.ROOT is deprecated, and will be removed next version. Use KSPe.IO.Hierarchy.ROOT instead.")]
		new public static readonly Hierarchy<T> ROOT = new Hierarchy<T>(Hierarchy.ROOT);

		new public static readonly Hierarchy<T> GAMEDATA = new Hierarchy<T>(Hierarchy.GAMEDATA);
		new public static readonly Hierarchy<T> PLUGINDATA = new Hierarchy<T>(Hierarchy.PLUGINDATA);
		new public static readonly Hierarchy<T> LOCALDATA = new Hierarchy<T>(Hierarchy.LOCALDATA);
		new public static readonly Hierarchy<T> SCREENSHOT = new Hierarchy<T>(Hierarchy.SCREENSHOT);
		new public static readonly Hierarchy<T> SAVE = new Hierarchy<T>(Hierarchy.SAVE);
		new public static readonly Hierarchy<T> THUMB = new Hierarchy<T>(Hierarchy.THUMB);

		internal static string CalculateTypeRoot()
		{
			LocalCache<string>.Dictionary c = CACHE[typeof(T)];
			return c.ContainsKey(".") ? c["."] : (c["."] = calculateTypeRoot());
		}

		private static string calculateTypeRoot() => Util.SystemTools.Reflection.Version<T>.EffectivePathInternal;
	}
}
