/*
	This file is part of KSPe, a component for KSP API Extensions/L
	© 2018-21 Lisias T : http://lisias.net <support@lisias.net>

	KSPe API Extensions/L is double licensed, as follows:

	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe API Extensions/L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe API Extensions/L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSPe API Extensions/L. If not, see <https://www.gnu.org/licenses/>.

*/
using UnityEngine;
namespace KSPe
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	public class Startup:MonoBehaviour
	{
		private void Start()
		{
			Log.force("Version {0}", Version.Text);
		}

		private void OnDestroy()
		{
			if (!quitOnDestroy) return;

			// Someone, probably a FatalError, told us to quit the game.
			Log.force("Quitting KSP due an unrecoverable error.");
			UnityEngine.Application.Quit();
		}

		// Be *REALLY* cautious with this one!
		// This is used as a fallback in the case the user don't click on the FatalError MsgBox
		private static bool quitOnDestroy = false;
		public static bool QuitOnDestroy
		{
			set
			{
				if (value)
				{
					Log.fatal(1, "I was told to quit the game. Stackdump of the caller follows.");
					Log.stack(typeof(Startup), true);
					quitOnDestroy = value;
				}
			}
		}

		private static readonly Util.Log.Logger Log = Util.Log.Logger.CreateForType<Startup>("KSPe.Light.TweakScale");
	}
}
