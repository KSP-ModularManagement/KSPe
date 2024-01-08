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
using UnityEngine;

namespace KSPe.InstallChecker.GUI.Dialogs
{
	internal static class ShowStopperAlertBox
	{
		private const string URL = "https://ksp.lisias.net/add-ons/KSPe/KNOWN_ISSUES";
		private static readonly string AMSG = @"to get instructions about how to Download and Install KSPe";

		internal static void Show(string msg)
		{
			KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
				msg,
				AMSG,
				() => { Util.CkanTools.OpenURL(URL); Application.Quit(); }
			);
			Log.detail("\"Houston, we have a Problem!\" was displayed about : {0} . See {1} for details.", msg, URL);
		}
	}
}
