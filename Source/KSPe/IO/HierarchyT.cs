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
		internal static readonly LocalCache<string> CACHE = new LocalCache<string>();
		private Hierarchy(Hierarchy hierarchy) : base(hierarchy.ToString(), SIO.Path.Combine(hierarchy.relativePathName, CalculateTypeRoot())) {}

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

		private static string calculateTypeRoot()
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
