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

namespace KSPe.GameDB
{
	public static class Asset<T>
	{
		public static string Solve(string fn)
		{
			string r = KSPe.IO.File<T>.Asset.Solve(fn);
			r = r.Substring(r.IndexOf("GameData/", StringComparison.Ordinal) + 9);
			return r;
		}

		public static string Solve(string fn, LocalCache<string> cache)
		{
			LocalCache<string>.Dictionary c = cache[typeof(T)];
			return c.ContainsKey(fn) ? c[fn] : (c[fn] = Solve(fn));
		}
	}
}
