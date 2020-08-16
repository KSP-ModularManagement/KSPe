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
using SIO = System.IO;

namespace KSPe.GameDB
{
	public static class Asset<T>
	{
       internal static readonly KSPe.LocalCache<string> CACHE = new KSPe.LocalCache<string>();

		// Hell of a hack, but it works for now! :)
		private static string hackPath(string r)
		{
			{ 
				int i = r.IndexOf("GameData/", StringComparison.Ordinal);
				r = (i < 0) ? r : r.Substring(i + 9);
			}

			return r.Replace("\\", "/"); // GameDatabase uses "/" on the naming.
			// FIXME: I need to do furher transformations (underscores, dots, etc)
		}

		public static string Solve(string fn)
		{
			LocalCache<string>.Dictionary c = CACHE[typeof(T)];
			if (c.ContainsKey(fn)) return c[fn];

			string r = IO.Hierarchy<T>.GAMEDATA.SolveFull(false, "Assets"); // Hack dos infernos. :( Perhaps this Assets stunt isn't a good idea after all. :/
			if (SIO.File.Exists(r))
				r = IO.Hierarchy<T>.GAMEDATA.SolveFull(false, "Assets", fn);
			else
				r = IO.Hierarchy<T>.GAMEDATA.SolveFull(false, fn);
			

			r =  hackPath(r);
			return c[fn] = r;
		}

		public static string Solve(string fn, params string[] fns)
		{
			string path = fn;
			foreach (string s in fns)
				path = IO.Path.Combine(path, s);
			return Solve(path);
		}

	}
}
