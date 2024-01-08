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
using UnityEngine;
using KSPe.UI;

namespace KSPe.Common.Dialogs.ErrorHandling
{
	internal class AlreadyLoadedAlertBox:TimedCommonBox
	{
		private static readonly string MSG = @"{0}{1} tried to loaded the Assembly {2} while it's already in memory!

		KSPe prevented the mishap, but this suggests that {1} is borked and in need of maintenance.

		{3}File a bug report to the {1}'s Maintainer.";

		internal static void Show(Util.SystemTools.Assembly.Loader.AlreadyLoadedException ex)
		{
			GameObject go = new GameObject("KSPe.Common.Diallgs.ErrorHandling.AlreadyLoadedAlertBox");
			TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();

			GUIStyle win = createWinStyle(Color.yellow);
			GUIStyle text = createTextStyle();

			string preamble = null == ex.customPreamble ? "" : string.Format("{0}\n\n", ex.customPreamble);
			string epilogue = null == ex.customEpilogue ? "" : string.Format("{0}\n\n", ex.customEpilogue);

			dlg.Show(
				string.Format("{0} Warning", ex.offendedName),
				string.Format(MSG, preamble, ex.offendedName, ex.offendingAssembly, epilogue),
				30, 0, 0,
				win, text
			);
			Log.force("\"{0} Warning\" about trying to load {1} with it already in memory.", ex.offendedName, ex.offendingAssembly);
		}
	}
}
