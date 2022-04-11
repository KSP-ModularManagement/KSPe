/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-22 LisiasT : http://lisias.net <support@lisias.net>

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

namespace KSPe { namespace FatalErrors 
{
	// Copy & Paste from KSPe.UI.
	// KSPe must be autonomous without any dependency, as the source of the problem can be one of them!
	internal class FatalErrorMsgBox : MonoBehaviour
	{
		private const string TITTLE = "KSPe Fatal Error";
		private const string MSG = @"This is a **FATAL ERROR** from KSPe!

{0}

This is a *installment error*, not a bug on KSP, KSPe or any other Add'On. KSPe just can't proceed, KSP cannot be executed as is.

A page where you can ask for Support will be called. Read the instructions on that page, we will try to help fixing your problem.

KSP will close now, and an Internet Site where you can ask for help will be shown.";
		private string msg;
		private Action action;
		private int window_id;
		private Rect windowRect;
		private GUIStyle win;
		private GUIStyle text;

		public void Show(string msg)
		{
			Show(msg, null);
		}

		// MessageBox("SNAFU", "Situation Normal... All F* Up!", () => { Application.Quit() });
		public void Show(string msg, Action action)
		{
			this.msg = string.Format(MSG, msg);
			this.action = action;
			this.window_id = (int)-1;

			this.windowRect = this.calculateWindow();
			Texture2D textTex = new Texture2D(1, 1);
			{
				textTex.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.80f));
				textTex.Apply();
			}

			this.win = new GUIStyle("Window")
			{
				fontSize = 26,
				fontStyle = FontStyle.Bold,
				alignment = TextAnchor.UpperCenter,
				wordWrap = false
			};
			this.win.normal.textColor = Color.red;
			this.win.border.top = 0;
			this.win.padding.top = -5;
			this.win.active.background =
				this.win.focused.background =
				this.win.normal.background = textTex;

			this.text = new GUIStyle("Label")
			{
				fontSize = 18,
				fontStyle = FontStyle.Normal,
				alignment = TextAnchor.MiddleLeft,
				wordWrap = true
			};
			this.text.normal.textColor = Color.white;
			this.text.padding.top = 8;
			this.text.padding.bottom = text.padding.top;
			this.text.padding.left = text.padding.top;
			this.text.padding.right = text.padding.top;
			this.text.active.background =
				this.text.focused.background =
				this.text.normal.background = textTex;
		}

		private Rect calculateWindow()
		{
			const int maxWidth = 640;
			const int maxHeight = 480;

			int width = Mathf.Min(maxWidth, Screen.width - 20);
			int height = Mathf.Min(maxHeight, Screen.height - 20);
			
			return new Rect(
				(Screen.width - width) / 2, (Screen.height - height) / 2,
				width, height
			);

		}

		private void OnGUI()
		{
			this.windowRect = GUI.ModalWindow(this.window_id, this.windowRect, WindowFunc, TITTLE, this.win);
		}

		private void WindowFunc(int windowID)
		{
			const int border = 10;
			const int width = 100;
			const int height = 25;
			const int spacing = 10;

			Rect l = new Rect(
					border, border + spacing,
					this.windowRect.width - border * 2, this.windowRect.height - border * 2 - height - spacing
				);

			GUI.Label(l, this.msg, this.text);
			Rect b = new Rect(
				this.windowRect.width - width - border,
				this.windowRect.height - height - border,
				width,
				height);

			if (this.action is null)
			{
				if (GUI.Button(b, "Close KSP")) Destroy(this.gameObject);
			}
			else
			{
				if (GUI.Button(b, "Close KSP")) { this.action(); Destroy(this.gameObject); }
			}
		}
	}

	internal static class NoGameDataFound
	{
		private static readonly string MSG = @"KSPe could not find a GameData folder from where you fired up your KSP game.";

		private static bool shown = false;
		internal static void Show()
		{
			if (shown) return;

			Startup.QuitOnDestroy = shown = true;
			if (null != GameObject.Find("KSPe.FatalError.NoGameDataFound")) return; // Already being shown.

			GameObject go = new GameObject("KSPe.FatalError.NoGameDataFound");
			FatalErrorMsgBox dlg = go.AddComponent<FatalErrorMsgBox>();

			dlg.Show(
				MSG,
				() => { Application.OpenURL("https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/11"); Application.Quit(); }
			);
			Log.error("Fatal Error NoGameDataFound was shown. Please visit https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/11");
		}
	}

	internal static class PwdIsNotOrigin
	{
		private static readonly string MSG = @"The Current Working Directory (pwd on UNIX) doesn't matches the KSPe's origin!

pwd : {0}
origin : {1}

When this happens, KSP may write files on the wrong place, and you can lost track of saves (if KSP manages to startup at all!).";

		private static bool shown = false;
		internal static void Show(string pwd, string origin)
		{
			if (shown) return;

			Startup.QuitOnDestroy = shown = true;
			if (null != GameObject.Find("KSPe.FatalError.PwdIsNotOrigin")) return; // Already being shown.

			GameObject go = new GameObject("KSPe.FatalError.PwdIsNotOrigin");
			FatalErrorMsgBox dlg = go.AddComponent<FatalErrorMsgBox>();

			dlg.Show(
				string.Format(MSG, pwd, origin),
				() => { Application.OpenURL("https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/12"); Application.Quit(); }
			);
			Log.error("Fatal Error PwdIsNotOrigin was shown. pwd = {0} ; origin = {1} . Please visit https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/12", pwd, origin);
		}
	}

	internal static class CriticalComponentsAbsent
	{
		private static readonly string MSG = @"KSPe got a Fatal Error ""{0}"" while trying to load the KSPe.KSP subsystem.

KSPe can't work properly without it, and so anything using it will **NOT** work neither (what means the game is unusable right now).";

		private static bool shown = false;
		internal static void Show(Exception e)
		{
			if (shown) return;

			Startup.QuitOnDestroy = shown = true;
			if (null != GameObject.Find("KSPe.FatalError.CriticalComponentsAbsent")) return; // Already being shown.

			GameObject go = new GameObject("KSPe.FatalError.CriticalComponentsAbsent");
			FatalErrorMsgBox dlg = go.AddComponent<FatalErrorMsgBox>();

			dlg.Show(
				string.Format(MSG, e.Message),
				() => { Application.OpenURL("https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/17"); Application.Quit(); }
			);
			Log.error(e, "Fatal Error CriticalComponentsAbsent was shown. e = [{0}]. Please visit https://github.com/net-lisias-ksp/KSPAPIExtensions/issues/17", e.Message);
		}
	}
} }
