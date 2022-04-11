/*
	This file is part of KSPe, a component for KSP API Extensions/L
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
using SIO = System.IO;

namespace KSPe.IO.Data
{
	// TODO: Eliminate on Vesion 3
	[System.Obsolete("KSPe.IO.Data.FileStream is deprecated, please use KSPe.IO.Data<T>.FileStream instead.")]
	public class FileStream : SIO.FileStream
	{
		protected FileStream(string filename, SIO.FileMode filemode) : base(filename, filemode) {}

		[System.Obsolete("KSPe.IO.Data.CreateForType<T>(string, FileMode) is deprecated, please use CreateForType<T>(FileMode, string) instead.")]
		public static FileStream CreateForType<T>(string filename, FileMode mode)
		{
			return CreateForType<T>(mode, filename);
		}

		public static FileStream CreateForType<T>(FileMode mode, string filename)
		{
			string path = File<T>.Data.FullPathName(true, filename);
			return new FileStream(path, (SIO.FileMode)mode);
		}

		public static FileStream CreateForType<T>(FileMode mode, string fn, params string[] fns)
		{
			string path = File<T>.Data.FullPathName(true, fn, fns);
			return new FileStream(path, (SIO.FileMode)mode);
		}
	}
}
