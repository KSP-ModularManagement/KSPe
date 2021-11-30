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
using System;
using UTexture2D = UnityEngine.Texture2D;

namespace KSPe.IO.Local
{
	internal static class Texture2D<T>
	{
		public static UTexture2D LoadFromFile(String fn)
		{
			string path = IO.File<T>.Local.Solve(fn);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path);
		}

		public static UTexture2D LoadFromFile(String fn, params string[] fns)
		{
			string path = IO.File<T>.Local.Solve(fn, fns);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path);
		}

		public static UTexture2D LoadFromFile(int width, int height, string fn)
		{
			string path = IO.File<T>.Local.Solve(fn);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path, width, height, false);
		}

		public static UTexture2D LoadFromFile(int width, int height, string fn, params string[] fns)
		{
			string path = IO.File<T>.Local.Solve(fn, fns);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path, width, height, false);
		}

		public static UTexture2D LoadFromFile(bool mipmap, String fn)
		{
			string path = IO.File<T>.Local.Solve(fn);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path, -1, -1, mipmap);
		}

		public static UTexture2D LoadFromFile(bool mipmap, string fn, params string[] fns)
		{
			string path = IO.File<T>.Local.Solve(fn, fns);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path, -1, -1, mipmap);
		}

		public static UTexture2D LoadFromFile(int width, int height, bool mipmap, String fn)
		{
			string path = IO.File<T>.Local.Solve(fn);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path, -1, -1, mipmap);
		}

		public static UTexture2D LoadFromFile(int width, int height, bool mipmap, string fn, params string[] fns)
		{
			string path = IO.File<T>.Local.Solve(fn, fns);
			return KSPe.Util.Image.Texture2D.LoadFromFile(path, -1, -1, mipmap);
		}
	}
}
