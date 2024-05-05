﻿/*
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
	public class ErrorAlertBox : AbstractDialog
	{
		private const float TITTLE_TEXT_SIZE = 26;
		private const float TITTLE_TEXT_PADDING = -5;
		private const float BODY_TEXT_SIZE = 18;
		private const float BODY_TEXT_PADDING = 8;

		private static readonly string MSG = @"{0}

It's not wise to keep running KSP, as undesired side effects may happen. Make back-ups now!

Your KSP is running from:
<i>{1}</i>.";

		public static void Show(KSPe.Util.AbstractException ex)
		{
			Show(ex.ToLongMessage());
		}

		public static void Show(KSPe.Util.AbstractException ex, string actionMessage, Action lambda)
		{
			Show(ex.ToLongMessage(), lambda);
		}

		public static void Show(string errorMessage)
		{
			Show(errorMessage, Application.Quit);
		}

		public static void Show(string errorMessage, Action lambda)
		{
			GameObject go = new GameObject("KSPe.Common.Dialogs.ErrorAlertBox");
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
				wordWrap = true,
				richText = true
			};
			text.normal.textColor = Color.white;
			text.padding.top = (int)Math.Floor(BODY_TEXT_PADDING * GameSettings.UI_SCALE);
			text.padding.bottom = text.padding.top;
			text.padding.left = text.padding.top;
			text.padding.right = text.padding.top;
			SetTextBackground(text);

			// TODO: Shove a MUTEX here to prevent more than one AlertBox to be displayed at the same time!
			dlg.Show(
				"Houston, we have a problem!",
				String.Format(MSG, errorMessage, IO.Path.AppRoot()),
				lambda,
				win, text
			);
		}
	}
}