using System;
namespace UnityEngine
{
	public static class Debug
	{
		public static void LogFormat(string msg, params object[] @params)
		{
			Console.WriteLine(string.Format(msg, @params));
		}

		public static void LogWarningFormat(string msg, params object[] @params)
		{
			Console.WriteLine("WARN:" + string.Format(msg, @params));
		}

		public static void LogError(Exception e)
		{
			Console.WriteLine("ERROR:" + e.ToString());
		}
	}
}
