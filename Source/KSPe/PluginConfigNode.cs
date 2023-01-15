/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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
	[System.Obsolete("KSPe.PluginConfig is deprecated, please use KSPe.IO.Data.ConfigNode instead.")]
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
			this.RawNode.Save(this.Path);
		}

		public void Save(ConfigNode node)
		{
			if (null == node)
				throw new FormatException("Invalid NULL config for saving!");

			if (null == this.name)
				this.RawNode = node;
			else if (this.name == node.name)
				this.RawNode = node;
			else
			{
				ConfigNode[] n = node.GetNodes(this.name);
				if (0 != n.Length)
					this.RawNode = n[0];
				else
					throw new FormatException(string.Format("Incompatible Node '{1}' for Config '{0}' on {2}.", this.name, node.name, this.Path));
			}	
			this.Save();
		}

		public void Destroy()
		{
			if (SIO.File.Exists(this.Path))
				SIO.File.Delete(this.Path);
			this.Clear();
		}

		public static PluginConfig ForType<T>(string name = null)
		{
			string fn = IO.File<T>.Data.FullPathName(true, (name ?? typeof(T).FullName) + ".cfg");
			return new PluginConfig(name, fn);
		}

		public static PluginConfig ForType<T>(string name, string filename)
		{
			string fn = IO.File<T>.Data.FullPathName(true, filename);
			return new PluginConfig(name, fn);
		}

		public static string[] ListForType<T>(string mask = "*.cfg", bool subdirs = false)
		{
			string dir = IO.File<T>.Data.FullPathName(false, ".");
			string[] files = AbstractConfig.ListFiles(dir, mask, subdirs);
			return files;
		}
	}
}
