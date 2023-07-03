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
using UnityEngine;
using KSPe.UI;

namespace KSPe.Common.Dialogs.ErrorHandling
{
	internal class UnhandledAlertBox
	{
		private static readonly string MSG = @"This is hairy: an uncaught Exception was detected.

		This may or may not be a serious problem, there's no way to know at this point. Unless this is a known problem and this Exception is already known to be harmless, it's safer to stop now and ask for help on a Support Channel. The <i>KSP.log</i> will have a full StackDump pinpoint exactly where the Exception was thrown.

		If you choose to proceed, be advised that your savegames may be ruined if this is a new and serious problem. Backup everything <b>NOW</b> just in case.

		Reason reported: <i>{0}</i>.";

		internal static void Show(System.Exception ex, string offendedName, string url = null)
		{
			ShowStopperErrorBox.Show(
				MSG,
				url != null ? "Quit KSP and open the Support Page" : "Quit KSP",
				() => { Util.UrlTools.OpenURL(url); Application.Quit(); }
			);
			Log.force("\"Houston, we have a Problem!\"  about uncaught unexpected Exception was displayed. Reason reported: {0}.", ex.Message);
			Log.stackDump(ex);
		}

		internal static void Show(Util.AbstractException ex)
		{
			Show(ex, ex.offendedName);
		}
	}
}