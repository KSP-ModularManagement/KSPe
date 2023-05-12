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
using UnityEngine;
using UGUI = UnityEngine.GUI;

namespace KSPe.Loader.UI
{
	internal class MessageBox : MonoBehaviour
	{
		private string title;
		private string msg;
		private Action action;
		private bool noCancel;
		private GUIStyle win_style;
		private GUIStyle text_style;
		private int window_id;
		private Rect windowRect;

		public void Show(string title, string msg)
		{
			Show(title, msg, null, null, null);
		}

		// MessageBox("SNAFU", "Situation Normal... All F* Up!", () => { Application.Quit() });
		public void Show(string title, string msg, Action action)
		{
			Show(title, msg, action, null, null);
		}

		public void Show(string title, string msg, GUIStyle win_style, GUIStyle text_style)
		{
			Show(title, msg, null, win_style, text_style);
		}

		public void Show(string title, string msg, Action action, GUIStyle win_style, GUIStyle text_style)
		{
			Show(title, msg, action, win_style, text_style, false);
		}

		public void Show(string title, string msg, Action action, GUIStyle win_style, GUIStyle text_style, bool noCancel)
		{
			this.title = title;
			this.msg = msg;
			this.action = action;
			this.noCancel = noCancel;
			this.win_style = win_style;
			this.text_style = text_style;
			this.window_id = (int)WindowUtils.window_id_seed;

			this.windowRect = this.calculateWindow();
		}

		private Rect calculateWindow()
		{
			int maxWidth = (int)Math.Floor(640 * GameSettings.UI_SCALE);
			int maxHeight = (int)Math.Floor(480 * GameSettings.UI_SCALE);

			int width = Mathf.Min(maxWidth, Screen.width - 20);
			int height = Mathf.Min(maxHeight, Screen.height - 20);
			
			return new Rect(
				(Screen.width - width) / 2, (Screen.height - height) / 2,
				width, height
			);

		}

		private void OnGUI()
		{
			this.windowRect = this.win_style is null
				? UGUI.ModalWindow(this.window_id, this.windowRect, WindowFunc, this.title)
				: UGUI.ModalWindow(this.window_id, this.windowRect, WindowFunc, this.title, this.win_style);
		}

		private void WindowFunc(int windowID)
		{
			int border = (int)Math.Floor(10 * GameSettings.UI_SCALE);
			int width = (int)Math.Floor(50 * GameSettings.UI_SCALE);
			int height = (int)Math.Floor(25 * GameSettings.UI_SCALE);
			int spacing = (int)Math.Floor(10 * GameSettings.UI_SCALE);

			Rect l = new Rect(
					border, border + spacing,
					this.windowRect.width - border * 2, this.windowRect.height - border * 2 - height - spacing
				);

			if (this.text_style is null)
				UGUI.Label(l, this.msg);
			else
				UGUI.Label(l, this.msg, this.text_style);

			Rect b = new Rect(
				this.windowRect.width - width - border,
				this.windowRect.height - height - border,
				width,
				height);

			if (this.action is null)
			{
				if (UGUI.Button(b, "OK")) Destroy(this.gameObject);
			}
			else
			{
				if (UGUI.Button(b, "OK")) { this.action(); Destroy(this.gameObject); }
				if (!noCancel)
				{ 
					Rect b1 = new Rect(b);
					b1.x -= b.width + spacing;
					if (UGUI.Button(b1, "Cancel")) Destroy(this.gameObject);
				}
			}
		}
	}
}