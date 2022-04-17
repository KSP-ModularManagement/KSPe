using System;
namespace KSPe
{
	public static class Log
	{
		internal static void warn(string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
		}

		internal static void debug(string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
		}

		internal static void error(Exception ex, string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
			Console.WriteLine(ex.ToString());
			Console.WriteLine(ex.StackTrace);
		}
	}
}
