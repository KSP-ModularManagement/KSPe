/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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
	// This using is useless #tumdumtss but I put it here to remember how a headache
	// this one is going to give me on the field!
	// My derived *Scope classes can't inherit from an hypotethical KSPe.UI.GUI.Scope,
	// as it will break the inheritance from UnityEngine.GUI.Scope...
	using Scope = UnityEngine.GUI.Scope;

	// This one too, by similar reasons
	using WindowFunction = UnityEngine.GUI.WindowFunction;

	public static class GUI
	{
		public interface Interface
		{
			Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text);
			Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image);
			Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content);
			Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style);
			Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style);
			Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent title, GUIStyle style);
		}

		public static UnityEngine.GUISkin skin
		{
			get => UnityEngine.GUI.skin;
			set => UnityEngine.GUI.skin = value;
		}

		public static Matrix4x4 matrix
		{
			get => UnityEngine.GUI.matrix;
			set => UnityEngine.GUI.matrix = value;
		}

		public static string tooltip
		{
			get => UnityEngine.GUI.tooltip;
			set => UnityEngine.GUI.tooltip = value;
		}

		public static Color color
		{
			get => UnityEngine.GUI.color;
			set => UnityEngine.GUI.color = value;
		}

		public static Color backgroundColor
		{
			get => UnityEngine.GUI.backgroundColor;
			set => UnityEngine.GUI.backgroundColor = value;
		}

		public static Color contentColor
		{
			get => UnityEngine.GUI.contentColor;
			set => UnityEngine.GUI.contentColor = value;
		}

		public static bool changed
		{
			get => UnityEngine.GUI.changed;
			set => UnityEngine.GUI.changed = value;
		}

		public static bool enabled
		{
			get => UnityEngine.GUI.enabled;
			set => UnityEngine.GUI.enabled = value;
		}

		public static int depth
		{
			get => UnityEngine.GUI.depth;
			set => UnityEngine.GUI.depth = value;
		}

	#region Abstracted Calls

		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text)						{ return INSTANCE.Window(id, clientRect, func, text); }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image)						{ return INSTANCE.Window(id, clientRect, func, image); }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content)					{ return INSTANCE.Window(id, clientRect, func, content); }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style)		{ return INSTANCE.Window(id, clientRect, func, text, style); }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style)		{ return INSTANCE.Window(id, clientRect, func, image, style); }
		public static Rect Window(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent title, GUIStyle style)	{ return INSTANCE.Window(id, clientRect, func, title, style); }

	#endregion

		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text)							{ return UnityEngine.GUI.ModalWindow(id, clientRect, func, text); }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image)							{ return UnityEngine.GUI.ModalWindow(id, clientRect, func, image); }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content)					{ return UnityEngine.GUI.ModalWindow(id, clientRect, func, content); }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style)			{ return UnityEngine.GUI.ModalWindow(id, clientRect, func, text, style); }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style)			{ return UnityEngine.GUI.ModalWindow(id, clientRect, func, image, style); }
		public static Rect ModalWindow(int id, Rect clientRect, UnityEngine.GUI.WindowFunction func, GUIContent content, GUIStyle style)	{ return UnityEngine.GUI.ModalWindow(id, clientRect, func, content, style); }

		public static void Label(Rect position, string text)						{ UnityEngine.GUI.Label(position, text); }
		public static void Label(Rect position, Texture image)						{ UnityEngine.GUI.Label(position, image); }
		public static void Label(Rect position, GUIContent content)					{ UnityEngine.GUI.Label(position, content); }
		public static void Label(Rect position, string text, GUIStyle style)		{ UnityEngine.GUI.Label(position, text, style); }
		public static void Label(Rect position, Texture image, GUIStyle style)		{ UnityEngine.GUI.Label(position, image, style); }
		public static void Label(Rect position, GUIContent content, GUIStyle style)	{ UnityEngine.GUI.Label(position, content, style); }

		public static void DrawTexture(Rect position, Texture image)															{ UnityEngine.GUI.DrawTexture(position, image); }
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)										{ UnityEngine.GUI.DrawTexture(position, image, scaleMode); }
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)						{ UnityEngine.GUI.DrawTexture(position, image, scaleMode, alphaBlend); }
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend, float imageAspect)	{ UnityEngine.GUI.DrawTexture(position, image, scaleMode, alphaBlend, imageAspect); }

		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)					{ UnityEngine.GUI.DrawTextureWithTexCoords(position, image, texCoords); }
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, bool alphaBlend)	{ UnityEngine.GUI.DrawTextureWithTexCoords(position, image, texCoords, alphaBlend); }

		public static void Box(Rect position, string text)							{ UnityEngine.GUI.Box(position, text); }
		public static void Box(Rect position, Texture image)						{ UnityEngine.GUI.Box(position, image); }
		public static void Box(Rect position, GUIContent content)					{ UnityEngine.GUI.Box(position, content); }
		public static void Box(Rect position, string text, GUIStyle style)			{ UnityEngine.GUI.Box(position, text, style); }
		public static void Box(Rect position, Texture image, GUIStyle style)		{ UnityEngine.GUI.Box(position, image, style); }
		public static void Box(Rect position, GUIContent content, GUIStyle style)	{ UnityEngine.GUI.Box(position, content, style); }

		public static bool Button(Rect position, string text)								{ return UnityEngine.GUI.Button(position, text); }
		public static bool Button(Rect position, Texture image)								{ return UnityEngine.GUI.Button(position, image); }
		public static bool Button(Rect position, GUIContent content)						{ return UnityEngine.GUI.Button(position, content); }
		public static bool Button(Rect position, string text, GUIStyle style)				{ return UnityEngine.GUI.Button(position, text, style); }
		public static bool Button(Rect position, Texture image, GUIStyle style)				{ return UnityEngine.GUI.Button(position, image, style); }
		public static bool Button(Rect position, GUIContent content, GUIStyle style)		{ return UnityEngine.GUI.Button(position, content, style); }

		public static bool RepeatButton(Rect position, string text)							{ return UnityEngine.GUI.RepeatButton(position, text); }
		public static bool RepeatButton(Rect position, Texture image)						{ return UnityEngine.GUI.RepeatButton(position, image); }
		public static bool RepeatButton(Rect position, GUIContent content)					{ return UnityEngine.GUI.RepeatButton(position, content); }
		public static bool RepeatButton(Rect position, string text, GUIStyle style)			{ return UnityEngine.GUI.RepeatButton(position, text, style); }
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)		{ return UnityEngine.GUI.RepeatButton(position, image, style); }
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)	{ return UnityEngine.GUI.RepeatButton(position, content, style); }

		public static string TextField(Rect position, string text)									{ return UnityEngine.GUI.TextField(position, text); }
		public static string TextField(Rect position, string text, int maxLength)					{ return UnityEngine.GUI.TextField(position, text, maxLength); }
		public static string TextField(Rect position, string text, GUIStyle style)					{ return UnityEngine.GUI.TextField(position, text, style); }
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)	{ return UnityEngine.GUI.TextField(position, text, maxLength, style); }

		public static string PasswordField(Rect position, string password, char maskChar)									{ return UnityEngine.GUI.PasswordField(position, password, maskChar); }
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)					{ return UnityEngine.GUI.PasswordField(position, password, maskChar, maxLength); }
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)					{ return UnityEngine.GUI.PasswordField(position, password, maskChar, style); }
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)	{ return UnityEngine.GUI.PasswordField(position, password, maskChar, maxLength, style); }

		public static string TextArea(Rect position, string text)									{ return UnityEngine.GUI.TextArea(position, text); }
		public static string TextArea(Rect position, string text, int maxLength)					{ return UnityEngine.GUI.TextArea(position, text, maxLength); }
		public static string TextArea(Rect position, string text, GUIStyle style)					{ return UnityEngine.GUI.TextArea(position, text, style); }
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)	{ return UnityEngine.GUI.TextArea(position, text, maxLength, style); }

		public static bool Toggle(Rect position, bool value, string text)									{ return UnityEngine.GUI.Toggle(position, value, text); }
		public static bool Toggle(Rect position, bool value, Texture image)									{ return UnityEngine.GUI.Toggle(position, value, image); }
		public static bool Toggle(Rect position, bool value, GUIContent content)							{ return UnityEngine.GUI.Toggle(position, value, content); }
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)					{ return UnityEngine.GUI.Toggle(position, value, text, style); }
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)					{ return UnityEngine.GUI.Toggle(position, value, image, style); }
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)			{ return UnityEngine.GUI.Toggle(position, value, content, style); }
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)	{ return UnityEngine.GUI.Toggle(position, id, value, content, style); }

		public static int Toolbar(Rect position, int selected, string[] texts)							{ return UnityEngine.GUI.Toolbar(position, selected, texts); }
		public static int Toolbar(Rect position, int selected, Texture[] images)						{ return UnityEngine.GUI.Toolbar(position, selected, images); }
		public static int Toolbar(Rect position, int selected, GUIContent[] contents)					{ return UnityEngine.GUI.Toolbar(position, selected, contents); }
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)			{ return UnityEngine.GUI.Toolbar(position, selected, texts, style); }
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)		{ return UnityEngine.GUI.Toolbar(position, selected, images, style); }
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)	{ return UnityEngine.GUI.Toolbar(position, selected, contents, style); }

		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)						{ return UnityEngine.GUI.SelectionGrid(position, selected, texts, xCount); }
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)						{ return UnityEngine.GUI.SelectionGrid(position, selected, images, xCount); }
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount)					{ return UnityEngine.GUI.SelectionGrid(position, selected, contents, xCount); }
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)		{ return UnityEngine.GUI.SelectionGrid(position, selected, texts, xCount, style); }
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)		{ return UnityEngine.GUI.SelectionGrid(position, selected, images, xCount, style); }
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)	{ return UnityEngine.GUI.SelectionGrid(position, selected, contents, xCount, style); }

		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)										{ return UnityEngine.GUI.HorizontalSlider(position, value, leftValue, rightValue); }
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)	{ return UnityEngine.GUI.HorizontalSlider(position, value, leftValue, rightValue, slider, thumb); }

		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)									{ return UnityEngine.GUI.VerticalSlider(position, value, topValue, bottomValue); }
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)	{ return UnityEngine.GUI.VerticalSlider(position, value, topValue, bottomValue, slider, thumb); }

		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id) { return UnityEngine.GUI.Slider(position, value, size, start, end, slider, thumb, horiz, id); }

		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)					{ return UnityEngine.GUI.HorizontalScrollbar(position, value, size, leftValue, rightValue); }
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)	{ return UnityEngine.GUI.HorizontalScrollbar(position, value, size, leftValue, rightValue, style); }

		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)					{ return UnityEngine.GUI.VerticalScrollbar(position, value, size, topValue, bottomValue); }
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)	{ return UnityEngine.GUI.VerticalScrollbar(position, value, size, topValue, bottomValue, style); }

		public static void BeginGroup(Rect position)																{ UnityEngine.GUI.BeginGroup(position); }
		public static void BeginGroup(Rect position, string text)													{ UnityEngine.GUI.BeginGroup(position, text); }
		public static void BeginGroup(Rect position, Texture image)													{ UnityEngine.GUI.BeginGroup(position, image); }
		public static void BeginGroup(Rect position, GUIContent content)											{ UnityEngine.GUI.BeginGroup(position, content); }
		public static void BeginGroup(Rect position, GUIStyle style)												{ UnityEngine.GUI.BeginGroup(position, style); }
		public static void BeginGroup(Rect position, string text, GUIStyle style)									{ UnityEngine.GUI.BeginGroup(position, text, style); }
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)									{ UnityEngine.GUI.BeginGroup(position, image, style); }
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)							{ UnityEngine.GUI.BeginGroup(position, content, style); }
		public static void EndGroup()																				{ UnityEngine.GUI.EndGroup(); }
		public static void BeginClip(Rect position)																	{ UnityEngine.GUI.BeginClip(position); }
		public static void BeginClip(Rect position, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)	{ UnityEngine.GUI.BeginClip(position, scrollOffset, renderOffset, resetOffset); }
		public static void EndClip()																				{ UnityEngine.GUI.EndClip(); }

		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)																													{ return UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect); }
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)																{ return UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical); }
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)														{ return UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar); }
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)	{ return UnityEngine.GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar); }
		public static void EndScrollView()								{ UnityEngine.GUI.EndScrollView(); }
		public static void EndScrollView(bool handleScrollWheel)		{ UnityEngine.GUI.EndScrollView(handleScrollWheel); }
		public static void ScrollTo(Rect position)						{ UnityEngine.GUI.ScrollTo(position); }
		public static bool ScrollTowards(Rect position, float maxDelta)	{ return UnityEngine.GUI.ScrollTowards(position, maxDelta); }

		public static void SetNextControlName(string name)	{ UnityEngine.GUI.SetNextControlName(name); }
		public static string GetNameOfFocusedControl()		{ return UnityEngine.GUI.GetNameOfFocusedControl(); }
		public static void FocusControl(string name)		{ UnityEngine.GUI.FocusControl(name); }

		public static void DragWindow()						{ UnityEngine.GUI.DragWindow(); }
		public static void DragWindow(Rect position)		{ UnityEngine.GUI.DragWindow(position); }
		public static void BringWindowToFront(int windowID)	{ UnityEngine.GUI.BringWindowToFront(windowID); }
		public static void BringWindowToBack(int windowID)	{ UnityEngine.GUI.BringWindowToBack(windowID); }
		public static void FocusWindow(int windowID)		{ UnityEngine.GUI.FocusWindow(windowID); }
		public static void UnfocusWindow()					{ UnityEngine.GUI.UnfocusWindow(); }

		public class ClipScope : UnityEngine.GUI.ClipScope
		{
			public ClipScope (Rect position) : base(position) { }
		}

		public class GroupScope : UnityEngine.GUI.GroupScope
		{
			public GroupScope (Rect position)									: base(position) { }
			public GroupScope (Rect position, string text)						: base(position, text) { }
			public GroupScope (Rect position, Texture image)					: base(position, image) { }
			public GroupScope (Rect position, GUIContent content)				: base(position, content) { }
			public GroupScope (Rect position, GUIStyle style)					: base(position, style) { }
			public GroupScope (Rect position, string text, GUIStyle style)		: base(position, text, style) { }
			public GroupScope (Rect position, Texture image, GUIStyle style)	: base(position, image, style) { }
		}

		public class ScrollViewScope : UnityEngine.GUI.ScrollViewScope
		{
			public new Vector2 scrollPosition => base.scrollPosition;
			public bool handleScrollWheel {
				get => base.handleScrollWheel;
				set => base.handleScrollWheel = value;
			}
			public ScrollViewScope (Rect position, Vector2 scrollPosition, Rect viewRect)
				: base(position, scrollPosition, viewRect) { }
			public ScrollViewScope (Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
				: base(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical) { }
			public ScrollViewScope (Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
				: base(position, scrollPosition, viewRect, horizontalScrollbar, verticalScrollbar) { }
			public ScrollViewScope (Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
				: base(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar) { }
		}

		private static readonly Interface INSTANCE;
		private static Interface GetInstance()
		{
			Interface r = (Interface)Util.SystemTools.Interface.CreateInstanceByInterface(typeof(Interface));
			if (null == r) throw new System.NotImplementedException("No realisation for GUI found!");
			return r;
		}
		static GUI()
		{
			INSTANCE = GetInstance();
		}
	}
}
