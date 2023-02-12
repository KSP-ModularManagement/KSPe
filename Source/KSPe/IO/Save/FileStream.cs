﻿/*
	This file is part of KSPe, a component for KSP Enhanced /L
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
using SIO = System.IO;

namespace KSPe.IO.Save
{
	// TODO: Eliminate on Vesion 2.6
	[System.Obsolete("KSPe.IO.Save.FileStream is deprecated, please use KSPe.IO.Data<T>.FileStream instead.")]
	public class FileStream : SIO.FileStream
	{
		protected FileStream(string filename, SIO.FileMode filemode) : base(filename, filemode) {}

		public static FileStream CreateForType<T>(FileMode mode, string filename)
		{
			string path = File<T>.Save.FullPathName(true, filename);
			return new FileStream(path, (SIO.FileMode)mode);
		}

		public static FileStream CreateForType<T>(FileMode mode, string fn, params string[] fns)
		{
			string path = File<T>.Save.FullPathName(true, fn, fns);
			return new FileStream(path, (SIO.FileMode)mode);
		}
	}
}
