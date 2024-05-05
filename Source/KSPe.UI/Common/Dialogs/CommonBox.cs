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
using System;
using UnityEngine;
using KSPe.UI;

namespace KSPe.Common.Dialogs
{
	public class TimedCommonBox : TimedMessageBox
	{
		private const float TITTLE_TEXT_SIZE = 22;
		private const float TITTLE_BORDER = 5;
		private const float BODY_TEXT_SIZE = 14;
		private const float BODY_TEXT_PADDING = 8;

		public static GUIStyle createWinStyle(Color titlebarColor)
		{
			GUIStyle winStyle;
			{
				winStyle = new GUIStyle("Window")
				{
					fontSize = (int)Math.Floor(TITTLE_TEXT_SIZE * GameSettings.UI_SCALE),
					fontStyle = FontStyle.Bold,
					alignment = TextAnchor.UpperCenter,
					richText = true,
					wordWrap = false
				};
				winStyle.focused.textColor =
					winStyle.normal.textColor =
					winStyle.active.textColor =
					winStyle.hover.textColor = titlebarColor;
				winStyle.border.top = (int)Math.Floor(TITTLE_BORDER * GameSettings.UI_SCALE);
				{
					Texture2D tex = new Texture2D(1, 1);
					tex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.45f));
					tex.Apply();
					winStyle.normal.background = tex;
				}
			}

			return winStyle;
		}

		public static GUIStyle createTextStyle()
		{
			GUIStyle textStyle;
			{
				textStyle = new GUIStyle("Label")
				{
					fontSize = (int)Math.Floor(BODY_TEXT_SIZE * GameSettings.UI_SCALE),
					fontStyle = FontStyle.Normal,
					alignment = TextAnchor.MiddleLeft,
					richText = true,
					wordWrap = true
				};
				textStyle.focused.textColor =
					textStyle.normal.textColor =
					textStyle.active.textColor =
					textStyle.hover.textColor = Color.white;
				textStyle.padding.top = (int)Math.Floor(BODY_TEXT_PADDING * GameSettings.UI_SCALE);
				textStyle.padding.bottom = textStyle.padding.top;
				textStyle.padding.left = textStyle.padding.top;
				textStyle.padding.right = textStyle.padding.top;
				{
					Texture2D tex = new Texture2D(1, 1);
					tex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.45f));
					tex.Apply();
					textStyle.active.background =
						textStyle.focused.background =
						textStyle.normal.background = tex;
				}
			}
			return textStyle;
		}
	}
}
