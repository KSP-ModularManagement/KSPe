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
using UnityEngine;

namespace KSPe.UI
{
	public static class GUI
	{
		public static UnityEngine.GUISkin skin
        {
            get => UnityEngine.GUI.skin;
            set => UnityEngine.GUI.skin = value;
        }
        
        public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style)
        {
            return ClickThroughFix.ClickThruBlocker.GUIWindow(id, clientRect, func, image, style);
        }
        
        public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style)
        {
            return ClickThroughFix.ClickThruBlocker.GUIWindow(id, clientRect, func, text, style);
        }

        public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content)
        {
            return ClickThroughFix.ClickThruBlocker.GUIWindow(id, clientRect, func, content);
        }

        public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image)
        {
            return ClickThroughFix.ClickThruBlocker.GUIWindow(id, clientRect, func, image);
        }

        public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text)
        {
            return ClickThroughFix.ClickThruBlocker.GUIWindow(id, clientRect, func, text);
        }

        public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent title, GUIStyle style)
        {
            return ClickThroughFix.ClickThruBlocker.GUIWindow(id, clientRect, func, title, style);
        }
    }
}
