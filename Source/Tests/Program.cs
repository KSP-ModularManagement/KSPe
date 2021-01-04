using System;

namespace Tests
{
	public static class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			MisceTests.Test_CalculateRelativePath();
			MisceTests.Test_GetFullPath();
			Console.WriteLine(Environment.GetCommandLineArgs()[0]);
			Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
			Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
			Console.WriteLine(System.IO.Path.Combine("/a/b/c/d/", "e"));
			Console.WriteLine(System.IO.Path.Combine("/a/b/c/d//", "e"));

			Console.WriteLine(KSPe.Util.KSP.Version.FindByVersion(0,0,0).ToStringExtended());
			Console.WriteLine(KSPe.Util.KSP.Version.FindByVersion(0,1,0).ToStringExtended());
			Console.WriteLine(KSPe.Util.KSP.Version.FindByVersion(0,25,1).ToStringExtended());
			Console.WriteLine(KSPe.Util.KSP.Version.FindByVersion(0,26,10).ToStringExtended());
			Console.WriteLine(KSPe.Util.KSP.Version.FindByVersion(0,95,20).ToStringExtended());
			Console.WriteLine(KSPe.Util.KSP.Version.FindByVersion(1,4,8).ToStringExtended());
			Console.WriteLine(KSPe.Util.KSP.Version.FindByVersion(1,12,8).ToStringExtended());
			Console.WriteLine(KSPe.Util.KSP.Version.GetVersion(1,10,20).ToStringExtended());
		}
	}
}
