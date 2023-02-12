/*
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

namespace KSPe.IO.Temp
{
	// TODO: Eliminate on Vesion 3
	[System.Obsolete("KSPe.IO.Temp.FileStream is deprecated, please use KSPe.IO.Temp<T>.FileStream instead.")]
	public class FileStream : SIO.FileStream
	{
		protected FileStream(string filename, SIO.FileMode filemode) : base(filename, filemode) {}

		[System.Obsolete("KSPe.IO.Local.CreateForType<T>(string, FileMode) is deprecated, please use CreateForType<T>(FileMode, string) instead.")]
		public static FileStream CreateForType<T>(string filename, FileMode mode)
		{
			return CreateForType<T>(mode, filename);
		}

		public static FileStream CreateForType<T>(FileMode mode, string filename = null)
		{
			string path = File<T>.Temp.FullPathName(filename);
			return new FileStream(path, (SIO.FileMode)mode);
		}
	}
}
