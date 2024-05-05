/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;

using KSPe.Common.Dialogs;
using KSPe.UI;

using UnityEngine;

namespace KSPe.InstallChecker.GUI.Dialogs
{
	internal class WarningAlertBox : AbstractDialog
	{
		private static readonly string aMSG = "close KSP and then fix the problem described above";

		private static readonly string MSG = @"{0}

Your need to click the Close button to {1}.

You <b>should not</b> ignore this message, otherwise some critical features will not be loaded and your savegames will get corrupted <b>for sure</b>!
";

		public static void Show(string errorMessage)
		{
			Show(errorMessage, aMSG, Application.Quit);
		}

		public static void Show(string errorMessage, string actionMessage, Action lambda, bool noCancel = false)
		{
			GameObject go = new GameObject(typeof(WarningAlertBox).FullName);
			MessageBox dlg = go.AddComponent<MessageBox>();

			//GUIStyle win = new GUIStyle(HighLogic.Skin.window)
			GUIStyle win = new GUIStyle("Window")
			{
				fontSize = 26,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperCenter,
				richText = true,
				wordWrap = false
			};
			win.normal.textColor = Color.yellow;
			win.border.top = 0;
			win.padding.top = -5;
			SetWindowBackground(win);
			win.active.background =	win.focused.background = win.normal.background;

			GUIStyle text = new GUIStyle("Label")
			{
				fontSize = 18,
				fontStyle = FontStyle.Normal,
				alignment = TextAnchor.MiddleLeft,
				richText = true,
				wordWrap = true
			};
			text.normal.textColor = Color.white;
			text.padding.top = 8;
			text.padding.bottom = text.padding.top;
			text.padding.left = text.padding.top;
			text.padding.right = text.padding.top;
			SetTextBackground(text);

			dlg.Show(
				"Your Attention Please!",
				String.Format(MSG, errorMessage, actionMessage),
				lambda,
				win, text,
				noCancel
			);
		}
	}
}
