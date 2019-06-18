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
	public class TimedMessageBox : MonoBehaviour {
	
	    private string title;
	    private string msg;
	    private int seconds_to_show;
	    private int pos_horizontal; // -1 Left  ;  0 Center ; +1 Right
	    private int pos_vertical;   // -1 Top ; 0 Center ; +1 Bottom
	    private GUIStyle win_style;
	    private GUIStyle text_style;
	
	    private Rect windowRect;
	
	    public void Show(string title, string msg)
	    {
	        Show(title, msg, 15, 0, 0, null, null);
	    }
	
    // MessageBox("SNAFU", "Situation Normal... All F* Up!", () => { Application.Quit() });
	    public void Show(string title, string msg, int seconds_to_show)
	    {
	        Show(title, msg, seconds_to_show, 0, 0, null, null);
	    }
	
	    public void Show(string title, string msg, GUIStyle win_style, GUIStyle text_style)
	    {
	        Show(title, msg, 15, 0, 0, win_style, text_style);
	    }
	
	    public void Show(string title, string msg, int seconds_to_show ,int pos_horizontal, int pos_vertical, GUIStyle win_style, GUIStyle text_style)
	    {
	        this.title = title;
	        this.msg = msg;
	        this.seconds_to_show = seconds_to_show;
	        this.pos_horizontal = pos_horizontal;
	        this.pos_vertical = pos_vertical;
	        this.win_style = win_style;
	        this.text_style = text_style;
	    }
	
	    private void OnGUI()
	    {
	        const int maxWidth = 480;
	        const int maxHeight = 360;
	
	        int width = Mathf.Min(maxWidth, Screen.width - 20);
	        int height = Mathf.Min(maxHeight, Screen.height - 20);
	        int win_pos_x = (Screen.width - width) / 2;
	        int win_pos_y = (Screen.height - height) / 2;
	        this.windowRect = new Rect(
	            win_pos_x + (float)(this.pos_horizontal * win_pos_x * .80), win_pos_y + (float)(this.pos_vertical * win_pos_y * .80),
	            width, height
	        );

            this.windowRect = this.win_style is null
                ? UGUI.Window(0, this.windowRect, WindowFunc, this.title)
                : UGUI.Window(0, this.windowRect, WindowFunc, this.title, this.win_style);
        }

        private void WindowFunc(int windowID)
	    {
	        const int border = 10;
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
	    }
	    
	    private float timer = 0.0f;
        protected void Update()
		{
			timer += Time.smoothDeltaTime;
            if (timer > this.seconds_to_show)
                Destroy(this.gameObject);
        }
	} 
}