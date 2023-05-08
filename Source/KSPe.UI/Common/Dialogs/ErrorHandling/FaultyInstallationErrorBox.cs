/*
	This file is part of KSPe.UI, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP Enhanced /L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using UnityEngine;

namespace KSPe.Common.Dialogs.ErrorHandling
{
	internal static class FaultyInstallationErrorBox
	{
		private static readonly string MSG = @"{0}{1}

{2}Reason reported: {3}";

		private static readonly string AMSG = @"close KSP, then reinstall {0}";

		internal static void Show(Util.Installation.Exception ex) {
			string preamble = null == ex.customPreamble ? "" : string.Format("{0}\n\n", ex.customPreamble);
			string epilogue = null == ex.customEpilogue ? "" : string.Format("{0}\n\n", ex.customEpilogue);
			string actionMessage = ex.actionMessage??string.Format(AMSG, ex.offendedName);
			Action lambda = ex.lambda??(() => { Application.Quit(); });
			ShowStopperErrorBox.Show(
				string.Format(MSG, preamble, ex.ToLongMessage(), epilogue, ex.Message),
				actionMessage,
				lambda
			);
			Log.force("\"Houston, we have a problem!\" about a {0} faulty installation was displayed. Reason reported: {1}", ex.offendedName, ex.Message);
		}
	}
}
