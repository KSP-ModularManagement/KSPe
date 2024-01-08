/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
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

using UnityEngine;

namespace KSPe.Util
{
	public static class UrlTools
	{
		public abstract class Handler
		{
			public abstract void OpenURL(string uri);
		}

		internal class OpenAndExitHandler:Handler
		{
			public override void OpenURL(string url)
			{
				KSPe.Log.trace("Opening URL: {0}", url);
				Application.OpenURL(url);
			}
		}

		private static Handler __handler = new OpenAndExitHandler();
		internal static void Register(Handler handler) => __handler = handler;
		public static void OpenURL(string uri) => __handler.OpenURL(uri);
	}
}
