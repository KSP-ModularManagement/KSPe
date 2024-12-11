/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

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
using SIO = System.IO;

namespace KSPe.InstallChecker.Checks.GameData
{
	internal class Checker
	{
		private static readonly string WARN_MSG = "There's a `GameData` directory inside your canonical `GameData`. This is a sign that something was wrongly installed in your rig in a recent past. It's recommended to delete the directory `{0}`.";
		private static readonly string ERROR_MSG = "An inner <b><i>GameData</i></b> directory was found in the KSP's <b><i>GameData</i></b>, with {0} files and {1} directories inside - definitively an installation error. You <b>NEED</b> to remove this directory, and reinstall the Add'Ons correctly.\n\nOffending dir: <b><i>{2}</i></b>";

		internal static void Execute()
		{
			try
			{
				string gamedatadouble = SIO.Path.Combine(SanityLib.CalcGameData(), "GameData");
				if (SIO.Directory.Exists(gamedatadouble))
				{ 
					Log.warn(WARN_MSG, gamedatadouble);
					int nfiles = SIO.Directory.GetFiles(gamedatadouble).Length;
					int ndirs = SIO.Directory.GetDirectories(gamedatadouble).Length;
					if (nfiles + ndirs > 0)
						GUI.Dialogs.ShowStopperAlertBox.Show(string.Format(ERROR_MSG, nfiles, ndirs, gamedatadouble));

				}
			}
			catch (Exception e)
			{
				Log.error(e.ToString());
				GUI.Dialogs.ShowStopperAlertBox.Show(e.ToString());
			}
		}
	}
}
