using System;
using System.IO;

namespace KSPe
{
	public class KspConfig
	{
		public ConfigNode Node { get; private set; }
		public string Path { get; private set; }
		public bool IsLoadable => System.IO.File.Exists(this.Path) && (0 == (FileAttributes.Directory & System.IO.File.GetAttributes(this.Path)));

		public KspConfig(string name)
		{
			this.Path = GeneratePathname(name + ".cfg");
			this.Node = new ConfigNode(name);
		}

		public KspConfig Load()
		{
			if (!System.IO.File.Exists(this.Path))
				throw new FileNotFoundException(this.Path);
			ConfigNode n = ConfigNode.Load(this.Path);
			if (null == n)
				throw new IOException("Invalid config on " + this.Path);
			this.Node = n;
			return this;
		}

		private static string GeneratePathname(string filename)
		{
			string fn = System.IO.Path.Combine(KSPUtil.ApplicationRootPath, filename);
			return fn;
		}
	}
}
