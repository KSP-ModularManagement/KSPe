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
using SIO = System.IO;

namespace KSPe.IO.Temp
{
	// TODO: Eliminate on Vesion 2.6
	[System.Obsolete("KSPe.IO.Temp.ConfigNode is deprecated, please use KSPe.IO.Temp<T>.ConfigNode instead.")]
	public class ConfigNode: WritableConfigNode
	{
		protected ConfigNode(string name, string fn): base(name)
		{
			this.Path = fn;
		}

		public new ConfigNode Load()
		{
			return (ConfigNode)base.Load();
		}

		public static ConfigNode ForType<T>(string name = null)
		{
			string fn = IO.File<T>.Temp.FullPathName( IO.Path.GetRandomFileName() + ".cfg");
			return new ConfigNode(name, fn);
		}

		public static ConfigNode ForType<T>(string name, string filename = null )
		{
			string path = IO.File<T>.Temp.FullPathName(filename ?? IO.Path.GetRandomFileName() + ".cfg");
			return new ConfigNode(name, path);
		}

		public static string[] ListForType<T>(string mask = "*.cfg", bool subdirs = false)
		{
			string dir = IO.File<T>.Temp.FullPathName(".");
			try { 
				string[] files = ReadableConfigNode.ListFiles(dir, mask, subdirs);
				return files;
			} catch (SIO.FileNotFoundException) {
				return new string[0];
			}
		}
	}
}
