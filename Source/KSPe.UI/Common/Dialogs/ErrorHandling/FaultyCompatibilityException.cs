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
	internal class FaultyCompatibility:TimedCommonBox
	{
		private static readonly string MSG = @"{0}{1}

It's not certain that it will not work fine, it's <b>NOT KNOWN</b> and KSP may inject bad information on your savegames (ruining them), if anything goes south.

{3}Please proceed with caution - backup anything valuable (as your savegames!).";

		internal static void Show(Util.Compatibility.Exception ex)
		{
			GameObject go = new GameObject("KSPe.Common.Diallgs.ErrorHandling.FaultyCompatibility");
			TimedMessageBox dlg = go.AddComponent<TimedMessageBox>();

			GUIStyle win = createWinStyle(Color.white);
			GUIStyle text = createTextStyle();

			string preamble = null == ex.customPreamble ? "" : string.Format("{0}\n\n", ex.customPreamble);
			string epilogue = null == ex.customEpilogue ? "" : string.Format("{0}\n\n", ex.customEpilogue);

			dlg.Show(
				string.Format("{0} Advice", ex.offendedName),
				string.Format(MSG, preamble, ex.offendedName, Util.KSP.Version.Current.ToString(), epilogue),
				30, 0, 0,
				win, text
			);
			Log.force("\"{0} Advice\" about not known to work properly on the current installment was displayed. Reason reported: {1}", ex.offendedName, ex.Message);
		}
	}
}
