/*
    This file is part of KSPe, a component for KSP API Extensions/L
    (C) 2018 Lisias T : http://lisias.net <support@lisias.net>

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

using System.IO;

namespace KSPe
{
	public class PluginConfig: AbstractConfig
	{
		protected PluginConfig(string name, string fn): base(name)
		{
			this.Path = fn;
		}

		public new PluginConfig Load()
		{
			return (PluginConfig)base.Load();
		}

		public void Save()
		{
			this.Node.Save(this.Path);
		}

		public void Save(ConfigNode node)
		{
			if (null == node)
				throw new IOException("Invalid NULL config for saving!");
			this.Node = node;
			this.Save();
		}

		public void Destroy()
		{
			if (File.Exists(this.Path))
				File.Delete(this.Path);
		}

		public static PluginConfig ForType<T>(string name)
		{
			string fn = IO.File<T>.FullPathName(name + ".cfg", "PluginData", true);
			return new PluginConfig(name, fn);
		}

		public static PluginConfig ForType<T>(string name, string filename)
		{
			string fn = IO.File<T>.FullPathName(filename, "PluginData", true);
			return new PluginConfig(name, fn);
		}
	}
}
