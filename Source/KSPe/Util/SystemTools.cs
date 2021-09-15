/*
 	This file is part of KSPe, a component for KSP API Extensions/L
 	(C) 2018-21 Lisias T : http://lisias.net <support@lisias.net>

 	KSPe API Extensions/L is double licensed, as follows:

 	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
 	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

 	And you are allowed to choose the License that better suit your needs.

 	KSPe API Extensions/L is distributed in the hope that it will be useful,
 	but WITHOUT ANY WARRANTY; without even the implied warranty of
 	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

 	You should have received a copy of the SKL Standard License 1.0
 	along with KSPe API Extensions/L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

 	You should have received a copy of the GNU General Public License 2.0
 	along with KSPe API Extensions/L. If not, see <https://www.gnu.org/licenses/>.
*/
namespace KSPe.Util
{
	using System;
	using System.Collections.Generic;
    using System.Threading;
    using Reflection = System.Reflection;
	using Type = System.Type;

	public static class SystemTools
	{
		public static class TypeFinder
		{
			private static readonly Dictionary<string, Type> TYPES = new Dictionary<string, Type>();
			public static bool ExistsByQualifiedName(string qn)
			{
				lock (TYPES)
				{
					if (TYPES.ContainsKey(qn)) return true;
					foreach (System.Reflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
						foreach (System.Type type in assembly.GetTypes()) if (qn.Equals(string.Format("{0}.{1}", type.Namespace, type.Name)))
							{
								TYPES.Add(qn, type);
								return true;
							}
				}
				return false;
			}
			public static Type FindByQualifiedName(string qn)
			{
				lock(TYPES)
				{ 
					if (TYPES.ContainsKey(qn)) return TYPES[qn];
					foreach (System.Reflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
						foreach (System.Type type in assembly.GetTypes()) if (qn.Equals(string.Format("{0}.{1}", type.Namespace, type.Name)))
						{
							TYPES.Add(qn, type);
							return type;
						}
				}
				throw new DllNotFoundException("An Add'On Support DLL was not loaded. Missing type : " + qn);
			}
		}

		public static class Assembly
		{ 
			public class Loader : IDisposable
			{
				protected static readonly object MUTEX = new object();
				private readonly string namespaceOverride;
				protected string searchPath;

				protected Loader() { this.namespaceOverride = null; }
				public Loader(string namespaceOverride, params string[] subdirs)
				{
					this.namespaceOverride = namespaceOverride;
					List<string> parms = new List<string>(subdirs);
					parms.Insert(0, "PluginData");
					string[] sd = parms.ToArray();
					this.searchPath = this.TryPath("Plugins", sd)
										?? this.TryPath("Plugin", sd)
										?? throw new DllNotFoundException(
											string.Format("{0}'s DLL search path does not exists!", this.namespaceOverride)
										)
							;
					this.EnterCritical();
				}

				private string TryPath(string path, params string[] subdirs)
				{
					string t = System.IO.Path.Combine(this.namespaceOverride, path);
					string p = KSPe.IO.Hierarchy.GAMEDATA.SolveFull(false, t, subdirs);
					if (System.IO.Directory.Exists(p))
						return KSPe.IO.Hierarchy.GAMEDATA.Solve(false, t, subdirs);

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

				public Reflection.Assembly LoadAndStartup(string assemblyName)
				{
					return Assembly.LoadAndStartup(assemblyName);
				}
			}

			public class Loader<T> : Loader
			{
				private readonly Type type;

				public Loader(params string[] subdirs) : base()
				{
					this.type = typeof(T);
					List<string> parms = new List<string>(subdirs);
					parms.Insert(0, "PluginData");
					string[] sd = parms.ToArray();
					this.searchPath = this.TryPath("Plugins", sd)
										?? this.TryPath("Plugin", sd)
										?? throw new DllNotFoundException(
											string.Format("{0}'s DLL search path does not exists!", this.type.Namespace)
										)
							;
					this.EnterCritical();
				}

				private string TryPath(string path, params string[] subdirs)
				{
					string p = KSPe.IO.Hierarchy<T>.GAMEDATA.SolveFull(false, path, subdirs);
					// Tremendo furo, eu deveria ter implementado o KSPe.IO.Directory<T> jah...
					if (System.IO.Directory.Exists(p))
						return KSPe.IO.Hierarchy<T>.GAMEDATA.Solve(false, path, subdirs);
					return null;
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

			[Obsolete("Assembly.AddSearchPath(string) will be made internal on Release 2.5")]
			public static void AddSearchPath(string path)
			{
				string fullpath = KSPe.IO.Hierarchy.ROOT.Solve(path);

				if (!System.IO.Directory.Exists(fullpath))
					throw new System.IO.FileNotFoundException(string.Format("The path {0} doesn't resolve to a valid DLL search path!", path));

				lock(CUSTOM_SEARCH_PATHS)
					if (!CUSTOM_SEARCH_PATHS.Contains(path))
						CUSTOM_SEARCH_PATHS.Add(path);
			}

			[Obsolete("Assembly.RemoveSearchPath(string) will be made internal on Release 2.5")]
			public static void RemoveSearchPath(string path)
			{
				lock(CUSTOM_SEARCH_PATHS)
					if (CUSTOM_SEARCH_PATHS.Contains(path))
						CUSTOM_SEARCH_PATHS.Remove(path);
			}

			[Obsolete("Assembly.LoadAndStartup(string) will be made internal on Release 2.5")]
			public static Reflection.Assembly LoadAndStartup(string assemblyName)
			{
				Reflection.Assembly assembly = System.AppDomain.CurrentDomain.Load(assemblyName);
				foreach (Type type in assembly.GetTypes())
				{
					if ("Startup" != type.Name) continue;

					object instance = System.Activator.CreateInstance(type);
					InvokeOrNull(type, instance, "Awake");
					InvokeOrNull(type, instance, "Start");
					break;
				}
				return assembly;
			}

			// These ones don't load the Assembly on the same context from the caller.
			// DAMN. I will keep them however, these ones can be useful somehow.

			[Obsolete("This call doesn't loads the Assembly on the same context as the caller. Unexpected cast problems (among others) can happen. Consider using KSPe.Util.SystemTools.Assembly.AddSearchPath(path) to register a folder and using System.AppDomain.CurrentDomain.Load(name) instead.")]
			public static Reflection.Assembly LoadFromFile(string pathname)
			{
				byte[] rawAssembly;
				using (System.IO.FileStream fs = new System.IO.FileStream(pathname, System.IO.FileMode.Open))
				{
					rawAssembly = new byte[(int)fs.Length];
					fs.Read(rawAssembly, 0, rawAssembly.Length);
				}
				return System.AppDomain.CurrentDomain.Load(rawAssembly);
				//return Reflection.Assembly.LoadFrom(pathname);
			}

			[Obsolete("This call doesn't loads the Assembly on the same context as the caller. Unexpected cast problems (among others) can happen. Consider using KSPe.Util.SystemTools.Assembly.AddSearchPath(path) to register a folder and using KSPe.Util.SystemTools.Assembly.LoadAndStartup(name) instead.")]
			public static Reflection.Assembly LoadFromFileAndStartup(string pathname)
			{
				Reflection.Assembly assembly = LoadFromFile(pathname);
				foreach (Type type in assembly.GetTypes())
				{
					if ("Startup" != type.Name) continue;

					object instance = System.Activator.CreateInstance(type);
					InvokeOrNull(type, instance, "Awake");
					InvokeOrNull(type, instance, "Start");
					break;
				}
				return assembly;
			}

			private static object InvokeOrNull(Type t, object o, string methodName)
			{
				Reflection.MethodInfo method = t.GetMethod(methodName, Reflection.BindingFlags.Public |Reflection.BindingFlags.Instance);
				method = method ?? t.GetMethod(methodName, Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance);
				if (null != method)	return method.Invoke(o, null);
				return null;
			}
		}

		private static readonly System.Collections.Generic.List<string> CUSTOM_SEARCH_PATHS = new System.Collections.Generic.List<string>();
		private static System.Reflection.Assembly AssemblyResolve(object sender, System.ResolveEventArgs args)
		{
			// Ignore missing resources
			if (args.Name.Contains(".resources")) return null;

			// check for assemblies already loaded
			foreach (System.Reflection.Assembly assembly in System.AppDomain.CurrentDomain.GetAssemblies())
				if (assembly.GetName().Name == args.Name)
				{	// We had found it. Let's check for a conflict.
					string asmFile = FindThisGuy(args.Name, false);
					if (null != asmFile && assembly.Location != asmFile)
						UnityEngine.Debug.LogErrorFormat("[KSPe Binder] Found a duplicated Assembly for {0} on file {1}. This is an error, there can be only one! #highlanderFeelings", args.Name, asmFile);
					return assembly;
				}

			{
				string asmFile = FindThisGuy(args.Name, true);
				if (null != asmFile) try
				{
					UnityEngine.Debug.LogFormat("[KSPe Binder] Found it on {0}.", asmFile);
					return System.Reflection.Assembly.LoadFrom(asmFile);
				}
				catch (System.Exception ex)
				{
					UnityEngine.Debug.LogErrorFormat("[KSPe Binder] Error {0} loading {1} from {2}!", ex.Message, args.Name, asmFile);
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

			lock(CUSTOM_SEARCH_PATHS)
				foreach (string path in CUSTOM_SEARCH_PATHS)
				{
					if (verbose) UnityEngine.Debug.LogFormat("[KSPe Binder] Looking for {0} on {1}...", filename, path);
					string asmFile = IO.Path.Combine(path,filename);
					if (System.IO.File.Exists(asmFile)) return asmFile;
				}
			return null;
		}

		static SystemTools()
		{
			System.AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
			UnityEngine.Debug.Log("[KSPe Binder] Hooked.");
		}
	}
}

