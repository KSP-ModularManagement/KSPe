using System;
namespace KSPe
{
	public static class Log
	{
		internal static void warn(string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
		}

		internal static void detail(string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
		}

		internal static void trace(string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
		}

		internal static void debug(string msg, params object[] @params) => trace(msg, @params);

		internal static void error(string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
		}

		internal static void error(Exception ex, string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
			Console.WriteLine(ex.ToString());
			Console.WriteLine(ex.StackTrace);
		}

		internal static void error(System.Exception ex, object offended)
		{
			Console.WriteLine(ex.StackTrace);
		}
	}
}
