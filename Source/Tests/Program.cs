using System;

namespace Tests
{
	public static class MainClass
	{

		private static void TestCase_MiscPaths()
		{
			Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
			Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
			Console.WriteLine(System.IO.Path.Combine("/a/b/c/d/", "e"));
			Console.WriteLine(System.IO.Path.Combine("/a/b/c/d//", "e"));
		}

		private static void TestCase_Raparsing(string root_dir)
		{
			foreach (string d in System.IO.Directory.GetDirectories(root_dir))
			{
				Console.WriteLine(string.Format("Trying {0} ...", d));
				if (!KSPe.Multiplatform.FileSystem.IsReparsePoint(d))
					Console.WriteLine(string.Format("Directory {0} is not a ReparsePoint.", d));
				else
				{
					Console.WriteLine(string.Format("LowLevelTools.Windows32.GetFinalPathName({0}) = {1}", d, KSPe.Multiplatform.LowLevelTools.Windows32.GetFinalPathName(d)));
					string urd = KSPe.Multiplatform.FileSystem.ReparsePath(d);
					Console.WriteLine(string.Format("Directory {0} is unreparsed to {1}", d, urd));
				}
			}

			if (!KSPe.Multiplatform.LowLevelTools.Windows.IsThisWindows) return;

			foreach (string d in System.IO.Directory.GetFiles(root_dir, "*", System.IO.SearchOption.AllDirectories))
			{
				if (!KSPe.Multiplatform.FileSystem.IsReparsePoint(d))
					Console.WriteLine(string.Format("File {0} is not a ReparsePoint.", d));
				else
				{
					string urd = KSPe.Multiplatform.FileSystem.ReparsePath(d);
					Console.WriteLine(string.Format("File {0} is unreparsed to {1}", d, urd));
				}
			}
		}

		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Console.WriteLine(Environment.GetCommandLineArgs()[0]);

			TestCase_MiscPaths();
			TestCase_Raparsing(Environment.GetCommandLineArgs()[1]);
		}
	}
}
