/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-19 Lisias T : http://lisias.net <support@lisias.net>

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
using UnityEngine;
using KSPe.UI;

namespace KSPe.Common.Dialogs
{
	public static class ShowStopperAlertBox
	{
		private static readonly string aMSG = "close KSP and then fix the problem described above";

		private static readonly string MSG = @"{0}

This is a Show Stopper problem. Your best line of action is to click the OK button to {1}.

If you choose to ignore this message and click Cancel to proceed, be advised that your savegames can get corrupted at any time, even when things appear to work by now - and the salvage can be harder.

Backup everything *NOW* if you choose to ignore this message and proceed - it's recommended to use S.A.V.E. to automate this task for you.";

		public static void Show(KSPe.Util.AbstractException ex)
		{
			Show(ex.ToLongMessage());
		}

		public static void Show(KSPe.Util.AbstractException ex, string actionMessage, Action lambda)
		{
			Show(ex.ToLongMessage(), actionMessage, lambda);
		}

		public static void Show(string errorMessage)
		{
			Show(errorMessage, aMSG, Application.Quit);
		}

		public static void Show(string errorMessage, string actionMessage, Action lambda)
		{
			GameObject go = new GameObject("KSPe.Common.Diallgs.ShowStopperAlertBox");
			MessageBox dlg = go.AddComponent<MessageBox>();

			GUIStyle win = new GUIStyle("Window")
			{
				fontSize = 26,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperCenter,
				wordWrap = false
			};
			win.normal.textColor = Color.red;
			win.border.top = 36;

			GUIStyle text = new GUIStyle("Label")
			{
				fontSize = 18,
				fontStyle = FontStyle.Normal,
				alignment = TextAnchor.MiddleLeft,
				wordWrap = true
			};
			text.normal.textColor = Color.white;
			text.padding.top = 8;
			text.padding.bottom = text.padding.top;
			text.padding.left = text.padding.top;
			text.padding.right = text.padding.top;
			{
				Texture2D tex = new Texture2D(1, 1);
				tex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.45f));
				tex.Apply();
				text.normal.background = tex;
			}

			dlg.Show(
				"Houston, we have a Problem!",
				String.Format(MSG, errorMessage, actionMessage),
				lambda,
				win, text
			);
		}
	}
}