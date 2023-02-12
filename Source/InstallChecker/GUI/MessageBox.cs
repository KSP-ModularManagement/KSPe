/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;
using UnityEngine;
using UGUI = UnityEngine.GUI;

namespace KSPe.UI
{
	public class MessageBox : MonoBehaviour
	{
		private string title;
		private string msg;
		private Action action;
		private GUIStyle win_style;
		private GUIStyle text_style;
		private bool no_cancel;
		private int window_id;
		private Rect windowRect;

		public void Show(string title, string msg)
		{
			Show(title, msg, null, null, null, false);
		}

		public void Show(string title, string msg, Action action)
		{
			Show(title, msg, action, null, null, false);
		}

		public void Show(string title, string msg, GUIStyle win_style, GUIStyle text_style)
		{
			Show(title, msg, null, win_style, text_style, false);
		}

		public void Show(string title, string msg, Action action, GUIStyle win_style, GUIStyle text_style, bool noCancel)
		{
			this.title = title;
			this.msg = msg;
			this.action = action;
			this.win_style = win_style;
			this.text_style = text_style;
			this.no_cancel = noCancel;
			this.window_id = (int)System.DateTime.Now.Ticks;

			this.windowRect = this.calculateWindow();
		}

		private Rect calculateWindow()
		{
			const int maxWidth = 640;
			const int maxHeight = 480;

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
			const int border = 10;
			const int width = 50;
			const int height = 25;
			const int spacing = 10;

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

			if (this.no_cancel)
			{
				if (UGUI.Button(b, "Close")) { this.action(); Destroy(this.gameObject); }
			}
			else if (this.action is null)
			{
				if (UGUI.Button(b, "OK")) Destroy(this.gameObject);
			}
			else
			{
				if (UGUI.Button(b, "OK")) { this.action(); Destroy(this.gameObject); }

				Rect b1 = new Rect(b);
				b1.x -= b.width + spacing;
				if (UGUI.Button(b1, "Cancel")) Destroy(this.gameObject);
			}
		}
	}
}
