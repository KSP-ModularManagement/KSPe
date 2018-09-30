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
			if (!System.IO.Directory.Exists(fn))
				System.IO.Directory.CreateDirectory(fn);
			fn = System.IO.Path.Combine(fn, filename);
			return fn;
		}

		public static PluginConfig ForType<T>(string name)
		{
			string fn = GeneratePathname<T>(name + ".cfg");
			return new PluginConfig(name, fn);
		}
	}
}
