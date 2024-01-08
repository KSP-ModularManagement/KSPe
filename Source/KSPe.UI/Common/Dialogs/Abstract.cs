/*
	This file is part of KSPe.UI, a component for KSP Enhanced /L
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
using UnityEngine;

namespace KSPe.Common.Dialogs
{
	public class AbstractDialog
	{
		private static Texture2D windowTex = null;
		protected static void SetWindowBackground(GUIStyle style)
		{
			if (null == windowTex)
			{
				//windowTex = style.normal.background;
				//Color32[] pixels = windowTex.GetPixels32();
				//for (int i = 0;i < pixels.Length;++i) pixels[i].a = 96;
				//windowTex.SetPixels32(pixels);
				windowTex = new Texture2D(1, 1);
				windowTex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.45f));
				windowTex.Apply();
			}
			style.active.background =
				style.focused.background =
				style.normal.background = windowTex;
		}

		private static Texture2D textTex = null;
		protected static void SetTextBackground(GUIStyle style)
		{
			if (null == textTex)
			{ 
				textTex = new Texture2D(1, 1);
				//textTex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.25f));
				textTex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.45f));
				textTex.Apply();
			}
			style.active.background =
				style.focused.background =
				style.normal.background = textTex;
		}

	}
}
