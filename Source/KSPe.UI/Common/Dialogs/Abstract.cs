using UnityEngine;
using KSPe.UI;

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
				style.active.background =
					style.focused.background =
					style.normal.background = windowTex;
			}
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
