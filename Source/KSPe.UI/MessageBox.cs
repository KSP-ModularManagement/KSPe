/*
	This file is part of KSPe.UI, a component for KSP API Extensions/L
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
			this.title = title;
			this.msg = msg;
			this.action = action;
			this.win_style = win_style;
			this.text_style = text_style;
			this.window_id = (int)WindowUtils.window_id_seed;
		}

		private void OnGUI()
		{
			const int maxWidth = 640;
			const int maxHeight = 480;

			int width = Mathf.Min(maxWidth, Screen.width - 20);
			int height = Mathf.Min(maxHeight, Screen.height - 20);
			Rect windowRect = new Rect(
				(Screen.width - width) / 2, (Screen.height - height) / 2,
				width, height
			);

			this.windowRect = this.win_style is null
				? UGUI.ModalWindow(this.window_id, windowRect, WindowFunc, this.title)
				: UGUI.ModalWindow(this.window_id, windowRect, WindowFunc, this.title, this.win_style);
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

			if (this.action is null)
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