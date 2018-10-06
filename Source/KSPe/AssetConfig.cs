using System;
using SIO = System.IO;

namespace KSPe
{
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
			string dir = SIO.Path.GetFullPath(IO.File<T>.Asset.FullPathName("."));
			string[] files = AbstractConfig.ListFiles(SIO.Path.GetDirectoryName(dir), mask, subdirs);
			for (int i = files.Length; --i >= 0;)
				files[i] = files[i].Substring(files[i].IndexOf(dir) + dir.Length);
			return files;
		}
	}
}
