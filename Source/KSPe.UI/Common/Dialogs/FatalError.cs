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
	// TODO: Remove on Version 2.7
	[System.Obsolete("KSPe.Common.Dialogs.ShowStopperAlertBox is deprecated, please use KSPe.Common.Dialogs.ShowStopperErrorBox instead.")]
	public class ShowStopperAlertBox : ShowStopperErrorBox { }

	public class ShowStopperErrorBox : AbstractDialog
	{
		private const float TITTLE_TEXT_SIZE = 26;
		private const float TITTLE_TEXT_PADDING = -5;
		private const float BODY_TEXT_SIZE = 18;
		private const float BODY_TEXT_PADDING = 8;

		private static readonly string aMSG = "close KSP and then fix the problem described above";

		private static readonly string MSG = @"{0}

This is a Show Stopper problem. Your best line of action is to click the OK button to {1}. If you choose to ignore this message and click Cancel to proceed, be advised that your savegames can get corrupted at any time, even when things appear to work by now. Backup everything <b>NOW</b> if you choose to ignore this message and proceed.

Your KSP is running from:
<b>{2}</b>.";

		public static void Show(KSPe.Util.AbstractException ex)
		{
			if (null != ex.lambda)	Show(ex, ex.actionMessage??aMSG, ex.lambda);
			else					Show(ex, ex.actionMessage??aMSG, () => { Application.Quit(); });
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
			GameObject go = new GameObject("KSPe.Common.Diallgs.ShowStopperAlertBox");
			MessageBox dlg = go.AddComponent<MessageBox>();

			//GUIStyle win = new GUIStyle(HighLogic.Skin.window)
			GUIStyle win = new GUIStyle("Window")
			{
				fontSize = (int)Math.Floor(TITTLE_TEXT_SIZE * GameSettings.UI_SCALE),
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperCenter,
				richText = true,
				wordWrap = false
			};
			win.normal.textColor = Color.red;
			win.border.top = 0;
			win.padding.top = (int)Math.Floor(TITTLE_TEXT_PADDING * GameSettings.UI_SCALE);
			SetWindowBackground(win);
			win.active.background =	win.focused.background = win.normal.background;

			GUIStyle text = new GUIStyle("Label")
			{
				fontSize = (int)Math.Floor(BODY_TEXT_SIZE * GameSettings.UI_SCALE),
				fontStyle = FontStyle.Normal,
				alignment = TextAnchor.MiddleLeft,
				richText = true,
				wordWrap = true,
			};
			text.normal.textColor = Color.white;
			text.padding.top = (int)Math.Floor(BODY_TEXT_PADDING * GameSettings.UI_SCALE);
			text.padding.bottom = text.padding.top;
			text.padding.left = text.padding.top;
			text.padding.right = text.padding.top;
			SetTextBackground(text);

			// TODO: Shove a MUTEX here to prevent more than one AlertBox to be displayed at the same time!
			dlg.Show(
				"Houston, we have a Problem!",
				String.Format(MSG, errorMessage, actionMessage, IO.Path.AppRoot()),
				lambda,
				win, text
			);
		}
	}
}