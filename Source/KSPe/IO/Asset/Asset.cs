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
using UTexture2D = UnityEngine.Texture2D;

namespace KSPe.IO
{
	// Public interface for KSPe 3.
	public static class Asset<T>
	{
		public static class Texture2D
		{
			public static UTexture2D LoadFromFile(String fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(fn);
			}

			public static UTexture2D LoadFromFile(String fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(fn, fns);
			}

			public static UTexture2D LoadFromFile(int width, int height, string fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, fn);
			}

			public static UTexture2D LoadFromFile(int width, int height, string fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, fn, fns);
			}

			public static UTexture2D LoadFromFile(bool mipmap, String fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(mipmap, fn);
			}

			public static UTexture2D LoadFromFile(bool mipmap, string fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(mipmap, fn, fns);
			}

			public static UTexture2D LoadFromFile(int width, int height, bool mipmap, String fn)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, mipmap, fn);
			}

			public static UTexture2D LoadFromFile(int width, int height, bool mipmap, string fn, params string[] fns)
			{
				return KSPe.IO.Asset.Texture2D<T>.LoadFromFile(width, height, mipmap, fn, fns);
			}
		}
	}
}
