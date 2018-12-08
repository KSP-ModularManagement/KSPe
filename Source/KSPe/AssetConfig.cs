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
