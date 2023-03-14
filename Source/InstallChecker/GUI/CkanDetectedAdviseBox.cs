/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using KSPe.Common.Dialogs;
using KSPe.UI;
using KSPe.Util;

using UnityEngine;

namespace KSPe.InstallChecker.GUI.Dialogs
{
	internal class CkanDetectedAdviseBox:TimedCommonBox
	{
		private static readonly string MSG = @"CKAN was detected in your KSP, but from March 2023 CKAN is not supported anymore by TweakScale, KSP-Recall et all.

Any problems related to TweakScale, KSP-Recall and any other Add'On authored or maintained by LisiasT (and a few others) should be reported to CKAN's maintainers directly.

Visit {0} for details.";

		private static GameObject flag;
		internal static void Show()
		{
			flag = GameObject.Find(typeof(CkanDetectedAdviseBox).FullName);
			Debug.LogFormat("CkanDetectedAdviseBox = {0}", flag);
			if (null != flag) return;

			flag = new GameObject(typeof(CkanDetectedAdviseBox).FullName);
			DontDestroyOnLoad(flag);

			if (!ModuleManagerTools.IsLoadedFromCache)
			{ 
				GameObject go = new GameObject(flag.name);
				TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();

				GUIStyle win = createWinStyle(Color.white);
				GUIStyle text = createTextStyle();
				dlg.Show(
					"Announcement",
					string.Format(MSG, CkanTools.CKAN_URL),
					30, 0, 0,
					win, text
				);
			}
			Log.force("An Announcement about CKAN being present was {0}.", ModuleManagerTools.IsLoadedFromCache ? "omitted" : "displayed");
		}
	}
}