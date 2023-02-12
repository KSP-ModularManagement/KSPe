/*
	This file is part of KSPe, a component for KSP Enhanced /L
	unless when specified otherwise below this code is:
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

namespace KSPe.Util.Image {
	public static partial class Screenshot
	{
		private class Fallback : Interface
		{
			void Interface.Capture(string pathname)
			{
				Capture(pathname, 1);
			}

			void Interface.Capture(string pathname, int superSampleValue)
			{
				ScreenMessages.PostScreenMessage ("No KSPe Screenshot support installed.");
				KSPe.Log.warn("Util.Image.Screenshot: Screenshot support not properly initialized! Screenshot {0} not taken!", pathname);
			}
		}

		private static Interface GetInstance()
		{
			//Interface r = (Interface)Util.SystemTools.Interface.CreateInstanceByInterfaceName("KSPe.Util.Image.Screenshot+Interface");
			Interface r = (Interface)Util.SystemTools.Interface.CreateInstanceByInterface(typeof(Interface));
			if (null != r) return r;

			KSPe.Log.warn("Util.Image.Screenshot: No realisation for the abstract Interface found! Using a fallback one!");
			return (Interface) new Fallback();
		}
	}

}
