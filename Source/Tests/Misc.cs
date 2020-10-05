using System;
using System.Linq;
using SIO = System.IO;

namespace Tests
{
	public static class MisceTests
	{
		public static void Test1()
		{
			Console.WriteLine("MisceTests.Test1");

			{
				Type target = typeof(KSPe.KspConfig);
				var t = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
						 from tt in assembly.GetTypes()
						 where tt.Namespace == target.Namespace && tt.Name == "Version" && tt.GetMembers().Any(m => m.Name == "Vendor")
						 select tt).FirstOrDefault();
				if (null != t)
					Console.WriteLine(t.GetField("Vendor").GetValue(null).ToString());
			}

			{
				string[] files = System.IO.Directory.GetFiles("/Users/lisias/Applications/Games/KSP/Exodus/", "*.cfg", System.IO.SearchOption.AllDirectories);
				for (int i = files.Length; --i >= 0;)
					files[i] = files[i].Replace("/Users/lisias/Applications/Games/KSP/Exodus/", "");
				foreach (string f in files.OrderBy(x => x).ToArray())
				{
					Console.WriteLine(f);
				}
				Console.WriteLine(files.Length.ToString());
			}

			{
				string[] strings = { "S1", "a1", "S11", "a11", "S2", "a2" };
				foreach (string f in strings.OrderBy(x => x).ToArray())
				{
					Console.WriteLine(f);
				}
				Console.WriteLine(SIO.Path.GetFullPath("/Users/lisias/Applications/.././Blah/../Bleh/./"));
			}

			if (System.IO.File.Exists("/Users/lisias/Workspaces/KSP/runtime/1.4.3/GameData/net.lisias.ksp/KramaxAutoPilot/."))
				Console.WriteLine("it File.Exists!");
			if (System.IO.Directory.Exists("/Users/lisias/Workspaces/KSP/runtime/1.4.3/GameData/net.lisias.ksp/KramaxAutoPilot/."))
				Console.WriteLine("it Directory.Exists!");
		}

		internal static string CalculateRelativePath(string fullDestinationPath, string rootPath)
		{
			// from https://social.msdn.microsoft.com/Forums/vstudio/en-US/954346c8-cbe8-448c-80d0-d3fc27796e9c - Wednesday, May 20, 2009 3:37 PM
			string[] startPathParts = SIO.Path.GetFullPath(rootPath).Trim(SIO.Path.DirectorySeparatorChar).Split(SIO.Path.DirectorySeparatorChar);
			string[] destinationPathParts = SIO.Path.GetFullPath(fullDestinationPath).Trim(SIO.Path.DirectorySeparatorChar).Split(SIO.Path.DirectorySeparatorChar);

			int i = 0; // Finds the first difference on both paths (if any)
			int max = Math.Min(startPathParts.Length, destinationPathParts.Length);
			while ((i < max) && startPathParts[i].Equals(destinationPathParts[i], StringComparison.Ordinal))
				++i;

			if (0 == i) return fullDestinationPath;

			System.Text.StringBuilder relativePath = new System.Text.StringBuilder();

			if (i >= startPathParts.Length)
				relativePath.Append("." + SIO.Path.DirectorySeparatorChar); // Just for the LULZ.
			else
				for (int j = i; j < startPathParts.Length; j++) // Adds how many ".." as necessary
					relativePath.Append(".." + SIO.Path.DirectorySeparatorChar);

			for (int j = i; j < destinationPathParts.Length; j++) // And now feeds the remaning directories
				relativePath.Append(destinationPathParts[j] + SIO.Path.DirectorySeparatorChar);

			relativePath.Length--;

			return relativePath.ToString();
		}

		public static void Test_CalculateRelativePath()
		{
			string target = "/Users/lisias/Workspaces/KSP/runtime/1.6.1/GameData/net.lisias.ksp/NavInstruments/PluginData/Textures/hsi_overlay.png";
			string root = "/Users/lisias/Workspaces/KSP/runtime/1.6.1/GameData/net.lisias.ksp/NavInstruments/Plugins/";
			Console.WriteLine(target);
			Console.WriteLine(root);
			Console.WriteLine(CalculateRelativePath(target, root));
		}

		public static void Test_GetFullPath()
		{
			string path = "/Users/lisias/xuxu/../banana/../Blah/./Bleh/Blih";
			string full = KSPe.IO.Path.GetFullPath(path);
			string absolute = KSPe.IO.Path.GetAbsolutePath(path);
			string ms = System.IO.Path.GetFullPath(path);
			Console.WriteLine(path);
			Console.WriteLine(full);
			Console.WriteLine(absolute);
			Console.WriteLine(ms);
		}
	}
}
