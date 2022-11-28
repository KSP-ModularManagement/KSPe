/*
	This file is part of KSPe, a component for KSP API Extensions/L
	unless when specified otherwise below this code is:
		© 2018-22 LisiasT : http://lisias.net <support@lisias.net>

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
using System.Reflection;

namespace KSPe.Util.Image
{
	public static partial class Screenshot
	{
		private class CrappyImplementation:Interface
		{
			private MethodInfo method = null;

			internal CrappyImplementation()
			{
				Assembly unityEngineAssembly = Assembly.Load("UnityEngine");
				if (5 == KSPe.Util.UnityTools.UnityVersion)
				{
					Type applicationClass = unityEngineAssembly.GetType("UnityEngine.Application");
					this.method = applicationClass.GetMethod(
								"CaptureScreenshot", 
								BindingFlags.Static | BindingFlags.Public, 
								null,  
								new [] {typeof(string), typeof(int)}, 
								null
							);
				}
				else
				{
					Type applicationClass = unityEngineAssembly.GetType("UnityEngine.ScreenCapture");
					this.method = applicationClass.GetMethod(
								"CaptureScreenshot", 
								BindingFlags.Static | BindingFlags.Public, 
								null,  
								new [] {typeof(string), typeof(int)}, 
								null
							);
				}
			}

			void Interface.Capture(string pathname)
			{
				Capture(pathname, 1);
			}

			void Interface.Capture(string pathname, int superSampleValue)
			{
				this.method.Invoke(null, new object[] { pathname, superSampleValue });
			}
		}

		private static Interface GetInstance()
		{
			return (Interface)new CrappyImplementation();
		}
	}

}
