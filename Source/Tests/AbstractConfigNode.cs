using System;
using KSPe.IO;

namespace Tests
{
	public class ReadTestConfigNode : ReadableConfigNode
	{
		public ReadTestConfigNode(string name, string path) : base(name)
		{
			this.Path = path;
		}
	}

	public class WriteTestConfigNode : WritableConfigNode
	{
		public WriteTestConfigNode(string name, string path) : base(name)
		{
			this.Path = path;
		}
	}

	public static class AbstractConfigNode
	{
		private const string ROOT = "/Users/lisias/Workspaces/KSP/runtime/1.6.0";
		public static void Test()
		{
			Console.WriteLine("AbstractConfigNode.Test");

			ReadTestConfigNode def = new ReadTestConfigNode("PreSettings",          ROOT+"/GameData/PhysicsRangeExtender/PluginData/default.cfg");
			def.Load();
			
			WriteTestConfigNode settings = new WriteTestConfigNode("PreSettings",   ROOT+"/PluginData/PhysicsRangeExtender/settings.cfg");		
			bool v = def.NodeWithSteroids.GetValue<bool>("ModEnabled");
			settings.Node.SetValue("ModEnabled", v, true);
			settings.Save();
		}
	}
}
