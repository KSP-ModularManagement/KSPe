/*
	This file is part of KSPe.Loader, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe.Loader is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe.Loader is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe.Loader. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;
using SIO = System.IO;
using Util = KSPe.Loader.Util;
using Dialog = KSPe.Loader.Dialogs;

public static class ModuleInitializer
{
	public static void Initialize()
	{
		LoadKSPe();
	}

	public static void LoadKSPe()
	{
		KSPe.Loader.Log.info("Loading KSPe main DLL");
		try
		{
			string filename = Util.SystemTools.Path.Combine(Util.SystemTools.Path.GAMEDATA, "000_KSPe", "Plugins", "PluginData", "KSPe.dll");
			// This works, but then KSP's plugins can't find it:
			System.Reflection.Assembly sourceAsm = System.Reflection.Assembly.Load(SIO.File.ReadAllBytes(filename));

			// This make KSPe findable by KSP, but breaks the Loader internally. See https://github.com/net-lisias-ksp/KSPe/issues/50#issuecomment-1546761296
			AssemblyLoader.LoadPlugin(new SIO.FileInfo(filename), filename, null);
		}
		catch (Exception e)
		{
			KSPe.Loader.Log.error(e);
			Dialog.ShowStopperErrorBox.Show(e.Message);
		}
	}
}