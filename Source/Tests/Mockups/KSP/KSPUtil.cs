using System;

public static class KSPUtil
{
	public static string ApplicationRootPath => System.IO.Path.GetDirectoryName(typeof(KSPUtil).Assembly.Location);
}
