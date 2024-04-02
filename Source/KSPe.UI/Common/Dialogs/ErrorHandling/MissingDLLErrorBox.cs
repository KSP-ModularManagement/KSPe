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
	internal static class MissingDLLErrorBox
	{
		private const string URL = "https://github.com/KSP-ModularManagement/KSPe/issues/55";

		private static readonly string MSG = @"Unfortunately {0} didn't found a(some) DLL(s) it needs. It may be due a faulty installation, but also due the (in)famous <i>Assembly Loader/Resolver</i> bug.

Reason reported: <i>{2}</i>";

		private static readonly string AMSG = @"check {0} and its dependencies, installing them again from a trusted Distribution Channel if missing. Or fix your KSP. (KSP will close and the Support Page will open)";

		internal static void Show(System.DllNotFoundException ex, string offendedName) {
			string actionMessage = string.Format(AMSG, offendedName);
			ShowStopperErrorBox.Show(
				string.Format(MSG, offendedName, ex.Message),
				string.Format(actionMessage, offendedName),
				() => { Util.UrlTools.OpenURL(URL); Application.Quit(); }
			);
			Log.force("\"Houston, we have a Problem!\" about Missing DLLs on {0} was displayed. Reason reported: {1}. URL handled: {2}.", offendedName, ex.Message, URL);
		}
	}
}
