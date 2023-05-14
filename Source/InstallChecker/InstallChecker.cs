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
using System;
using System.Collections.Generic;
using System.Linq;

namespace KSPe.InstallChecker
{
	internal class Checker
	{
		private readonly SanityLib.UpdateData[] UPDATEABLES = new SanityLib.UpdateData[]
		{
			new SanityLib.UpdateData(
				"KSPe Install Checker",
				System.IO.Path.Combine("000_KSPe",
					System.IO.Path.Combine("Plugins", "KSPe.InstallChecker.dll")),
				"000_KSPe.dll")
			,
			new SanityLib.UpdateData(
				"KSPe",
				System.IO.Path.Combine("000_KSPe",
					System.IO.Path.Combine("Plugins", "KSPe.dll")),
				"001_KSPe.dll")
		};

		internal void CheckKSPe()
		{
			try
			{
				// Always check for being the unique Assembly loaded. This will avoid problems in the future.
				String msg = CheckMyself();

				if ( null != msg )
					GUI.Dialogs.ShowStopperAlertBox.Show(msg);
			}
			catch (Exception e)
			{
				Log.error(e.ToString());
				GUI.Dialogs.ShowStopperAlertBox.Show(e.ToString());
			}

			try
			{
				List<string> msgs = new List<string>();
				string msg;
				foreach (SanityLib.UpdateData ud in UPDATEABLES)
				{
					Log.debug("Checking {0}", ud.name);
					msg = SanityLib.UpdateIfNeeded(ud);
					if (null != msg) msgs.Add(msg);
				}

				msg = string.Join("\n\n", msgs.ToArray());
				if ( !string.Empty.Equals(msg) )
					GUI.Dialogs.ShowRebootTheGame.Show(msg);
			}
			catch (Exception e)
			{
				Log.error(e.ToString());
				GUI.Dialogs.ShowStopperAlertBox.Show(e.ToString());
			}

			if (Util.CkanTools.CheckCkanInstalled())
				Log.force("CKAN was detected on this KSP instalment.");
		}

		private const string ERR_MULTIPLE_TOOL = "There're more than one KSPe Install Checker on this KSP installment! Please delete all but the one on GameData/000_KSPe/Plugins !";
		private const string ERR_OLD_TOOL = "A deprecated release of KSPe was found!! Please delete the `GameData/000_KSPAPIExtensions` directory !";

		private string CheckMyself()
		{
			{
				if(System.IO.Directory.Exists(System.IO.Path.Combine(
					KSPUtil.ApplicationRootPath,
					"GameData/000_KSPAPIExtensions"))
				)
					return ERR_OLD_TOOL;
			}
			{
				IEnumerable<AssemblyLoader.LoadedAssembly> loaded = SanityLib.FetchLoadedAssembliesByName("KSPe.InstallChecker");
				// Obviously, would be pointless to check for it not being installed! (0 == count). :)
				if (1 != loaded.Count()) return ERR_MULTIPLE_TOOL;
			}
			return null;
		}
	}
}
