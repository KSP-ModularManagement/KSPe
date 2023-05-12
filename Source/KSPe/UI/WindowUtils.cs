/*
	This file is part of KSPe.Loader, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe.Loader is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe.Loader is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe.Loader. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;
namespace KSPe.Loader.UI
{
	internal class WindowUtils
	{
		private static readonly object MUTEX = new object();
		private static long __window_id_seed = System.DateTime.Now.Ticks;
		internal static long window_id_seed { get { lock(MUTEX) { return ++__window_id_seed; } } }
	}
}
