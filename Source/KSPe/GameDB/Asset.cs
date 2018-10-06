using System;

namespace KSPe.GameDB

{
	public static class Asset<T>
	{
		public static string Solve(string fn)
		{
			string r = KSPe.IO.File<T>.Asset.Solve(fn);
			r = r.Substring(r.IndexOf("GameData/") + 9);
			return r;
		}
	}
}
