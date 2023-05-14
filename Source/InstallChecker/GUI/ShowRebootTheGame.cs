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
using UnityEngine;

namespace KSPe.InstallChecker.GUI.Dialogs
{
	internal static class ShowRebootTheGame
	{
		private static readonly string AMSG = @"close KSP and restart it so the changes take effect";

		internal static void Show(string msg)
		{
			Startup.quitOnDestroy = true;
			WarningAlertBox.Show(
				msg,
				AMSG,
				() => { Application.Quit(); },
				true
			);
			Log.detail("\"Your Attention Please!\" was displayed about : {0}", msg.Replace("\n"," "));
		}
	}
}
