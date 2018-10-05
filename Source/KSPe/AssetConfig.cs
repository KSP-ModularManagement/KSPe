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

		protected static string GeneratePathname<T>(string filename)
		{
			string fn = IO.File<T>.FullPathName(filename, "GameData");
			return fn;
		}

		public static AssetConfig ForType<T>(string name = null)
		{
			string fn = GeneratePathname<T>(name ?? typeof(T).FullName + ".cfg");
			return new AssetConfig(name, fn);
		}

		public static AssetConfig ForType<T>(string name, string filename)
		{
			string fn = GeneratePathname<T>(filename);
			return new AssetConfig(name, fn);
		}

		public static string[] ListForType<T>(string mask = "*.cfg", bool subdirs = false)
		{
			string dir = GeneratePathname<T>(".");
			return ListFiles(SIO.Path.GetDirectoryName(dir), mask, subdirs);
		}
	}
}
