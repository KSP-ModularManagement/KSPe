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
	public class MessageBox : MonoBehaviour {
	
	    private string title;
	    private string msg;
	    private Action action;
	
	    private Rect windowRect;
	
	    // MessageBox("SNAFU", "Situation Normal... All F* Up!", () => { Application.Quit() });
	    public void Show(string title, string msg, Action action)
	    {
	        this.title = title;
	        this.msg = msg;
	        this.action = action;
	    }
	
	    private void OnGUI()
	    {
	        const int maxWidth = 640;
	        const int maxHeight = 480;
	
	        int width = Mathf.Min(maxWidth, Screen.width - 20);
	        int height = Mathf.Min(maxHeight, Screen.height - 20);
	        this.windowRect = new Rect(
	            (Screen.width - width) / 2, (Screen.height - height) / 2,
	            width, height
	        );
	
	        this.windowRect = UGUI.Window(0, this.windowRect, WindowFunc, this.title);
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
	        UGUI.Label(l, this.msg);
	
	        Rect b = new Rect(
	            this.windowRect.width - width - border,
	            this.windowRect.height - height - border,
	            width,
	            height);
	
	        if (UGUI.Button(b, "OK"))
	        {
	            if (!(this.action is null)) this.action();
                Destroy(this.gameObject);
	        }
	
	    }
	} 
}