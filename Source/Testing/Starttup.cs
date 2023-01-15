/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-2023 Lisias T : http://lisias.net <support@lisias.net>

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
using GUILayout = KSPe.UI.GUILayout;
using GUI = KSPe.UI.GUI;

namespace KSPe.Testing
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	public class MainMenu : MonoBehaviour
	{
		private Rect window;

		public void Awake()
		{
			this.window = new Rect(400, 300, 320, 200);
		}

		public void OnGUI()
		{
			this.window = GUI.Window(0, this.window, OnMainWindow, "It's me, Mario!!");
		}

		private void OnMainWindow(int windowID)
		{
			GUIStyle style = new GUIStyle(GUI.skin.toggle);
			GUILayout.BeginVertical();
			GUILayout.TextArea("Hello World!", GUILayout.ExpandHeight(true), GUILayout.MaxHeight(100));
			GUILayout.EndVertical();
			GUI.DragWindow();
		}
	}
}
