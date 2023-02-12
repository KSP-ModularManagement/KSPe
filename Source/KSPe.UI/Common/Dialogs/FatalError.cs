/*
	This file is part of KSPe.UI, a component for KSP Enhanced /L
	© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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
	public class ShowStopperAlertBox : AbstractDialog
	{
		private static readonly string MSG = @"{0}

This is a show-stopper problem. You should click OK to {1}.

If you choose to continue running KSP, your saves may be unrecoverably corrupted, even if it seems to be working correctly. Make back-ups now! You may find the S.A.V.E mod helpful for this.

Your KSP is running from {2}.";

		private static readonly string aMSG = "close KSP, then fix the problem described above";

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
			Show(errorMessage, aMSG, () => {});
		}

		public static void Show(string errorMessage, string actionMessage, Action lambda)
		{
			GameObject go = new GameObject("KSPe.Common.Dialogs.ShowStopperAlertBox");
			MessageBox dlg = go.AddComponent<MessageBox>();

			//GUIStyle win = new GUIStyle(HighLogic.Skin.window)
			GUIStyle win = new GUIStyle("Window")
			{
				fontSize = 26,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperCenter,
				wordWrap = false
			};
			win.normal.textColor = Color.red;
			win.border.top = 0;
			win.padding.top = -5;
			SetWindowBackground(win);
			win.active.background =	win.focused.background = win.normal.background;

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
			SetTextBackground(text);

			// TODO: Shove a MUTEX here to prevent more than one AlertBox to be displayed at the same time!
			dlg.Show(
				"Houston, we have a problem!",
				String.Format(MSG, errorMessage, actionMessage, IO.Path.AppRoot()),
				lambda,
				win, text
			);
		}
	}
}