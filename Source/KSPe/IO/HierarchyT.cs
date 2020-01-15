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
using System.Linq;
using SIO = System.IO;
using System.Reflection;

namespace KSPe.IO
{
	public class Hierarchy<T> : Hierarchy
	{
		private Hierarchy(Hierarchy hierarchy) : base(hierarchy) { }

		new public static Hierarchy ROOT = new Hierarchy<T>(Hierarchy.ROOT);
		new public static Hierarchy GAMEDATA = new Hierarchy<T>(Hierarchy.GAMEDATA);
		new public static Hierarchy PLUGINDATA = new Hierarchy<T>(Hierarchy.PLUGINDATA);
		new public static Hierarchy LOCALDATA = new Hierarchy<T>(Hierarchy.LOCALDATA);
		new public static Hierarchy SCREENSHOT = new Hierarchy<T>(Hierarchy.SCREENSHOT);
		new public static Hierarchy SAVE = new Hierarchy<T>(Hierarchy.SAVE);
		new public static Hierarchy THUMB = new Hierarchy<T>(Hierarchy.THUMB);

		internal static readonly LocalCache<string> CACHE = new LocalCache<string>();

		new public string Solve()
		{
			return CalculateRoot(this);
		}

		new public string Solve(bool createDirs, string fname, params string[] fnames)
		{
			string rootDir = this.Solve();
			string path = SIO.Path.Combine(rootDir, fname);
			return this.Solve(createDirs, path, fnames);
		}

		new internal string SolveFull(bool createDirs, string fname, params string[] fnames)
		{
			string rootDir = this.Solve();
			string path = SIO.Path.Combine(rootDir, fname);
			return this.SolveFull(createDirs, path, fnames);
		}

		internal static string CalculateRoot()
		{
			LocalCache<string>.Dictionary c = CACHE[typeof(T)];
			return c.ContainsKey(".") ? c["."] : (c["."] = calculateRoot());
		}

		private static string CalculateRoot(Hierarchy<T> hierarchy)
		{
			LocalCache<string>.Dictionary c = CACHE[typeof(T)];
			return c.ContainsKey(hierarchy.dirName) ? c[hierarchy.dirName] : (c[hierarchy.dirName] = SIO.Path.Combine(calculateRoot(), hierarchy.dirName));
		}

		private static string calculateRoot()
		{
			string typeRootDir = typeof(T).Namespace;
			{
				Type t = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
						  from tt in assembly.GetTypes()
						  where tt.Namespace == typeof(T).Namespace && tt.Name == "Version" && tt.GetMembers().Any(m => m.Name == "Namespace")
						  select tt).FirstOrDefault();

				typeRootDir = (null == t)
					? typeRootDir
					: t.GetField("Namespace").GetValue(null).ToString();
			}
			{
				Type t = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
						  from tt in assembly.GetTypes()
						  where tt.Namespace == typeof(T).Namespace && tt.Name == "Version" && tt.GetMembers().Any(m => m.Name == "Vendor")
						  select tt).FirstOrDefault();

				typeRootDir = (null == t)
					? typeRootDir
					: SIO.Path.Combine(t.GetField("Vendor").GetValue(null).ToString(), typeRootDir);
			}
			return typeRootDir;
		}
	}
}
