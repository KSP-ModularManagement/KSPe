using System;
using System.Linq;
using KSP;
using KSPe;

namespace Tests
{
	class ConfigNodeWithSteroids
	{
		public static void test()
		{
			Console.WriteLine("Tests.ConfigNodeWithSteroids");
			ConfigNode node = ConfigNode.Load("/Users/lisias/Workspaces/KSP/runtime/1.6.0/PluginData/KerbalJointReinforcement/user.cfg");
			KSPe.ConfigNodeWithSteroids sn = KSPe.ConfigNodeWithSteroids.from(node);
			bool b = sn.GetValue<bool>("debug");
			System.Console.WriteLine(b ? "TRUE" : "FALSE");
		}   
	}
}
