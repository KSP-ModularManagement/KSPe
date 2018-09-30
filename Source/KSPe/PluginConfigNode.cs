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

using System;
using System.IO;

namespace KSPe
{
	public class PluginConfig
	{
		public ConfigNode Node { get; private set; }
		public string Path { get; private set; }
		public bool IsLoadable => System.IO.File.Exists(this.Path) && (0 == (FileAttributes.Directory & System.IO.File.GetAttributes(this.Path)));

		protected PluginConfig(string name, string fn)
		{
			this.Path = fn;
			this.Node = new ConfigNode(name);
		}

		public PluginConfig Load()
		{
			if (!System.IO.File.Exists(this.Path))
				throw new FileNotFoundException(this.Path);
			ConfigNode n = ConfigNode.Load(this.Path);
			if (null == n)
				throw new IOException("Invalid config on " + this.Path);
			this.Node = n;
			return this;
		}

		public void Save()
		{
			this.Node.Save(this.Path);
		}

		public void Save(ConfigNode node)
		{
			this.Node = node;
			this.Save();
		}

		public void Destroy()
		{
			if (File.Exists(this.Path))
				File.Delete(this.Path);
		}

		private static string GeneratePathname<T>(string filename)
		{
			Type target = typeof(T);
			string fn = System.IO.Path.Combine(KSPUtil.ApplicationRootPath, "PluginData");
			fn = System.IO.Path.Combine(fn, target.Namespace);
			fn = System.IO.Path.Combine(fn, filename);
			{
				string d = System.IO.Path.GetDirectoryName(fn);
				if (!System.IO.Directory.Exists(d))
					System.IO.Directory.CreateDirectory(d);
			}
			return fn;
		}

		public static PluginConfig ForType<T>(string name)
		{
			string fn = GeneratePathname<T>(name + ".cfg");
			return new PluginConfig(name, fn);
		}
	}
}
