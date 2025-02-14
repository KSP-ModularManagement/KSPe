/*
	This file is part of KSPe.UI, a component for KSP Enhanced /L
		© 2018-2025 LisiasT : http://lisias.net <support@lisias.net>

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
using System.Collections.Generic;

namespace KSPe.UI
{
	public static class UID
	{
		private static readonly object MUTEX = new object();
		private static readonly System.Random R = new System.Random(System.Environment.TickCount);
		private static readonly HashSet<int> IN_USE = new HashSet<int>();

		public static int Get()
		{
			int r;
			lock (MUTEX) while (true)
				{
					r = R.Next();
					if (IN_USE.Contains(r)) continue;
					IN_USE.Add(r);
					break;
				}
			return r;
		}

		public static void Release(int r) => IN_USE.Remove(r);
	}
}
