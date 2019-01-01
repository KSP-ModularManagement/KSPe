using System;
using System.Linq;
using KSP;
using KSPe;

namespace Tests
{
	public static class ConfigNodeWithSteroids
	{
		public static void Test()
		{
			Console.WriteLine("ConfigNodeWithSteroids.Test");
			
			// We run from inside bin/[debug|release]/Tests
			ConfigNode node = ConfigNode.Load("../../../Tests/user.cfg");
			KSPe.ConfigNodeWithSteroids sn = KSPe.ConfigNodeWithSteroids.from(node).GetNode("KJR");
			bool b = sn.GetValue<bool>("debug");
			System.Console.WriteLine(b ? "TRUE" : "FALSE");
			int i=0;
			foreach (ConfigNode cc in sn.GetNodes("Exempt"))
			{
				System.Console.WriteLine(i++.ToString());
				KSPe.ConfigNodeWithSteroids c = KSPe.ConfigNodeWithSteroids.from(cc);
				if (c.HasValue("PartType"))
					System.Console.WriteLine(c.GetValue<string>("PartType"));
				if (c.HasValue("ModuleType"))
					System.Console.WriteLine(c.GetValue<string>("ModuleType"));
				if (c.HasValue("DecouplerStiffeningExtensionType"))
					System.Console.WriteLine(c.GetValue<string>("DecouplerStiffeningExtensionType"));
			}
		}   
	}
}
