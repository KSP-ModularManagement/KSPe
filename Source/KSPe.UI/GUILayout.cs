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
 	
namespace KSPe.UI { 
	public static class GUILayout {
	
		public static Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, string text, params GUILayoutOption[] options)                         { return ClickThroughFix.ClickThruBlocker.GUILayoutWindow(id, screenRect, func, text, options); }
		public static Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)                       { return ClickThroughFix.ClickThruBlocker.GUILayoutWindow(id, screenRect, func, image, options); }
		public static Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)                  { return ClickThroughFix.ClickThruBlocker.GUILayoutWindow(id, screenRect, func, content, options); }
		public static Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)         { return ClickThroughFix.ClickThruBlocker.GUILayoutWindow(id, screenRect, func, text, style, options); }
		
		// FIXME : https://github.com/linuxgurugamer/ClickThroughBlocker/issues/6
		public static Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)       { return UnityEngine.GUILayout.Window(id, screenRect, func, image, style, options); }
		public static Rect Window (int id, Rect screenRect, UnityEngine.GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)  { return UnityEngine.GUILayout.Window(id, screenRect, func, content, style, options); }
	
	    public static void Label (Texture image, params GUILayoutOption[] options)                      { UnityEngine.GUILayout.Label(image, options); }
	    public static void Label (string text, params GUILayoutOption[] options)                        { UnityEngine.GUILayout.Label(text, options); }
	    public static void Label (GUIContent content, params GUILayoutOption[] options)                 { UnityEngine.GUILayout.Label(content, options); }
	    public static void Label (Texture image, GUIStyle style, params GUILayoutOption[] options)      { UnityEngine.GUILayout.Label(image, style, options); }
	    public static void Label (string text, GUIStyle style, params GUILayoutOption[] options)        { UnityEngine.GUILayout.Label(text, style, options); }
	    public static void Label (GUIContent content, GUIStyle style, params GUILayoutOption[] options) { UnityEngine.GUILayout.Label(content, style, options); }
	
	    public static void Box (Texture image, params GUILayoutOption[] options)                        { UnityEngine.GUILayout.Box(image, options); }
	    public static void Box (string text, params GUILayoutOption[] options)                          { UnityEngine.GUILayout.Box(text, options); }
	    public static void Box (GUIContent content, params GUILayoutOption[] options)                   { UnityEngine.GUILayout.Box(content, options); }
	    public static void Box (Texture image, GUIStyle style, params GUILayoutOption[] options)        { UnityEngine.GUILayout.Box(image, style, options); }
	    public static void Box (string text, GUIStyle style, params GUILayoutOption[] options)          { UnityEngine.GUILayout.Box(text, style, options); }
	    public static void Box (GUIContent content, GUIStyle style, params GUILayoutOption[] options)   { UnityEngine.GUILayout.Box(content, style, options); }
	
	    public static bool Button (Texture image, params GUILayoutOption[] options)                         { return UnityEngine.GUILayout.Button(image, options); }
	    public static bool Button (string text, params GUILayoutOption[] options)                           { return UnityEngine.GUILayout.Button(text, options); }
	    public static bool Button (GUIContent content, params GUILayoutOption[] options)                    { return UnityEngine.GUILayout.Button(content, options); }
	    public static bool Button (Texture image, GUIStyle style, params GUILayoutOption[] options)         { return UnityEngine.GUILayout.Button(image, style, options); }
	    public static bool Button (string text, GUIStyle style, params GUILayoutOption[] options)           { return UnityEngine.GUILayout.Button(text, style, options); }
	    public static bool Button (GUIContent content, GUIStyle style, params GUILayoutOption[] options)    { return UnityEngine.GUILayout.Button(content, style, options); }
	
	    public static bool RepeatButton (Texture image, params GUILayoutOption[] options)                       { return UnityEngine.GUILayout.RepeatButton(image, options); }
	    public static bool RepeatButton (string text, params GUILayoutOption[] options)                         { return UnityEngine.GUILayout.RepeatButton(text, options); }
	    public static bool RepeatButton (GUIContent content, params GUILayoutOption[] options)                  { return UnityEngine.GUILayout.RepeatButton(content, options); }
	    public static bool RepeatButton (Texture image, GUIStyle style, params GUILayoutOption[] options)       { return UnityEngine.GUILayout.RepeatButton(image, style, options); }
	    public static bool RepeatButton (string text, GUIStyle style, params GUILayoutOption[] options)         { return UnityEngine.GUILayout.RepeatButton(text, style, options); }
	    public static bool RepeatButton (GUIContent content, GUIStyle style, params GUILayoutOption[] options)  { return UnityEngine.GUILayout.RepeatButton(content, style, options); }
	
	    public static string TextField (string text, params GUILayoutOption[] options)                                  { return UnityEngine.GUILayout.TextField(text, options); }
	    public static string TextField (string text, int maxLength, params GUILayoutOption[] options)                   { return UnityEngine.GUILayout.TextField(text, maxLength, options); }
	    public static string TextField (string text, GUIStyle style, params GUILayoutOption[] options)                  { return UnityEngine.GUILayout.TextField(text, style, options); }
	    public static string TextField (string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)   { return UnityEngine.GUILayout.TextField(text, maxLength, style, options); }
	
	    public static string PasswordField (string password, char maskChar, params GUILayoutOption[] options)                                   { return UnityEngine.GUILayout.PasswordField(password, maskChar, options); }
	    public static string PasswordField (string password, char maskChar, int maxLength, params GUILayoutOption[] options)                    { return UnityEngine.GUILayout.PasswordField(password, maskChar, maxLength, options); }
	    public static string PasswordField (string password, char maskChar, GUIStyle style, params GUILayoutOption[] options)                   { return UnityEngine.GUILayout.PasswordField(password, maskChar, style, options); }
	    public static string PasswordField (string password, char maskChar, int maxLength, GUIStyle style, params GUILayoutOption[] options)    { return UnityEngine.GUILayout.PasswordField(password, maskChar, maxLength, style, options); }
	
	    public static string TextArea (string text, params GUILayoutOption[] options)                                   { return UnityEngine.GUILayout.TextArea(text, options); }
	    public static string TextArea (string text, int maxLength, params GUILayoutOption[] options)                    { return UnityEngine.GUILayout.TextArea(text, maxLength, options); }
	    public static string TextArea (string text, GUIStyle style, params GUILayoutOption[] options)                   { return UnityEngine.GUILayout.TextArea(text, style, options); }
	    public static string TextArea (string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)    { return UnityEngine.GUILayout.TextArea(text, maxLength, style, options); }
	
	    public static bool Toggle (bool value, Texture image, params GUILayoutOption[] options)                         { return UnityEngine.GUILayout.Toggle(value, image, options); }
		public static bool Toggle (bool value, string text, params GUILayoutOption[] options)                           { return UnityEngine.GUILayout.Toggle(value, text, options); }
		public static bool Toggle (bool value, GUIContent content, params GUILayoutOption[] options)                    { return UnityEngine.GUILayout.Toggle(value, content, options); }
		public static bool Toggle (bool value, Texture image, GUIStyle style, params GUILayoutOption[] options)         { return UnityEngine.GUILayout.Toggle(value, image, style, options); }
		public static bool Toggle (bool value, string text, GUIStyle style, params GUILayoutOption[] options)           { return UnityEngine.GUILayout.Toggle(value, text, style, options); }
		public static bool Toggle (bool value, GUIContent content, GUIStyle style, params GUILayoutOption[] options)    { return UnityEngine.GUILayout.Toggle(value, content, style, options); }
	
		public static int Toolbar (int selected, string[] texts, params GUILayoutOption[] options)                          { return UnityEngine.GUILayout.Toolbar(selected, texts, options); }
		public static int Toolbar (int selected, Texture[] images, params GUILayoutOption[] options)                        { return UnityEngine.GUILayout.Toolbar(selected, images, options); }
		public static int Toolbar (int selected, GUIContent[] contents, params GUILayoutOption[] options)                   { return UnityEngine.GUILayout.Toolbar(selected, contents, options); }
		public static int Toolbar (int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)          { return UnityEngine.GUILayout.Toolbar(selected, texts, style, options); }
		public static int Toolbar (int selected, Texture[] images, GUIStyle style, params GUILayoutOption[] options)        { return UnityEngine.GUILayout.Toolbar(selected, images, style, options); }
		public static int Toolbar (int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)   { return UnityEngine.GUILayout.Toolbar(selected, contents, style, options); }
	
		public static int SelectionGrid (int selected, string[] texts, int xCount, params GUILayoutOption[] options)                            { return UnityEngine.GUILayout.SelectionGrid(selected, texts, xCount, options); }
		public static int SelectionGrid (int selected, Texture[] images, int xCount, params GUILayoutOption[] options)                          { return UnityEngine.GUILayout.SelectionGrid(selected, images, xCount, options); }
		public static int SelectionGrid (int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)                      { return UnityEngine.GUILayout.SelectionGrid(selected, content, xCount, options); }
		public static int SelectionGrid (int selected, string[] texts, int xCount, GUIStyle style, params GUILayoutOption[] options)            { return UnityEngine.GUILayout.SelectionGrid(selected, texts, xCount, style, options); }
		public static int SelectionGrid (int selected, Texture[] images, int xCount, GUIStyle style, params GUILayoutOption[] options)          { return UnityEngine.GUILayout.SelectionGrid(selected, images, xCount, style, options); }
		public static int SelectionGrid (int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options)     { return UnityEngine.GUILayout.SelectionGrid(selected, contents, xCount, style, options); }
	
		public static float HorizontalSlider (float value, float leftValue, float rightValue, params GUILayoutOption[] options)                                     { return UnityEngine.GUILayout.HorizontalSlider(value, leftValue, rightValue, options); }
		public static float HorizontalSlider (float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)    { return UnityEngine.GUILayout.HorizontalSlider(value, leftValue, rightValue, slider, thumb, options); }
	
		public static float VerticalSlider (float value, float leftValue, float rightValue, params GUILayoutOption[] options)                                   { return UnityEngine.GUILayout.VerticalSlider(value, leftValue, rightValue, options); }
		public static float VerticalSlider (float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)  { return UnityEngine.GUILayout.VerticalSlider(value, leftValue, rightValue, slider, thumb, options); }
		public static float HorizontalScrollbar (float value, float size, float leftValue, float rightValue, params GUILayoutOption[] options)                  { return UnityEngine.GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, options); }
		public static float HorizontalScrollbar (float value, float size, float leftValue, float rightValue, GUIStyle style, params GUILayoutOption[] options)  { return UnityEngine.GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, style, options); }
		public static float VerticalScrollbar (float value, float size, float topValue, float bottomValue, params GUILayoutOption[] options)                    { return UnityEngine.GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, options); }
		public static float VerticalScrollbar (float value, float size, float topValue, float bottomValue, GUIStyle style, params GUILayoutOption[] options)    { return UnityEngine.GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, style, options); }
		
		public static void Space (float pixels)     {UnityEngine.GUILayout.Space(pixels); }
		public static void FlexibleSpace ()         { UnityEngine.GUILayout.FlexibleSpace(); }
		
		public static void BeginHorizontal (params GUILayoutOption[] options)                                       { UnityEngine.GUILayout.BeginHorizontal(options); }
		public static void BeginHorizontal (GUIStyle style, params GUILayoutOption[] options)                       { UnityEngine.GUILayout.BeginHorizontal(style, options); }
		public static void BeginHorizontal (string text, GUIStyle style, params GUILayoutOption[] options)          { UnityEngine.GUILayout.BeginHorizontal(text, style, options); }
		public static void BeginHorizontal (Texture image, GUIStyle style, params GUILayoutOption[] options)        { UnityEngine.GUILayout.BeginHorizontal(image, style, options); }
		public static void BeginHorizontal (GUIContent content, GUIStyle style, params GUILayoutOption[] options)   { UnityEngine.GUILayout.BeginHorizontal(content, style, options); }
		public static void EndHorizontal ()                                                                         { UnityEngine.GUILayout.EndHorizontal(); }
		
		public static void BeginVertical (params GUILayoutOption[] options)                                         { UnityEngine.GUILayout.BeginVertical(options); }
		public static void BeginVertical (GUIStyle style, params GUILayoutOption[] options)                         { UnityEngine.GUILayout.BeginVertical(style, options); }
		public static void BeginVertical (string text, GUIStyle style, params GUILayoutOption[] options)            { UnityEngine.GUILayout.BeginVertical(text, style, options); }
		public static void BeginVertical (Texture image, GUIStyle style, params GUILayoutOption[] options)          { UnityEngine.GUILayout.BeginVertical(image, style, options); }
		public static void BeginVertical (GUIContent content, GUIStyle style, params GUILayoutOption[] options)     { UnityEngine.GUILayout.BeginVertical(content, style, options); }
		public static void EndVertical ()                                                                           { UnityEngine.GUILayout.EndVertical(); }
		
		public static void BeginArea (Rect screenRect)                                          { UnityEngine.GUILayout.BeginArea(screenRect); }
		public static void BeginArea (Rect screenRect, string text)                             { UnityEngine.GUILayout.BeginArea(screenRect, text); }
		public static void BeginArea (Rect screenRect, Texture image)                           { UnityEngine.GUILayout.BeginArea(screenRect, image); }
		public static void BeginArea (Rect screenRect, GUIContent content)                      { UnityEngine.GUILayout.BeginArea(screenRect, content); }
		public static void BeginArea (Rect screenRect, GUIStyle style)                          { UnityEngine.GUILayout.BeginArea(screenRect, style); }
		public static void BeginArea (Rect screenRect, string text, GUIStyle style)             { UnityEngine.GUILayout.BeginArea(screenRect, text, style); }
		public static void BeginArea (Rect screenRect, Texture image, GUIStyle style)           { UnityEngine.GUILayout.BeginArea(screenRect, image, style); }
		public static void BeginArea (Rect screenRect, GUIContent content, GUIStyle style)      { UnityEngine.GUILayout.BeginArea(screenRect, content, style); }
		public static void EndArea ()                                                           { UnityEngine.GUILayout.EndArea(); }
	
		public static Vector2 BeginScrollView (Vector2 scrollPosition, params GUILayoutOption[] options)                                                            { return UnityEngine.GUILayout.BeginScrollView(scrollPosition, options); }
		public static Vector2 BeginScrollView (Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)        { return UnityEngine.GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options); }
		public static Vector2 BeginScrollView (Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)  { return UnityEngine.GUILayout.BeginScrollView(scrollPosition, horizontalScrollbar, verticalScrollbar, options); }
		public static Vector2 BeginScrollView (Vector2 scrollPosition, GUIStyle style)                                                                              { return UnityEngine.GUILayout.BeginScrollView(scrollPosition); }
		public static Vector2 BeginScrollView (Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)                                            { return UnityEngine.GUILayout.BeginScrollView(scrollPosition, options); }
		public static Vector2 BeginScrollView (Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)                      { return UnityEngine.GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, options); }
		public static Vector2 BeginScrollView (Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options) { return UnityEngine.GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options); }
		public static void EndScrollView ()                                                                                                                         { UnityEngine.GUILayout.EndScrollView(); }
		
		public static GUILayoutOption Width (float width)           { return UnityEngine.GUILayout.Width(width); }
		public static GUILayoutOption MinWidth (float minWidth)     { return UnityEngine.GUILayout.MinWidth(minWidth); }
		public static GUILayoutOption MaxWidth (float maxWidth)     { return UnityEngine.GUILayout.MaxWidth(maxWidth); }
		public static GUILayoutOption Height (float height)         { return UnityEngine.GUILayout.Height(height); }
		public static GUILayoutOption MinHeight (float minHeight)   { return UnityEngine.GUILayout.MinHeight(minHeight); }
		public static GUILayoutOption MaxHeight (float maxHeight)   { return UnityEngine.GUILayout.MaxHeight(maxHeight); }
		public static GUILayoutOption ExpandWidth (bool expand)     { return UnityEngine.GUILayout.ExpandWidth(expand); }
		public static GUILayoutOption ExpandHeight (bool expand)    { return UnityEngine.GUILayout.ExpandHeight(expand); }
		
    }
}