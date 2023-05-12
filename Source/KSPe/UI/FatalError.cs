/*
	This file is part of KSPe.Loader, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe.Loader is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe.Loader is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe.Loader. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;

using KSPe.Loader.UI;

using UnityEngine;

namespace KSPe.Loader.Dialogs
{
	internal class ShowStopperErrorBox : AbstractDialog
	{
		private const float TITTLE_TEXT_SIZE = 26;
		private const float TITTLE_TEXT_PADDING = -5;
		private const float BODY_TEXT_SIZE = 18;
		private const float BODY_TEXT_PADDING = 8;

		private const string URL = "https://ksp.lisias.net/add-ons/KSPe/KNOWN_ISSUES";
		private static readonly string AMSG = @"get instructions about how to Download and Install KSPe";

		private static readonly string MSG = @"{0}

This is a Show Stopper problem. Your best line of action is to click the OK button to {1}.

KSP <b>can't</b> be run safely and will be closed, and a Web page with further instructions will be opened.

Your KSP is running from:
<i>{2}</i>.";

		public static void Show(string errorMessage)
		{
			Show(
				errorMessage,
				AMSG,
				() => { Application.OpenURL(URL); Application.Quit(); }
			);
			Startup.quitOnDestroy = true;
			Log.detail("\"Houston, we have a Problem!\" was displayed about : {0} . See {1} for details.", errorMessage, URL);
		}

		private static void Show(string errorMessage, string actionMessage, Action lambda)
		{
			GameObject go = new GameObject("KSPe.Common.Diallgs.ShowStopperAlertBox");
			MessageBox dlg = go.AddComponent<MessageBox>();

			//GUIStyle win = new GUIStyle(HighLogic.Skin.window)
			GUIStyle win = new GUIStyle("Window")
			{
				fontSize = (int)Math.Floor(TITTLE_TEXT_SIZE * GameSettings.UI_SCALE),
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperCenter,
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
				"Houston, we have a Problem!",
				String.Format(MSG, errorMessage, actionMessage, Util.SystemTools.Path.ROOT),
				lambda,
				win, text,
				true
			);
		}
	}
}