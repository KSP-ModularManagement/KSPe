using KSP;
using System;
using System.IO;

namespace KSPe.IO
{
	public class PluginConfiguration : KSP.IO.PluginConfiguration
	{
		protected PluginConfiguration(string pathToFile) : base(pathToFile) { }

		public static PluginConfiguration CreateForType<T>(string filename)
		{
			Type target = typeof(T);
			string fn = Path.Combine(KSPUtil.ApplicationRootPath, "PluginData");
			fn = Path.Combine(fn, target.Namespace);
			if (!System.IO.Directory.Exists(fn))
				System.IO.Directory.CreateDirectory(fn);

			// TODO: Checar como funciona o Vessel. Possivelmente nesse caso o XML deve estar no savegame!
			fn = Path.Combine(fn, filename);
			return new PluginConfiguration(fn);
		}

		public static new PluginConfiguration CreateForType<T>(Vessel flight)
		{
			Type target = typeof(T);
			return CreateForType<T>((flight ? flight.GetName() + "." + target.Name : target.Name) + ".xml");
		}

		public static PluginConfiguration CreateForType<T>()
		{
			return CreateForType<T>((Vessel)null);
		}
	}
}
