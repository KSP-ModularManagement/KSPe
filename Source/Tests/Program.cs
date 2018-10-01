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
		}
	}
}
