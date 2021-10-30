/*
	This file is part of KSPe, a component for KSP API Extensions/L
	© 2018-21 Lisias T : http://lisias.net <support@lisias.net>

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
using SIO = System.IO;

namespace KSPe.IO
{
	namespace Simple
	{
		public static class Path<T>
		{
			// string exportPath = KSPUtil.ApplicationRootPath + "/GameData/MechJeb2/Export/";
			// string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			// KSP.IO.IOUtils.GetFilePathFor(typeof(T), fn);

			private static readonly LocalCache<string> CACHE = new LocalCache<string>();

			private static string CalculateRoot()
			{
				string r = KSPUtil.ApplicationRootPath;
				r = Path.Combine(r, "PluginData");
				r = Path.Combine(r,  typeof(T).Namespace);
				SIO.Directory.CreateDirectory(r);
				return r;
			}

			private static string Solve(string fn)
			{
				LocalCache<string>.Dictionary c = CACHE[typeof(T)];
				string root =  c.ContainsKey(".") ? c["."] : (c["."] = CalculateRoot());
				return Path.Combine(root, fn);
			}

			public static string Name(string fn)
			{
				fn = File<T>.Name(fn);
				return Solve(fn);
			}

			public static string Name(string mask, params object[] @params)
			{
				string fn = Name(string.Format(mask, @params));
				return Solve(fn);
			}

			public static string Combine(string dir, string fn)
			{
				fn = IO.Path.Combine(Directory<T>.Name(dir), File<T>.Name(fn));
				return Solve(fn);
			}

			public static string Combine(string dir, string mask, params object[] @params)
			{
				string fn = IO.Path.Combine(Directory<T>.Name(dir), File<T>.Name(string.Format(mask, @params)));
				return Solve(fn);
			}

		}

		public static class File<T>
		{
			public static string Name(string fn)
			{
				return string.Join("_", fn.Split(IO.Path.GetInvalidFileNameChars())); // Strip illegal char from the filename
			}

			public static bool Exists(string fn)
			{
				string fullPath = Path<T>.Name(fn);
				return SIO.File.Exists(fullPath);
			}

			public static bool Exists(string mask, params object[] @params)
			{
				string fullPath = Path<T>.Name(mask, @params);
				return SIO.File.Exists(fullPath);
			}

			public static void Delete(string fn)
			{
				string fullPath = Path<T>.Name(fn);
				if (SIO.File.Exists(fullPath))
					SIO.File.Delete(fullPath);
			}

			public static void Delete(string mask, params object[] @params)
			{
				string fullPath = Path<T>.Name(mask, @params);
				if (SIO.File.Exists(fullPath))
					SIO.File.Delete(fullPath);
			}
		}

		public static class Directory<T>
		{
			public static string Name(string fn)
			{
				return File<T>.Name(fn); // Just to be orthogonal
			}

			public static bool Exists(string fn)
			{
				string fullPath = Path<T>.Name(fn);
				return Directory.Exists(fullPath);
			}

			public static bool Exists(string mask, params object[] @params)
			{
				string fullPath = Path<T>.Name(mask, @params);
				return Directory.Exists(fullPath);
			}

			public static void Delete(string fn)
			{
				string fullPath = Path<T>.Name(fn);
				if (Directory.Exists(fullPath))
					SIO.Directory.Delete(fullPath);
			}

			public static void Delete(string mask, params object[] @params)
			{
				string fullPath = Path<T>.Name(mask, @params);
				if (Directory.Exists(fullPath))
					SIO.Directory.Delete(fullPath);
			}

			public static void Create(string fn)
			{
				string fullPath = Path<T>.Name(fn);
				if (!Directory.Exists(fullPath))
					SIO.Directory.CreateDirectory(fullPath);
			}

			public static void Create(string mask, params object[] @params)
			{
				string fullPath = Path<T>.Name(mask, @params);
				if (!Directory.Exists(fullPath))
					SIO.Directory.CreateDirectory(fullPath);
			}
		}
	}
}
