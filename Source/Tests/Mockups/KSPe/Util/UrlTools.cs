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
using System.Collections.Generic;

namespace KSPe.Util
{
	public static class UrlTools
	{
		public abstract class Handler
		{
			public abstract void OpenURL(string uri);
		}

		public class OpenAndExitHandler:Handler
		{
			public override void OpenURL(string uri)
			{
				Application.OpenURL(uri);
				//Application.Quit();
			}
		}

		static UrlTools()
		{
			Register(new OpenAndExitHandler());
		}

		private static readonly Stack<Handler> __handler = new Stack<Handler>();
		public static void Register(Handler handler) => __handler.Push(handler);
		public static void Pop()
		{
			if (1 == __handler.Count) return;
			__handler.Pop();
		}
		public static void OpenURL(string uri) => __handler.Peek().OpenURL(uri);
	}
}
