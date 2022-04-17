using System;
using System.Collections.Generic;

namespace Tests
{
	public static class MainClass
	{
		private static void TestCase_StackDump()
		{
			System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
			foreach (System.Diagnostics.StackFrame frame in st.GetFrames())
			{
				string classname = frame.GetMethod().DeclaringType.Name;
				string methodname = frame.GetMethod().ToString();
				Console.WriteLine(classname + "::" + methodname);
			}
		}

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

		private static readonly String CheckForWrongDirectoy_PLUGINS = "/Plugins/";
		private static readonly String CheckForWrongDirectoy_PLUGIN = "/Plugins/";
		private static readonly String CheckForWrongDirectoy_DOT = "/./";
		private static readonly String CheckForWrongDirectoy_GAMEDATA = "/GameData/";
		private static void TestCase_InstallPath()
		{
			string intendedPath = System.IO.Path.Combine("/Users/lisias/Workspaces/KSP/runtime/1.12.0 with Plugins", "GameData");
			intendedPath = System.IO.Path.Combine(intendedPath, "TweakScale");
			intendedPath += "/";

			string installedDllPath = System.IO.Path.GetDirectoryName("/Users/lisias/Workspaces/KSP/runtime/1.12.0 with Plugins/GameData/TweakScale/Plugins/Scale.dll");
			installedDllPath += "/";

			{
				// get rid of any Plugins or Plugin subdirs, but only inside the GameData
				int pos;
				if ((pos = installedDllPath.IndexOf(CheckForWrongDirectoy_GAMEDATA)) < 0)
					return;
				pos += CheckForWrongDirectoy_GAMEDATA.Length;

				string baseIntendedPath = installedDllPath.Substring(0, pos);
				string postfixIntendedPath = installedDllPath.Substring(pos);
				postfixIntendedPath = postfixIntendedPath.Replace(CheckForWrongDirectoy_PLUGINS,CheckForWrongDirectoy_DOT).Replace(CheckForWrongDirectoy_PLUGIN,CheckForWrongDirectoy_DOT);
				installedDllPath = System.IO.Path.Combine(baseIntendedPath, postfixIntendedPath);
			}

			Console.WriteLine((installedDllPath.StartsWith(intendedPath)));
		}

		private static void DumbTest()
		{
			List<string> names = new List<string>("net.lisias.ksp.test.debug".Split('.'));
			int i = names.Count - 1;
			for (;i > 0;--i)
			{
				string subnamespace = String.Join(".", names.GetRange(0, i).ToArray());
				Console.WriteLine("* " + subnamespace);
			}
		}

		private class ActiveState
		{
			readonly bool v;
			protected ActiveState(bool v) { this.v = v; }
			public static implicit operator ActiveState(bool v) => new ActiveState(v);
			public static implicit operator bool(ActiveState s) => s.v;
			public override bool Equals(object o) => o is ActiveState && this.v == ((ActiveState)o).v;
			public override int GetHashCode() => this.v.GetHashCode();
		}

		private static void T()
		{
			Dictionary<object, string> d = new Dictionary<object, string>();
			ActiveState a = false;
			ActiveState b = true;
			ActiveState c = true;

			d[a] = "a";
			d[b] = "b";
			d[c] = "c";

			Console.WriteLine(string.Format("a == a {0}", a == a));
			Console.WriteLine(string.Format("a equals a {0}", a.Equals(a)));
			Console.WriteLine(string.Format("a == b {0}", a == b));
			Console.WriteLine(string.Format("a equals b {0}", a.Equals(b)));
			Console.WriteLine(string.Format("c == b {0}", c == b));
			Console.WriteLine(string.Format("c equals b {0}", c.Equals(b)));


		}

		private class Dummy { }

		private static void InstallationTest()
		{
			KSPe.Util.Compatibility.Check<Dummy>();
			KSPe.Util.Installation.Check<Dummy>();
		}

		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			Console.WriteLine(Environment.GetCommandLineArgs()[0]);

			TestCase_StackDump();

			TestCase_InstallPath();

			TestCase_MiscPaths();
			//TestCase_Raparsing(Environment.GetCommandLineArgs()[1]);

			DumbTest();

			InstallationTest();
		}
	}
}
