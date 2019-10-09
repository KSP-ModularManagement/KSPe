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
        private static readonly string MSG = @"Unfortunately TweakScale found {0} **FATAL** issue(s) on your KSP installment! This *will* corrupt your savagames sooner or later **FOR SURE**!

The KSP.log is listing every compromised part(s) on your installment, look for lines with '[TweakScale] ERROR: **FATAL**' on the log line. Be aware that the parts being reported are not the culprits, but the Screaming Victims.

There's no possible automated fix for the problem, your best line of action is to call for help on Forum by clicking on the OK button below. We will help you on diagnosing the Add'On that is troubling you. Publish your KSP.log on some file share service and mention it on the post.

Be advised that by not closing KSP right now, your savegames can get corrupted at any time, even when things appear to work by now - and the salvage can be harder.

Backup everything *NOW* if you choose to ignore this message and proceed - TweakScale recommends S.A.V.E. to automate this task for you.";

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
                "Houston, we have a Problem!", 
                String.Format(MSG, failure_count),
                () => { Application.OpenURL("https://forum.kerbalspaceprogram.com/index.php?/topic/179030-*"); },
                win, text
            );
            Log.detail("\"Houston, we have a Problem!\" was displayed");
        }
    }
}