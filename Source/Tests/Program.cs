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
		}
	}
}
