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
using UnityEngine;

namespace KSPe.UI
{
	// Implementation Stub (Prototype) to be used when compiling programs.
	// Will be ignored at runtime, and replaced by a KSP speciliaed realization.
	public static partial class GUI
	{
		private static readonly Rect RECT = new Rect();
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text)						{ return RECT; }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image)						{ return RECT; }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content)					{ return RECT; }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style)		{ return RECT; }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style)		{ return RECT; }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent title, GUIStyle style)	{ return RECT; }

		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text)							{ return RECT; }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image)							{ return RECT; }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content)					{ return RECT; }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style)			{ return RECT; }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style)			{ return RECT; }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content, GUIStyle style)	{ return RECT; }
	}
}
