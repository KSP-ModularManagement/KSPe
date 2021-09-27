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
		public static readonly string KSPE_ROOT_DIR = "000_KSPAPIExtensions";
		private void Start()
		{
			Log.force("Version {0}", Version.Text);
			SanityChecks.DoIt();
		}

		private void Awake()
		{
			try
			{ 
				using (KSPe.Util.SystemTools.Assembly.Loader a = new KSPe.Util.SystemTools.Assembly.Loader(KSPE_ROOT_DIR))
				{ 
					{
						int target = KSPe.Util.UnityTools.UnityVersion;
						target = (0 == target) ? 2019 : target;
						Log.dbg("Trying to load KSPe.Unity.{0}...", target);
						a.LoadAndStartup(string.Format("KSPe.Unity.{0}", target));
					}

					for (int i = KSPe.Util.KSP.Version.Current.MINOR; i > 0; --i)
						if (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.GetVersion(1,i,0))
						{
							Log.dbg("Trying to load KSPe.KSP.1{0}...", i);
							if ( null != a.LoadAndStartup(string.Format("KSPe.KSP.1{0}",i)) ) break;
						}
				}
			}
			catch (System.Exception e)
			{
				FatalErrors.CriticalComponentsAbsent.Show(e);
			}
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

		private static readonly Util.Log.Logger Log = Util.Log.Logger.CreateForType<Startup>();
	}
}
