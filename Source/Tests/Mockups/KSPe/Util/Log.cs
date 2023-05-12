using System;
namespace KSPe.Util
{
	public static class Log
	{
		public class Logger
		{
			public static Logger CreateForType<T>(string v1, string v2, int v3)
			{
				return new Logger();
			}

			public void force(string msg, params object[] @params)
			{
				Console.WriteLine(string.Format(msg, @params));
			}

			public void error(string msg, params object[] @params)
			{
				Console.WriteLine(string.Format(msg, @params));
			}

			public void detail(string msg, params object[] @params)
			{
				Console.WriteLine(string.Format(msg, @params));
			}

			public void trace(string msg, params object[] @params)
			{
				Console.WriteLine(string.Format(msg, @params));
			}

			public void debug(string msg, params object[] @params)
			{
				Console.WriteLine(string.Format(msg, @params));
			}
		}
	}

	public static class LOG
	{
		private static Log.Logger log = new Log.Logger();

		internal static void debug(string msg, params object[] @params)
		{
			log.debug(msg, @params);
		}
	}
}
