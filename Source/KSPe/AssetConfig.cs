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
using System;
using SIO = System.IO;

namespace KSPe
{
	// TODO: Remove on Version 3
	[System.Obsolete("KSPe.AssetConfig is deprecated, please use KSPe.IO.Asset.ConfigNode instead.")]
	public class AssetConfig : AbstractConfig
	{
		public AssetConfig(string name, string fn) : base(name)
		{
			this.Path = fn;
		}

		public new AssetConfig Load()
		{
			return (AssetConfig)base.Load();
		}

		public static AssetConfig ForType<T>(string name = null)
		{
			string fn = IO.File<T>.Asset.FullPathName(name ?? typeof(T).FullName + ".cfg");
			return new AssetConfig(name, fn);
		}

		public static AssetConfig ForType<T>(string name, string filename)
		{
			string fn = IO.File<T>.Asset.FullPathName(filename);
			return new AssetConfig(name, fn);
		}

		public static string[] ListForType<T>(string mask = "*.cfg", bool subdirs = false)
		{
			string dir = IO.File<T>.Asset.FullPathName(".");
			string[] files = AbstractConfig.ListFiles(dir, mask, subdirs);
			return files;
		}
	}
}
