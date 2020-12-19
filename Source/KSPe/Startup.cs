/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-20 Lisias T : http://lisias.net <support@lisias.net>

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
			// Nope, we should not use the Log Facilities ourselves. Ironic, uh? :)
			UnityEngine.Debug.LogFormat("[KSPe] Version {0}", Version.Text);
		}

		private void Awake()
		{
			KSPe.Util.SystemTools.Assembly.AddSearchPath("GameData/000_KSPAPIExtensions/Plugins/PluginData");

			{
				#if DEBUG
					UnityEngine.Debug.LogFormat("Trying to load KSPe.Unity.{0}...", KSPe.Util.UnityTools.UnityVersion);
				#endif
				Util.SystemTools.Assembly.LoadAndStartup(string.Format("KSPe.Unity.{0}", KSPe.Util.UnityTools.UnityVersion));
			}

			for (int i = 11; i > 0; --i)
				if (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.FindByVersion(1,i,0))
				{
					#if DEBUG
						UnityEngine.Debug.LogFormat("Trying to load KSPe.KSP.1{0}...", i);
					#endif
					Util.SystemTools.Assembly.LoadAndStartup(string.Format("KSPe.KSP.1{0}",i));
				}

			#if DEBUG
				UnityEngine.Debug.LogFormat("Trying to load KSPe.UI...");
			#endif
			KSPe.Util.SystemTools.Assembly.LoadAndStartup("KSPe.UI");
		}
	}
}
