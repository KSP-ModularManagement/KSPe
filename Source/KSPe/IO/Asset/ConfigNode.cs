using System;

namespace KSPe.IO.Asset
{
	public class ConfigNode : ReadableConfigNode
	{
		public ConfigNode(string name, string fn) : base(name)
		{
			this.Path = fn;
		}

		public new ConfigNode Load()
		{
			return (ConfigNode)base.Load();
		}

		public static ConfigNode ForType<T>(string name = null)
		{
			string fn = IO.File<T>.Asset.FullPathName(name ?? typeof(T).FullName + ".cfg");
			return new ConfigNode(name, fn);
		}

		public static ConfigNode ForType<T>(string name, string filename)
		{
			string fn = IO.File<T>.Asset.FullPathName(filename);
			return new ConfigNode(name, fn);
		}

		public static string[] ListForType<T>(string mask = "*.cfg", bool subdirs = false)
		{
			string dir = IO.File<T>.Asset.FullPathName(".");
			string[] files = ReadableConfigNode.ListFiles(dir, mask, subdirs);
			return files;
		}
	}
}
