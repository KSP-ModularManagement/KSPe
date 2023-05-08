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
	internal static class MissingDLLErrorBox
	{
		private static readonly string MSG = @"Unfortunately {0} didn't found a(some) DLL(s) it needs.

Reason reported: {2}";

		private static readonly string AMSG = @"reinstall {0} and its dependencies from a trusted Distribution Channel (KSP will close)";

		internal static void Show(System.DllNotFoundException ex, string offendedName) {
			string actionMessage = string.Format(AMSG, offendedName);
			ShowStopperErrorBox.Show(
				string.Format(MSG, offendedName, ex.Message),
				string.Format(actionMessage, offendedName),
				() => { Application.Quit(); }
			);
			Log.force("\"Houston, we have a Problem!\" about Missing DLLs on {0} was displayed. Reason reported: {1}", offendedName, ex.Message);
		}
	}
}
