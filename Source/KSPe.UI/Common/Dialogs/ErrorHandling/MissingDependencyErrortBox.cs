/*
	This file is part of KSPe.UI, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

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
	internal static class MissingDependencyErrorBox
	{
		private static readonly string MSG = @"{0}Unfortunately {1} didn't found {2} installed.

{3}Reason reported: <i>{4}</i>";

		private static readonly string AMSG = @"reinstall {0} from a trusted Distribution Channel (KSP will close)";

		internal static void Show(KSPe.Util.AbstractException ex) {
			string preamble = null == ex.customPreamble ? "" : string.Format("{0}\n\n", ex.customPreamble);
			string epilogue = null == ex.customEpilogue ? "" : string.Format("{0}\n\n", ex.customEpilogue);
			string actionMessage = ex.actionMessage??string.Format(AMSG, ex.offendedName);
			Action lambda = ex.lambda??(() => { Application.Quit(); });
			ShowStopperErrorBox.Show(
				string.Format(MSG, preamble, ex.offendedName, epilogue, ex.Message),
				string.Format(actionMessage, ex.offendedName),
				lambda
			);
			Log.force("\"Houston, we have a Problem!\" about Missing Dependencies on {0} was displayed. Reason reported: {1}", ex.offendedName, ex.Message);
		}
	}
}
