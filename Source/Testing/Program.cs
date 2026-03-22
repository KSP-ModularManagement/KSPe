using System;

using SIO = System.IO;

namespace Tests
{
	public static class MainClass
	{
		private static void TestGetDrives()
		{
			try
			{
				// SIO.Directory.GetLogicalDrives() is not implemented on Unity < 2019!!
				foreach (string di in SIO.Directory.GetLogicalDrives())
					Console.WriteLine(di);

			} catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				string stackTrace = Environment.StackTrace;
				Console.WriteLine(stackTrace);
			}
			try
			{
				foreach (SIO.DriveInfo di in SIO.DriveInfo.GetDrives())
					Console.WriteLine(di.Name);

			} catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				string stackTrace = Environment.StackTrace;
				Console.WriteLine(stackTrace);
			}
		}

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

			TestGetDrives();
		}
	}
}
