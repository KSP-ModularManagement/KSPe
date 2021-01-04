/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-21 Lisias T : http://lisias.net <support@lisias.net>

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
	public class TimedMessageBox : MonoBehaviour
	{

		private string title;
		private string msg;
		private int seconds_to_show;
		private int pos_horizontal; // -1 Left  ;  0 Center ; +1 Right
		private int pos_vertical;   // -1 Top ; 0 Center ; +1 Bottom
		private GUIStyle win_style;
		private GUIStyle text_style;
		private int window_id;
		private Rect windowRect;

		public void Show(string title, string msg)
		{
			Show(title, msg, 15, 0, 0, null, null);
		}

		public void Show(string title, string msg, int seconds_to_show)
		{
			Show(title, msg, seconds_to_show, 0, 0, null, null);
		}

		public void Show(string title, string msg, GUIStyle win_style, GUIStyle text_style)
		{
			Show(title, msg, 15, 0, 0, win_style, text_style);
		}

		public void Show(string title, string msg, int seconds_to_show, int pos_horizontal, int pos_vertical, GUIStyle win_style, GUIStyle text_style)
		{
			this.title = title;
			this.msg = msg;
			this.seconds_to_show = seconds_to_show;
			this.pos_horizontal = pos_horizontal;
			this.pos_vertical = pos_vertical;
			this.win_style = win_style;
			this.text_style = text_style;
			this.window_id = (int)WindowUtils.window_id_seed;

			this.windowRect = this.calculateWindow();
		}

		private Rect calculateWindow()
		{
			const int minWidth = 400;
			const int minHeight = 300;

			int width = Mathf.Max(minWidth, Screen.width / 3);
			int height = Mathf.Max(minHeight, Screen.height / 3);
			int win_pos_x = (Screen.width - width) / 2;
			int win_pos_y = (Screen.height - height) / 2;

			int diff_x = Screen.width - width - win_pos_x;
			int diff_y = Screen.height - height - win_pos_y;

			return new Rect(
				win_pos_x + (float)(this.pos_horizontal * diff_x), win_pos_y + (float)(this.pos_vertical * diff_y),
				width, height
			);
		}

		private void OnGUI()
		{
			this.windowRect = this.win_style is null
				? UGUI.Window(this.window_id, this.windowRect, WindowFunc, this.title)
				: UGUI.Window(this.window_id, this.windowRect, WindowFunc, this.title, this.win_style);
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

			if ((0 ==  Event.current.button) && (EventType.MouseUp == Event.current.type))
				this.isTicking = false;

			string label = this.isTicking ? string.Format("{0}", (int)(this.seconds_to_show - this.timer)) : "OK";
			if (UGUI.Button(b, label))
				Destroy(this.gameObject);

			GUI.DragWindow();
		}

		private float timer = 0.0f;
		private bool isTicking = true;
		protected void Update()
		{
			timer += this.isTicking ? Time.smoothDeltaTime : 0;
			if (timer > this.seconds_to_show)
				Destroy(this.gameObject);
		}
	}
}