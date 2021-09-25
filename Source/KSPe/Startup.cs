/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-21 Lisias T : http://lisias.net <support@lisias.net>

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

			try
			{/*	That's the deal:
			  *	
			  *	We need to have the KSPe.UI avaiable as soon as possible, because something can blow up imediately after
			  *	KSPe is loaded (as Module Manager), and if the KSP.UI is delayed too much, we will have a bork inside a bork
			  *	and things will be pretty hard to diagnose. So I choose to initialise it here.
			  *	
			  *	Additionally, a spacialized Loader DLL was introduced to decouple the KSP.UI Logic from the KSPe main DLL, and
			  *	avoiding the problem I created on the code above where KSP specific code loading is tied to the KSPe itself, making
			  *	harder to deploy things later if something change. (TODO: create a Loader for it too in a near future).
			  *	
			  *	The 000_KSPe.dll file, being needed on GameData itself, is tricky to be updated and so it would be wiser to allow
			  *	updating things that can change a lot from it.
			  */ 
				Log.debug("Trying to load KSPe.UI...");
				using (KSPe.Util.SystemTools.Assembly.Loader a = new KSPe.Util.SystemTools.Assembly.Loader(KSPE_ROOT_DIR))
					a.LoadAndStartup("KSPe.UI.Loader");
			}
			catch (System.Exception e)
			{
				Log.error(e, "Error while trying to load KSPe.UI subsystem. This will cause bad side effects later on Add'Ons that depends on it, but it's not fatal for KSPe.");
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
