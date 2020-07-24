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
namespace KSPe.Util
{
	using Reflection = System.Reflection;
	using Type = System.Type;

	public static class SystemTools
	{
		public static class Assembly
		{
			public static Reflection.Assembly LoadFromFile(string pathname)
			{
				return Reflection.Assembly.LoadFrom(pathname);
			}

			public static Reflection.Assembly LoadFromFileAndStartup(string pathname)
			{
				Reflection.Assembly assembly = LoadFromFile(pathname);
				foreach (Type type in assembly.GetTypes())
				{
					if ("Startup" != type.Name) continue;

					object instance = System.Activator.CreateInstance(type);
					InvokeOrNull(type, instance, "Awake");
					InvokeOrNull(type, instance, "Start");
				}
				return assembly;
			}

			private static object InvokeOrNull(Type t, object o, string methodName)
			{
				Reflection.MethodInfo method = t.GetMethod(methodName, Reflection.BindingFlags.Public |Reflection.BindingFlags.Instance);
				method = method ?? t.GetMethod(methodName, Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance);
				if (null != method)	return method.Invoke(o, null);
				return null;
			}
		}
	}
}

