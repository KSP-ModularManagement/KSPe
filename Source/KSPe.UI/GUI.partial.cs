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
	// Partial Implementation Stub (Prototype) to be used when compiling programs.
	// Need to be complemented on every target Aseembly
	public static partial class GUI
	{
		public static global::UnityEngine.GUISkin skin
		{
			get => global::UnityEngine.GUI.skin;
			set => global::UnityEngine.GUI.skin = value;
		}

		public static Matrix4x4 matrix
		{
			get => global::UnityEngine.GUI.matrix;
			set => global::UnityEngine.GUI.matrix = value;
		}

		public static string tooltip
		{
			get => global::UnityEngine.GUI.tooltip;
			set => global::UnityEngine.GUI.tooltip = value;
		}

		public static Color color
		{
			get => global::UnityEngine.GUI.color;
			set => global::UnityEngine.GUI.color = value;
		}

		public static Color backgroundColor
		{
			get => global::UnityEngine.GUI.backgroundColor;
			set => global::UnityEngine.GUI.backgroundColor = value;
		}

		public static Color contentColor
		{
			get => global::UnityEngine.GUI.contentColor;
			set => global::UnityEngine.GUI.contentColor = value;
		}

		public static bool changed
		{
			get => global::UnityEngine.GUI.changed;
			set => global::UnityEngine.GUI.changed = value;
		}

		public static bool enabled
		{
			get => global::UnityEngine.GUI.enabled;
			set => global::UnityEngine.GUI.enabled = value;
		}

		public static int depth
		{
			get => global::UnityEngine.GUI.depth;
			set => global::UnityEngine.GUI.depth = value;
		}

/****
		public static Rect Window(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, string text);
		public static Rect Window(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, Texture image);
		public static Rect Window(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, GUIContent content);
		public static Rect Window(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, string text, GUIStyle style);
		public static Rect Window(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style);
		public static Rect Window(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, GUIContent title, GUIStyle style);

 		public static Rect ModalWindow(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, string text);
		public static Rect ModalWindow(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, Texture image);
		public static Rect ModalWindow(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, GUIContent content);
		public static Rect ModalWindow(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, string text, GUIStyle style);
		public static Rect ModalWindow(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style);
		public static Rect ModalWindow(int id, Rect clientRect, global::UnityEngine.GUI.WindowFunction func, GUIContent content, GUIStyle style);
*/

		public static void Label(Rect position, string text)						{ global::UnityEngine.GUI.Label(position, text); }
		public static void Label(Rect position, Texture image)						{ global::UnityEngine.GUI.Label(position, image); }
		public static void Label(Rect position, GUIContent content)					{ global::UnityEngine.GUI.Label(position, content); }
		public static void Label(Rect position, string text, GUIStyle style)		{ global::UnityEngine.GUI.Label(position, text, style); }
		public static void Label(Rect position, Texture image, GUIStyle style)		{ global::UnityEngine.GUI.Label(position, image, style); }
		public static void Label(Rect position, GUIContent content, GUIStyle style)	{ global::UnityEngine.GUI.Label(position, content, style); }

		public static void DrawTexture(Rect position, Texture image)															{ global::UnityEngine.GUI.DrawTexture(position, image); }
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)										{ global::UnityEngine.GUI.DrawTexture(position, image, scaleMode); }
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)						{ global::UnityEngine.GUI.DrawTexture(position, image, scaleMode, alphaBlend); }
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)	{ global::UnityEngine.GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect); }

		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)					{ global::UnityEngine.GUI.DrawTextureWithTexCoords(position, image, texCoords); }
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, bool alphaBlend)	{ global::UnityEngine.GUI.DrawTextureWithTexCoords(position, image, texCoords, alphaBlend); }

		public static void Box(Rect position, string text)							{ global::UnityEngine.GUI.Box(position, text); }
		public static void Box(Rect position, Texture image)						{ global::UnityEngine.GUI.Box(position, image); }
		public static void Box(Rect position, GUIContent content)					{ global::UnityEngine.GUI.Box(position, content); }
		public static void Box(Rect position, string text, GUIStyle style)			{ global::UnityEngine.GUI.Box(position, text, style); }
		public static void Box(Rect position, Texture image, GUIStyle style)		{ global::UnityEngine.GUI.Box(position, image, style); }
		public static void Box(Rect position, GUIContent content, GUIStyle style)	{ global::UnityEngine.GUI.Box(position, content, style); }

		public static bool Button(Rect position, string text)								{ return global::UnityEngine.GUI.Button(position, text); }
		public static bool Button(Rect position, Texture image)								{ return global::UnityEngine.GUI.Button(position, image); }
		public static bool Button(Rect position, GUIContent content)						{ return global::UnityEngine.GUI.Button(position, content); }
		public static bool Button(Rect position, string text, GUIStyle style)				{ return global::UnityEngine.GUI.Button(position, text, style); }
		public static bool Button(Rect position, Texture image, GUIStyle style)				{ return global::UnityEngine.GUI.Button(position, image, style); }
		public static bool Button(Rect position, GUIContent content, GUIStyle style)		{ return global::UnityEngine.GUI.Button(position, content, style); }

		public static bool RepeatButton(Rect position, string text)							{ return global::UnityEngine.GUI.RepeatButton(position, text); }
		public static bool RepeatButton(Rect position, Texture image)						{ return global::UnityEngine.GUI.RepeatButton(position, image); }
		public static bool RepeatButton(Rect position, GUIContent content)					{ return global::UnityEngine.GUI.RepeatButton(position, content); }
		public static bool RepeatButton(Rect position, string text, GUIStyle style)			{ return global::UnityEngine.GUI.RepeatButton(position, text, style); }
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)		{ return global::UnityEngine.GUI.RepeatButton(position, image, style); }
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)	{ return global::UnityEngine.GUI.RepeatButton(position, content, style); }

		public static string TextField(Rect position, string text)									{ return global::UnityEngine.GUI.TextField(position, text); }
		public static string TextField(Rect position, string text, int maxLength)					{ return global::UnityEngine.GUI.TextField(position, text, maxLength); }
		public static string TextField(Rect position, string text, GUIStyle style)					{ return global::UnityEngine.GUI.TextField(position, text, style); }
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)	{ return global::UnityEngine.GUI.TextField(position, text, maxLength, style); }

		public static string PasswordField(Rect position, string password, char maskChar)									{ return global::UnityEngine.GUI.PasswordField(position, password, maskChar); }
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)					{ return global::UnityEngine.GUI.PasswordField(position, password, maskChar, maxLength); }
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)					{ return global::UnityEngine.GUI.PasswordField(position, password, maskChar, style); }
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)	{ return global::UnityEngine.GUI.PasswordField(position, password, maskChar, maxLength, style); }

		public static string TextArea(Rect position, string text)									{ return global::UnityEngine.GUI.TextArea(position, text); }
		public static string TextArea(Rect position, string text, int maxLength)					{ return global::UnityEngine.GUI.TextArea(position, text, maxLength); }
		public static string TextArea(Rect position, string text, GUIStyle style)					{ return global::UnityEngine.GUI.TextArea(position, text, style); }
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)	{ return global::UnityEngine.GUI.TextArea(position, text, maxLength, style); }

		public static bool Toggle(Rect position, bool value, string text)									{ return global::UnityEngine.GUI.Toggle(position, value, text); }
		public static bool Toggle(Rect position, bool value, Texture image)									{ return global::UnityEngine.GUI.Toggle(position, value, image); }
		public static bool Toggle(Rect position, bool value, GUIContent content)							{ return global::UnityEngine.GUI.Toggle(position, value, content); }
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)					{ return global::UnityEngine.GUI.Toggle(position, value, text, style); }
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)					{ return global::UnityEngine.GUI.Toggle(position, value, image, style); }
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)			{ return global::UnityEngine.GUI.Toggle(position, value, content, style); }
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)	{ return global::UnityEngine.GUI.Toggle(position, id, value, content, style); }

		public static int Toolbar(Rect position, int selected, string[] texts)							{ return global::UnityEngine.GUI.Toolbar(position, selected, texts); }
		public static int Toolbar(Rect position, int selected, Texture[] images)						{ return global::UnityEngine.GUI.Toolbar(position, selected, images); }
		public static int Toolbar(Rect position, int selected, GUIContent[] contents)					{ return global::UnityEngine.GUI.Toolbar(position, selected, contents); }
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)			{ return global::UnityEngine.GUI.Toolbar(position, selected, texts, style); }
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)		{ return global::UnityEngine.GUI.Toolbar(position, selected, images, style); }
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)	{ return global::UnityEngine.GUI.Toolbar(position, selected, contents, style); }

		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)						{ return global::UnityEngine.GUI.SelectionGrid(position, selected, texts, xCount); }
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)						{ return global::UnityEngine.GUI.SelectionGrid(position, selected, images, xCount); }
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount)					{ return global::UnityEngine.GUI.SelectionGrid(position, selected, contents, xCount); }
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)		{ return global::UnityEngine.GUI.SelectionGrid(position, selected, texts, xCount, style); }
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)		{ return global::UnityEngine.GUI.SelectionGrid(position, selected, images, xCount, style); }
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)	{ return global::UnityEngine.GUI.SelectionGrid(position, selected, contents, xCount, style); }

		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)										{ return global::UnityEngine.GUI.HorizontalSlider(position, value, leftValue, rightValue); }
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)	{ return global::UnityEngine.GUI.HorizontalSlider(position, value, leftValue, rightValue, slider, thumb); }

		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)									{ return global::UnityEngine.GUI.VerticalSlider(position, value, topValue, bottomValue); }
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)	{ return global::UnityEngine.GUI.VerticalSlider(position, value, topValue, bottomValue, slider, thumb); }

		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id) { return global::UnityEngine.GUI.Slider(position, value, size, start, end, slider, thumb, horiz, id); }

		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)					{ return global::UnityEngine.GUI.HorizontalScrollbar(position, value, size, leftValue, rightValue); }
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)	{ return global::UnityEngine.GUI.HorizontalScrollbar(position, value, size, leftValue, rightValue, style); }

		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)					{ return global::UnityEngine.GUI.VerticalScrollbar(position, value, size, topValue, bottomValue); }
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)	{ return global::UnityEngine.GUI.VerticalScrollbar(position, value, size, topValue, bottomValue, style); }

		public static void BeginGroup(Rect position)																{ global::UnityEngine.GUI.BeginGroup(position); }
		public static void BeginGroup(Rect position, string text)													{ global::UnityEngine.GUI.BeginGroup(position, text); }
		public static void BeginGroup(Rect position, Texture image)													{ global::UnityEngine.GUI.BeginGroup(position, image); }
		public static void BeginGroup(Rect position, GUIContent content)											{ global::UnityEngine.GUI.BeginGroup(position, content); }
		public static void BeginGroup(Rect position, GUIStyle style)												{ global::UnityEngine.GUI.BeginGroup(position, style); }
		public static void BeginGroup(Rect position, string text, GUIStyle style)									{ global::UnityEngine.GUI.BeginGroup(position, text, style); }
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)									{ global::UnityEngine.GUI.BeginGroup(position, image, style); }
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)							{ global::UnityEngine.GUI.BeginGroup(position, content, style); }
		public static void EndGroup()																				{ global::UnityEngine.GUI.EndGroup(); }
		public static void BeginClip(Rect position)																	{ global::UnityEngine.GUI.BeginClip(position); }
		public static void BeginClip(Rect position, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)	{ global::UnityEngine.GUI.BeginClip(position, scrollOffset, renderOffset, resetOffset); }
		public static void EndClip()																				{ global::UnityEngine.GUI.EndClip(); }

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)																													{ return global::UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect); }
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)																{ return global::UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical); }
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)														{ return global::UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar); }
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)	{ return global::UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar); }
		public static void EndScrollView()								{ global::UnityEngine.GUI.EndScrollView(); }
		public static void EndScrollView(bool handleScrollWheel)		{ global::UnityEngine.GUI.EndScrollView(handleScrollWheel); }
		public static void ScrollTo(Rect position)						{ global::UnityEngine.GUI.ScrollTo(position); }
		public static bool ScrollTowards(Rect position, float maxDelta)	{ return global::UnityEngine.GUI.ScrollTowards(position, maxDelta); }

		public static void SetNextControlName(string name)	{ global::UnityEngine.GUI.SetNextControlName(name); }
		public static string GetNameOfFocusedControl()		{ return global::UnityEngine.GUI.GetNameOfFocusedControl(); }
		public static void FocusControl(string name)		{ global::UnityEngine.GUI.FocusControl(name); }

		public static void DragWindow()						{ global::UnityEngine.GUI.DragWindow(); }
		public static void DragWindow(Rect position)		{ global::UnityEngine.GUI.DragWindow(position); }
		public static void BringWindowToFront(int windowID)	{ global::UnityEngine.GUI.BringWindowToFront(windowID); }
		public static void BringWindowToBack(int windowID)	{ global::UnityEngine.GUI.BringWindowToBack(windowID); }
		public static void FocusWindow(int windowID)		{ global::UnityEngine.GUI.FocusWindow(windowID); }
		public static void UnfocusWindow()					{ global::UnityEngine.GUI.UnfocusWindow(); }
	}
}
