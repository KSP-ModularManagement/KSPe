/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using UnityEngine;

namespace KSPe.InstallChecker.GUI
{
	internal static class ShowStopperAlertBox
	{
		private static readonly string AMSG = @"to get instructions about how to Download and Install KSPe";

		internal static void Show(string msg)
		{
			KSPe.Common.Dialogs.ShowStopperAlertBox.Show(
				msg,
				AMSG,
				() => { Application.OpenURL("https://ksp.lisias.net/add-ons/KSPe/KNOWN_ISSUES"); Application.Quit(); }
			);
			Log.detail("\"Houston, we have a Problem!\" was displayed about : {0}", msg);
		}
	}
}
