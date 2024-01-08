/*
	This file is part of KSPe, a component for KSP Enhanced /L
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
using System;
using System.Diagnostics;

namespace KSPe.Util
{
	public static class UnityTools
	{
		private static int _unityVersion = -1;
		public static int UnityVersion
		{
			get
			{
				if (_unityVersion < 0)
				{
					dbg(" {0}", UnityEngine.Application.unityVersion);
					_unityVersion =
						(UnityEngine.Application.unityVersion.StartsWith("5.")) // 5.4.0p4
							? 5
						: (UnityEngine.Application.unityVersion.StartsWith("2017."))  // 2017.1.3p4
							? 2017
						: (UnityEngine.Application.unityVersion.StartsWith("2019."))  // 2019.2.????
							? 2019
						: 0;
				}
				return _unityVersion;
			}
		}

		[ConditionalAttribute("DEBUG")]
		private static void dbg(string msg, params object[] p)
		{
			KSPe.Log.debug("Util.UnityTools: " + msg, p);
		}

		[ConditionalAttribute("DEBUG")]
		private static void dbg(Exception ex)
		{
			KSPe.Log.error(ex, "Util.UnityTools " + ex.ToString());
		}
	}
}
