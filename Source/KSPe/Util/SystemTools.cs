/*
	This file is part of KSPe.Loader, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe.Loader is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe.Loader is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe.Loader. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
namespace KSPe.Loader.Util
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using SType = System.Type;

	using SIO = System.IO;
	using SReflection = System.Reflection;


	internal static class SystemTools
	{
		public abstract class Exception:System.Exception
		{
			protected Exception(string message, params object[] @params) : base(string.Format(message, @params)) { }
		}

		public static class Path
		{
			public static readonly string ROOT = SIO.Path.GetFullPath(KSPUtil.ApplicationRootPath);
			public static readonly string GAMEDATA = SIO.Path.Combine(ROOT, "GameData");
			public static readonly string MYSELF = SIO.Path.Combine(GAMEDATA, "000_KSPe");

			internal static string Combine(string p, params string[] subdirs)
			{
				foreach (string s in subdirs) p = SIO.Path.Combine(p, s);
				return p;
			}

			internal static string Normalize(string path) => path.Replace(ROOT, "");
		}

		public static class Assembly
		{
			public class Exception : SystemTools.Exception
			{
				public readonly string offendingAssembly;
				protected Exception(string message, string assemblyName) : base(message, assemblyName)
				{
					this.offendingAssembly = assemblyName;
				}
			}

			public static class Exists
			{
				public static bool ByName(string qn)
				{
					foreach (SReflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies()) if (assembly.GetName().Name.Equals(qn))
					{
						return true;
					}
					return false;
				}
			}

			public static class Find
			{
				public static SReflection.Assembly ByName(string qn)
				{
					foreach (SReflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies()) if (assembly.GetName().Name.Equals(qn))
					{
						return assembly;
					}
					throw new DllNotFoundException("An Add'On Support DLL was not loaded. Missing Assembly : " + qn);
				}
			}

			public static class Search
			{
				public static IEnumerable<SReflection.Assembly> ByName(string name)
				{
					foreach (SReflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
						if (assembly.GetName().Name.Equals(name))
							yield return assembly;
				}
			}

			public class Loader : IDisposable
			{
				public class Exception : Assembly.Exception
				{
					protected Exception(string message, string assemblyName) : base(message, assemblyName) { }
				}

				public class AlreadyLoadedException : Exception
				{
					private static readonly string MESSAGE = "The Assembly {0} is already loaded!";
					internal AlreadyLoadedException(string assemblyName) : base(MESSAGE, assemblyName) { }
				}

				protected static readonly object MUTEX = new object();
				protected readonly string effectivePath;
				protected readonly string searchPath;

				public Loader() : this(new string[0]) { }
				public Loader(params string[] subdirs)
				{
					this.effectivePath = Path.MYSELF;
					this.searchPath = this.buildSearchPath(subdirs);
					Log.debug("Assembly searchPath: {0}", this.searchPath);
					this.EnterCritical();
				}
				protected string buildSearchPath(params string[] subdirs)
				{
					List<string> parms = new List<string>(subdirs);
					parms.Insert(0, "PluginData");
					string[] sd = parms.ToArray();
					return this.TryPath("Plugins", sd)
										?? this.TryPath("Plugin", sd)
										?? this.TryPath(".", sd)
										?? throw new DllNotFoundException(
											string.Format("{0}'s DLL search path does not exists!", Path.Normalize(this.effectivePath))
										)
							;
				}

				protected virtual string TryPath(string path, params string[] subdirs)
				{
					string t = SIO.Path.Combine(this.effectivePath, path);
					Log.debug("Assembly TryPath: {0} {1}", t, subdirs);
					string p = Path.Combine(t, subdirs);
					if (SIO.Directory.Exists(p))
						return p;

					return null;
				}

				protected void EnterCritical()
				{
					Monitor.Enter(MUTEX);
					Assembly.AddSearchPath(this.searchPath);

				}

				void IDisposable.Dispose()
				{
					Assembly.RemoveSearchPath(this.searchPath);
					Monitor.Exit(MUTEX);
				}

				public SReflection.Assembly LoadAndStartup(string assemblyName)
				{
					if (Assembly.Exists.ByName(assemblyName))
						throw new AlreadyLoadedException(assemblyName); 
					return Assembly.LoadAndStartup(assemblyName);
				}
			}

			// Obrigatory reading:
			//	https://flylib.com/books/en/4.331.1.56/1/
			//	https://weblog.west-wind.com/posts/2016/dec/12/loading-net-assemblies-out-of-seperate-folders
			//	https://docs.microsoft.com/en-us/archive/blogs/suzcook/loadfile-vs-loadfrom
			//	https://docs.microsoft.com/en-us/dotnet/api/system.appdomain.load?view=netcore-3.1
			//	https://jeremylindsayni.wordpress.com/2019/02/11/instantiating-a-c-object-from-a-string-using-activator-createinstance-in-net/
			// Solution used:
			//	from https://weblog.west-wind.com/posts/2016/dec/12/loading-net-assemblies-out-of-seperate-folders
			//	see also https://docs.microsoft.com/en-us/dotnet/standard/assembly/resolve-loads?redirectedfrom=MSDN

			internal static void AddSearchPath(string path)
			{
				string fullpath = SIO.Path.Combine(Path.ROOT, path);
					
				if (!SIO.Directory.Exists(fullpath))
					throw new SIO.FileNotFoundException(string.Format("The path {0} doesn't resolve to a valid DLL search path!", path));

				Log.debug("AddSearchPath {0}", path);
				lock(CUSTOM_SEARCH_PATHS)
					if (!CUSTOM_SEARCH_PATHS.Contains(path))
						CUSTOM_SEARCH_PATHS.Add(path);
				Log.debug("CUSTOM_SEARCH_PATHS : {0}", string.Join(":", CUSTOM_SEARCH_PATHS.ToArray()));
			}

			internal static void RemoveSearchPath(string path)
			{
				Log.debug("RemoveSearchPath {0}", path);
				lock(CUSTOM_SEARCH_PATHS)
					if (CUSTOM_SEARCH_PATHS.Contains(path))
						CUSTOM_SEARCH_PATHS.Remove(path);
				Log.debug("CUSTOM_SEARCH_PATHS : {0}", string.Join(":", CUSTOM_SEARCH_PATHS.ToArray()));
			}

			public static SReflection.Assembly LoadAndStartup(string assemblyName)
			{
				SReflection.Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
				foreach (SType type in assembly.GetTypes())
				{
					if ("Startup" != type.Name) continue;

					object instance = Activator.CreateInstance(type);
					InvokeOrNull(type, instance, "Awake");
					InvokeOrNull(type, instance, "Start");
					break;
				}
				return assembly;
			}

			private static object InvokeOrNull(SType t, object o, string methodName)
			{
				SReflection.MethodInfo method = t.GetMethod(methodName, SReflection.BindingFlags.Public | SReflection.BindingFlags.Instance);
				method = method ?? t.GetMethod(methodName, SReflection.BindingFlags.NonPublic | SReflection.BindingFlags.Instance);
				if (null != method)	return method.Invoke(o, null);
				return null;
			}

			private static readonly System.Collections.Generic.List<string> CUSTOM_SEARCH_PATHS = new System.Collections.Generic.List<string>();
			private static SReflection.Assembly AssemblyResolve(object sender, System.ResolveEventArgs args)
			{
				// Ignore missing resources
				if (args.Name.Contains(".resources")) return null;

				// check for assemblies already loaded
				foreach (SReflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
					if (assembly.GetName().Name == args.Name)
					{	// We had found it. Let's check for a conflict.
						string asmFile = FindThisGuy(args.Name, false);
						if (null != asmFile && assembly.Location != asmFile)
							Log.error("Found a duplicated Assembly for {0} on file {1}. This is an error, there can be only one! #highlanderFeelings", args.Name, asmFile);
						return assembly;
					}

				{
					string asmFile = FindThisGuy(args.Name, true);
					if (null != asmFile) try
					{
						Log.force("Found it on {0}.", asmFile);
						//return LoadAssemblyByKsp(args.Name, asmFile);
						return SReflection.Assembly.LoadFrom(asmFile);
					}
					catch (System.Exception ex)
					{
						Log.error("Error {0} loading {1} from {2}!", ex.Message, args.Name, asmFile);
						return null;
					}
				}

				return null;
			}

			private static string FindThisGuy(string assemblyName, bool verbose)
			{
				// Try to load by filename - split out the filename of the full assembly name
				// and append the base path of the original assembly (ie. look in the same dir)
				string filename = assemblyName.Split(',')[0] + ".dll";
				Log.debug("Finding {0} using {1}", assemblyName, filename);
				lock(CUSTOM_SEARCH_PATHS)
					foreach (string path in CUSTOM_SEARCH_PATHS)
					{
						if (verbose) Log.force("Looking for {0} on {1}...", filename, path);
						string fullpath = SIO.Path.Combine(Path.ROOT, path);
						string asmFile = SIO.Path.Combine(fullpath,filename);
						if (SIO.File.Exists(asmFile)) return asmFile;
					}
				return null;
			}

			internal static void init() => System.AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
			internal static void deinit() => System.AppDomain.CurrentDomain.AssemblyResolve -= AssemblyResolve;
		}
	}
}

