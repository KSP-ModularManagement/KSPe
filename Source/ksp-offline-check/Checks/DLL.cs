/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is double licensed, as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
		* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General Public License 2.0
	along with KSP Enhanced /L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using System.Collections.Generic;
using System.IO;

using kspofflinecheck.util;

namespace kspofflinecheck.Checks
{
	internal static class DLL
	{
		public static void Do(string[] args)
		{
			Console.WriteLine("DLL Integrity check.");
			if (0 == args.Length) Print_Help_and_Exit(0);

			List<string> paths = new List<string>();
			foreach (string arg in args)
			{
				if (arg.StartsWith("-")) { Parse_Argument(arg); continue; }
				if (!Directory.Exists(arg)) { Util.Print_Warning("Directory {0} does not exists!", arg); continue; }
				if (!KSP.Found_in(arg)) { Util.Print_Warning("There's no KSP on {0}!", arg); continue; }
				paths.Add(arg);
			}
			if (0 == paths.Count) Util.Print_Error_and_Exit(-2, "No useable paths were given");

			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += OnReflectionOnlyAssemblyResolve;
			foreach(string p in paths) Check_Assemblies_On(p);
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve -= OnReflectionOnlyAssemblyResolve;
		}

		private static void Print_Help_and_Exit(int exitCode)
		{
			// TODO
			System.Environment.Exit(exitCode);
		}

		private static void Parse_Argument(string arg)
		{
			// TODO
		}

		private static void Check_Assemblies_On(string path)
		{
			path = path
				.Replace("~", Environment.GetEnvironmentVariable("HOME"))
				.Replace("//", "/");
			Console.WriteLine(string.Format("Checking {0}...", path));
			root = KSP.Find_Managed(path);

			HashSet<string> dlls = new HashSet<string>();
			path = Path.Combine(path, "GameData");
			foreach (string f in Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories))
				dlls.Add(f);

			while (0 != dlls.Count)
			{
				int count = dlls.Count;
				HashSet<string> processed = new HashSet<string>();
				foreach (string f in dlls) try
				{
					string relativepathname = f.Replace(path, "");
					Console.Write(string.Format("\t{0}... ", relativepathname));
					Process_DLL(f, relativepathname);
					Console.Write("Ok.");
 					processed.Add(f);
				}
				catch (BadImageFormatException)
				{
					Console.Write("Not a C# Assembly.");
 					processed.Add(f);
				}
				catch (Exception e)
				{
					Console.Write(string.Format("{0} !!", e.GetType().FullName));
				}
				finally
				{
					Console.WriteLine("");
				}
				dlls.RemoveWhere(p => processed.Contains(p));
				if (count == dlls.Count)
				{ 
					Util.Print_Warning("Impossible to continue, the following DLL's are unprocessable: {0}", string.Join(" ; ", dlls));
					return;
				}
			}
		}

		private static Dictionary<string,Dictionary<Type, string>> Process_DLL(string pathname, string relativepathname)
		{
			Dictionary<string,Dictionary<Type, string>> r = new Dictionary<string, Dictionary<Type, string>>();
			byte[] rawAssembly;
			using (FileStream fs = new System.IO.FileStream(pathname, System.IO.FileMode.Open))
			{
				rawAssembly = new byte[(int)fs.Length];
				fs.Read(rawAssembly, 0, rawAssembly.Length);
			}
			System.Reflection.Assembly assembly = System.Reflection.Assembly.ReflectionOnlyLoad(rawAssembly);
			object[] aa = assembly.GetCustomAttributes(false);
			foreach (object o in aa)
			{
				Dictionary<Type, string> map = new Dictionary<Type, string>();
				if (o is System.Reflection.AssemblyTitleAttribute ata)
					map[o.GetType()] = ata.Title;
				else if (o is System.Reflection.AssemblyDescriptionAttribute ada)
					map[o.GetType()] = ada.Description;
				else if (o is System.Reflection.AssemblyConfigurationAttribute aca)
					map[o.GetType()] = aca.Configuration;
				else if (o is System.Reflection.AssemblyCompanyAttribute acoa)
					map[o.GetType()] = acoa.Company;
				else if (o is System.Reflection.AssemblyProductAttribute apa)
					map[o.GetType()] = apa.Product;
				else if (o is System.Reflection.AssemblyCopyrightAttribute acpa)
					map[o.GetType()] = acpa.Copyright;
				else if (o is System.Reflection.AssemblyFileVersionAttribute afva)
					map[o.GetType()] = afva.Version;
				else if (o is System.Reflection.AssemblyTrademarkAttribute atda)
					map[o.GetType()] = atda.Trademark;
				else if (o is System.Reflection.AssemblyInformationalVersionAttribute aiva)
					map[o.GetType()] = aiva.InformationalVersion;
				else if (o is System.Reflection.AssemblyDefaultAliasAttribute adaa)
					map[o.GetType()] = adaa.DefaultAlias;
				else if (o is System.Reflection.AssemblyDelaySignAttribute adsa)
					map[o.GetType()] = adsa.ToString();
				else
					map[o.GetType()] = o.ToString();

				r[relativepathname] = map;
			}
			return r;
		}

		private static string root = "";
		private static System.Reflection.Assembly OnReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			if (!Directory.Exists(root)) System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
			System.Reflection.AssemblyName name = new System.Reflection.AssemblyName(args.Name);
			string asmToCheck = Path.Combine(root, string.Format("{0}.dll", name.Name));
			if (File.Exists(asmToCheck))return System.Reflection.Assembly.ReflectionOnlyLoadFrom(asmToCheck);
			return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name);
		}
	}
}