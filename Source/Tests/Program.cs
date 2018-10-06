using System;
using System.Linq;
using KSPe;

namespace Tests
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			Type target = typeof(KSPe.KspConfig);
			var t = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
					 from tt in assembly.GetTypes()
					 where tt.Namespace == target.Namespace && tt.Name == "Version" && tt.GetMembers().Any(m => m.Name == "Vendor")
					 select tt).FirstOrDefault();

			if (null != t)
				Console.WriteLine(t.GetField("Vendor").GetValue(null).ToString());

			string[] files = System.IO.Directory.GetFiles("/Users/lisias/Applications/Games/KSP/Exodus/", "*.cfg", System.IO.SearchOption.AllDirectories);
			for (int i = files.Length; --i >= 0;)
				files[i] = files[i].Replace("/Users/lisias/Applications/Games/KSP/Exodus/", "");
			foreach (string f in files.OrderBy(x => x).ToArray())
			{
				Console.WriteLine(f);
			}
			Console.WriteLine(files.Length.ToString());
			
			string[] strings = { "S1", "a1", "S11", "a11", "S2", "a2"};
			foreach (string f in strings.OrderBy(x => x).ToArray())
			{
				Console.WriteLine(f);
			}
			Console.WriteLine(System.IO.Path.GetFullPath("/Users/lisias/Applications/.././Blah/../Bleh/./"));

			if (System.IO.File.Exists("/Users/lisias/Workspaces/KSP/runtime/1.4.3/GameData/net.lisias.ksp/KramaxAutoPilot/."))
				Console.WriteLine("it File.Exists!");
			if (System.IO.Directory.Exists("/Users/lisias/Workspaces/KSP/runtime/1.4.3/GameData/net.lisias.ksp/KramaxAutoPilot/."))
				Console.WriteLine("it Directory.Exists!");
		}   
	}
}
