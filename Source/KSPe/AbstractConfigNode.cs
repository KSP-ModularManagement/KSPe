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
	public class AbstractConfig
	{
		public string Path { get; protected set; }
		public bool IsLoadable => System.IO.File.Exists(this.Path) && (0 == (FileAttributes.Directory & System.IO.File.GetAttributes(this.Path)));

		private ConfigNode _Node;
		public ConfigNode Node {
			get {
				if (null == this._Node)
					this._Node = (null != this.name ? this.RawNode.GetNode(this.name) : this.RawNode);
				return this._Node;
			}
		}

		// KSP automatically prefixes all paths with the ApplicationRootPath before using it internally, what plays
		// havoc with our way to keep plugins sandboxed (by always using hardpaths).
		// So we use this when giving such paths to KSP, so it can find the file.
		// (found this due a problem with "my" ModuleManager when loading TechTree.cfg!)
		public string KspPath => this.Path.Replace(KSPUtil.ApplicationRootPath, "");

		protected ConfigNode RawNode;
		protected readonly string name;
		protected AbstractConfig(string name)
		{
			this.name = name;
			this.Clear();
		}

		public AbstractConfig Load()
		{
			if (!System.IO.File.Exists(this.Path))
				throw new FileNotFoundException(this.Path);
			ConfigNode n = ConfigNode.Load(this.Path);
			if (null == n)
				throw new IOException(string.Format("Invalid config on {0}.", this.Path));
			if (null != this.name && this.name != n.GetNodes()[0].name)
				throw new FormatException(string.Format("Incompatible Node '{1}' for Config '{0}' on {2}.", this.name, n.GetNodes()[0].name, this.Path));
			this.RawNode = n;
			return this;
		}

		public void Clear()
		{
			this._Node = null;
			ConfigNode n = null == this.name ? new ConfigNode() : new ConfigNode(this.name);
			this.RawNode = new ConfigNode();
			if (null != this.name)
				this.RawNode.AddNode(new ConfigNode(this.name));
		}
	}
}
