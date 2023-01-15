/*
	This file is part of KSPe, a component for KSP API Extensions/L
	© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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
using System;
using UnityEngine;
using KSPe.Util.Image;

namespace KSPe.Light.Util.Image
{
	public class Screenshooter : Screenshot.Interface
	{
		private bool oldUnity = KSPe.Util.UnityTools.UnityVersion < 2017;

		void Screenshot.Interface.Capture(string pathname)
		{
			if (oldUnity) CaptureOnUnity5(pathname, 0);
			else CaptureOnUnity2017(pathname, 0);
		}

		void Screenshot.Interface.Capture(string pathname, int superSampleValue)
		{
			if (oldUnity) CaptureOnUnity5(pathname, superSampleValue);
			else CaptureOnUnity2017(pathname, superSampleValue);
		}

		private static void CaptureOnUnity5(string pathname, int superSampleValue)
		{
			Type type = KSPe.Util.SystemTools.Type.Finder.FindByQualifiedName("UnityEngine.Application");
			type.GetMethod("CaptureScreenshot", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, new object[] { pathname, superSampleValue } );
		}

		private static void CaptureOnUnity2017(string pathname, int superSampleValue)
		{
			Type type = KSPe.Util.SystemTools.Type.Finder.FindByQualifiedName("UnityEngine.ScreenCapture");
			type.GetMethod("CaptureScreenshot", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Invoke(null, new object[] { pathname, superSampleValue } );
		}
	}
}
