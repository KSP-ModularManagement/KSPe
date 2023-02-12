﻿/*
	This file is part of KSPe.UI.12, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP Enhanced /L. If not, see <https://www.gnu.org/licenses/>.

*/
using UnityEngine;
 
namespace KSPe.KSP12.UI
{ 
	public class GUILayout : KSPe.UI.GUILayout.Interface
	{
		public Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, string text, params GUILayoutOption[] options)                         { return UnityEngine.GUILayout.Window(id, screenRect, func, text, options); }
		public Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)                       { return UnityEngine.GUILayout.Window(id, screenRect, func, image, options); }
		public Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)                  { return UnityEngine.GUILayout.Window(id, screenRect, func, content, options); }
		public Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)         { return UnityEngine.GUILayout.Window(id, screenRect, func, text, style, options); }

		public Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)       { return UnityEngine.GUILayout.Window(id, screenRect, func, image, style, options); }
		public Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)  { return UnityEngine.GUILayout.Window(id, screenRect, func, content, style, options); }
	}
}