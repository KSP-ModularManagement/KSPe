﻿/*
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

using System;
using KSPe.Util;
using SIO = System.IO;

namespace KSPe.IO
{
	public abstract class ReadableConfigNode
	{
		public string Path { get; protected set; }
		public bool IsLoadable => SIO.File.Exists(this.Path) && (0 == (SIO.FileAttributes.Directory & SIO.File.GetAttributes(this.Path)));

		protected ConfigNode _Node; // Staging Node - preventing unwanted or accidental changes. See Invalidate below, and Commit & Rollback on Writable descendants
		public virtual ConfigNode Node {
			get {
				if (null != this._Node) return this._Node;
				return this._Node = ((null != this.name ? this.RawNode.GetNode(this.name) : this.RawNode));
			}
		}
		public ConfigNodeWithSteroids NodeWithSteroids => ConfigNodeWithSteroids.from(this.Node);

		// KSP automatically prefixes all paths with the ApplicationRootPath before using it internally, what plays
		// havoc with our way to keep plugins sandboxed (by always using hardpaths).
		// So we use this when giving such paths to KSP, so it can find the file.
		// (found this due a problem with "my" ModuleManager when loading TechTree.cfg!)
		public string KspPath => this.Path.Replace(Hierarchy.ROOTPATH, "");

		protected ConfigNode RawNode;
		protected readonly string name;
		protected ReadableConfigNode(string name)
		{
			this.name = name;
			this.Clear();
		}

		public ReadableConfigNode Load()
		{
			if (!System.IO.File.Exists(this.Path))
				throw new SIO.FileNotFoundException(this.Path);
			ConfigNode n = ConfigNode.Load(this.Path);
			if (null == n)
				throw new SIO.IOException(string.Format("Invalid config on {0}.", this.Path));
			if (null != this.name && this.name != n.GetNodes()[0].name)
				throw new FormatException(string.Format("Incompatible Node '{1}' for Config '{0}' on {2}.", this.name, n.GetNodes()[0].name, this.Path));
			this.RawNode = n;
			this.Invalidate();
			return this;
		}

		public void Invalidate()
		{
			this._Node = null;
		}

		public void Clear()
		{
			this.Invalidate();
			ConfigNode n = null == this.name ? new ConfigNode() : new ConfigNode(this.name);
			this.RawNode = new ConfigNode();
			if (null != this.name)
				this.RawNode.AddNode(new ConfigNode(this.name));
		}

		protected static string[] ListFiles(string dir, string mask = "*.cfg", bool subdirs = false)
		{
			return File.List(dir, mask, subdirs);
		}
	}
	
	public abstract class WritableConfigNode : ReadableConfigNode
	{
		protected WritableConfigNode(string name) : base(name){}

		public override ConfigNode Node {
			get {
				if (null != this._Node) return this._Node;
				return this._Node = ((null != this.name ? this.RawNode.GetNode(this.name) : KSPe.Util.ConfigNode.CreateDeepCopy(this.RawNode)));
			}
		}

		public void Save()
		{
			this.Save((string)null);
		}

		public void Save(string header)
		{
			this.Commit();
			if (null != header)
				this.RawNode.Save(this.Path, header);
			else
				this.RawNode.Save(this.Path);
			this.Invalidate();
		}

		public void Save(ConfigNode node)
		{
			this.Save(node, null);
		}

		public void Save(ConfigNode node, string header)
		{
			if (null == node)
				throw new FormatException("Invalid NULL config for saving!");

			if (null == this.name)
				this.RawNode = node;
			else if (this.name.Equals(node.name))
				this.update(node);
			else
			{
				ConfigNode[] n = node.GetNodes(this.name);
				if (0 != n.Length)
					this.update(n[0]);
				else
					throw new FormatException(string.Format("Incompatible Node '{1}' for Config '{0}' on {2}.", this.name, node.name, this.Path));
			}
			this.Invalidate();	// Prevent the StagingNode to overwritte the new RawNode
			this.Save(header);
		}

		protected void update(ConfigNode node)
		{
			this.RawNode = new ConfigNode();
			this.RawNode.SetNode(this.name, node, true);
		}

		public void Commit()
		{
			if (null == this._Node) return;

			if (null == this.name)
				this.RawNode = this._Node;
			else
			{
				if (!this.name.Equals(this._Node.name))
					throw new FormatException(string.Format("Incompatible Node '{1}' for Config '{0}' on {2}. How you managed to do that?", this.name, this._Node.name, this.Path));
				if (this.RawNode.HasNode(this.name)) this.RawNode.RemoveNodes(this.name);
				this.RawNode.AddNode(this._Node);
			}

			this.Invalidate();
		}

		public void Rollback()	// Semantic sugar
		{
			this.Invalidate();
		}

		public void Destroy()
		{
			if (SIO.File.Exists(this.Path))
				SIO.File.Delete(this.Path);
			this.Clear();
		}

		/**
		 * Ensures the parent directory of the host file is available for when a Save command is issued.
		 * 
		 * Should be called, when pertinent, from the extending classes' constructors.
		 */
		protected void checkParentDir()
		{
			if (!SIO.File.Exists(this.Path))
			{
				string dir = SIO.Directory.GetParent(this.Path).FullName;
				SIO.Directory.CreateDirectory(dir);
			}
		}
	}

}
