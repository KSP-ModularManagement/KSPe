/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;

using KSPe.Common.Dialogs;
using KSPe.UI;
using KSPe.Util;

using UnityEngine;

namespace KSPe.InstallChecker.GUI.Dialogs
{
	internal class CkanDetectedAdviseBox:TimedCommonBox
	{
		private const int DAYS_BETWEEN_DIALOGS = 30;
		private static readonly string MSG = @"CKAN was detected in your KSP, but from March 2023 CKAN is not supported anymore by TweakScale, KSP-Recall et all.

Any problems related to TweakScale, KSP-Recall and some other Add'Ons authored or maintained by a few authors should be reported to CKAN's maintainers directly.

The KSP.log will mention the Add'Ons currently without support.

Visit {0} for details.";

#if KSPeLight
		// This stunt is needed because there can be various KSPe.Light around, and we don't want all of the showing messages!
		private static GameObject flag;
#endif
		internal static void Show()
		{
#if KSPeLight
			flag = GameObject.Find(typeof(CkanDetectedAdviseBox).FullName);
			Debug.LogFormat("CkanDetectedAdviseBox = {0}", flag);
			if (null != flag) return;

			flag = new GameObject(typeof(CkanDetectedAdviseBox).FullName);
			flag.SetActive(false);
			DontDestroyOnLoad(flag);
#endif

			TimeSpan deltaT = DateTime.Now - Globals.Instance.LastCkanMessage;
			if (!ModuleManagerTools.IsLoadedFromCache && deltaT.TotalDays > DAYS_BETWEEN_DIALOGS)
			{
				Globals.Instance.HitCkanMessage();
				GameObject go = new GameObject(typeof(CkanDetectedAdviseBox).FullName);
				TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();

				GUIStyle win = createWinStyle(Color.white);
				GUIStyle text = createTextStyle();
				dlg.Show(
					"Announcement",
					string.Format(MSG, CkanTools.CKAN_URL),
					30, 0, 0,
					win, text
				);
				Log.force("An Announcement about CKAN being present was displayed.");
			}
			else
				Log.force("An Announcement about CKAN being present was ommited.");
		}
	}
}