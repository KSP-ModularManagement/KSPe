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
namespace KSPe.UI
{
	[KSPAddon(KSPAddon.Startup.Instantly, true)]
	public class Startup:MonoBehaviour
	{
		private void Start()
		{
			LOG.force("Version {0}", Version.Text);
		}

		private void Awake()
		{
			using (KSPe.Util.SystemTools.Assembly.Loader a = new KSPe.Util.SystemTools.Assembly.Loader("000_KSPAPIExtensions"))
			{ 
				// There can be only one! #highlanderFeelings
				if (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.GetVersion(1,4,0))
				{
					if (System.IO.File.Exists("./000_ClickThroughBlocker/Plugins/ClickThroughBlocker.dll"))
						a.LoadAndStartup("KSPe.UI.14");
					else
					{
						LOG.warn("ClickThroughBlocker, dependency on KSP >= 1.4, was not found! Falling back to KSP.UI.12 instead!");
						a.LoadAndStartup("KSPe.UI.12");
					}
				}
				else if (KSPe.Util.KSP.Version.Current >= KSPe.Util.KSP.Version.GetVersion(1,2,0))
					a.LoadAndStartup("KSPe.UI.12");
			}
		}

		private static readonly Util.Log.Logger LOG = Util.Log.Logger.CreateForType<Startup>();
	}
}
