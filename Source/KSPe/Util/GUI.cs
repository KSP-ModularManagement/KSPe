/*
	This file is part of KSPe, a component for KSP API Extensions/L
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
using KSPe.UI;
using UnityEngine.Experimental.UIElements.StyleEnums;

namespace TweakScale.GUI
{
    internal static class ShowStopperAlertBox
    {
        private const string MSG = @"TweakScale found {0} **FATAL** issue(s) with your KSP install! This *will* corrupt your saves!

Your KSP.log lists every problematic part in your install; look for lines containing '[TweakScale] ERROR: **FATAL**'. Note that these parts are not the culprits, but innocent victims. No automated fix is possible for these problems.

You should click OK below to close KSP, then ask for help with diagnosing the problem mod on the forum. Please upload your KSP.log to a file share service and share a link to it in your post.

If you choose to continue running KSP, your saves may be unrecoverably corrupted, even if it seems to be working. Make back-ups now! You may find the S.A.V.E mod helpful for this.";

        internal static void Show(int failure_count)
        {
            GameObject go = new GameObject("TweakScale.AlertBox");
            MessageBox dlg = go.AddComponent<MessageBox>();

            GUIStyle win = new GUIStyle("Window")
            {
                fontSize = 26,
                fontStyle = FontStyle.Bold
            };
            win.normal.textColor = Color.red;
            win.border.top = 36;

            GUIStyle text = new GUIStyle("Label")
            {
                fontSize = 18,
                fontStyle = FontStyle.Normal,
                alignment = TextAnchor.MiddleLeft
            };
            text.padding.top = 8;
            text.padding.bottom = text.padding.top;
            text.padding.left = text.padding.top;
            text.padding.right = text.padding.top;
            {
                Texture2D tex = new Texture2D(1,1);
                tex.SetPixel(0,0,new Color(0f, 0f, 0f, 0.45f));
                tex.Apply();
                text.normal.background = tex;
            }

            dlg.Show(
                "Houston, we have a problem!",
                String.Format(MSG, failure_count),
                () => { Application.OpenURL("https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*"); },
                win, text
            );
            Log.detail("\"Houston, we have a problem!\" was displayed");
        }
    }
}