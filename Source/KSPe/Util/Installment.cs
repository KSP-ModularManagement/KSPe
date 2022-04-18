/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-22 LisiasT : http://lisias.net <support@lisias.net>

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
using System;
using System.Collections.Generic;
using System.Linq;

using SIO = System.IO;
using SReflection = System.Reflection;
using KAssemblyLoader = AssemblyLoader;
using IO = KSPe.IO;
using SystemTools = KSPe.Util.SystemTools;

namespace KSPe.Util
{
	public class InstallmentException : AbstractException
	{
		internal InstallmentException(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
	}

	public static class Installation
	{
		public class Exception : InstallmentException
		{
			internal Exception(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
		}

		public class WrongDirectoryInstallationException : Exception
		{
			private static readonly string message = @"{0} is installed on the wrong Directory!

It should be installed on [{1}] but it's currently installed on [{2}] ! Delete the latter and be sure to install {0} on the former.

Your KSP is running on [{3}]."
			;

			private static readonly string shortMessage = "{0} should be installed on [{1}], not on [{2}].";

			public readonly string name;
			public readonly string intendedPath;
			public readonly string installedDllPath;

			internal WrongDirectoryInstallationException(string name, string intendedPath, string installedDllPath) : base(shortMessage, name, intendedPath, installedDllPath)
			{
				this.name = name;
				this.intendedPath = intendedPath;
				this.installedDllPath = installedDllPath;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.name
					, IO.Hierarchy.CalculateRelativePath(this.intendedPath, IO.Hierarchy.ROOTPATH)
					, IO.Hierarchy.CalculateRelativePath(this.installedDllPath, IO.Hierarchy.ROOTPATH)
					, IO.Hierarchy.ROOTPATH
				);
			}
		}

		public class MissingDependencyInstallationException : Exception
		{
			public readonly string assemblyName;

			private static readonly string message = @"{0} was not found on this installment!

You need to install it.

Your KSP is running in [{1}]. Check {0}'s INSTALL instructions."
			;

			private static readonly string shortMessage = "There is no instance of the Add'On {0}. You need to install it.";

			internal MissingDependencyInstallationException(string assemblyName): base(shortMessage, assemblyName)
			{
				this.assemblyName = assemblyName;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.assemblyName, IO.Hierarchy.ROOTPATH);
			}
		}

		public class DuplicityInstallationException : Exception
		{
			public readonly string assemblyName;
			public readonly string[] paths;

			private static readonly string message = @"{0} is installed on multiple places on this installment!

{1}
(On KSP 1.8 and newer, this list is not accurate and you must find the copies manually)

Your KSP is running on [{2}]. Check {0}'s INSTALL instructions."
			;

			private static readonly string shortMessage = "There are {1} instances of the Add'On {0}. Only one must exist.";

			internal DuplicityInstallationException(List<KAssemblyLoader.LoadedAssembly> loaded): base(shortMessage, loaded[0].name, loaded.Count)
			{
				this.assemblyName = loaded[0].name;
				List<string> paths = new List<string>();
				foreach (KAssemblyLoader.LoadedAssembly l in loaded) paths.Add(l.path);
				this.paths = paths.ToArray();
			}

			public override string ToLongMessage()
			{
				List<string> paths = new List<string>();
				foreach(string path in this.paths) paths.Add(string.Format("* {0}\n", IO.Hierarchy.CalculateRelativePath(path, IO.Hierarchy.ROOTPATH)));
				return string.Format(message, this.assemblyName, string.Join("", paths.ToArray()), IO.Hierarchy.ROOTPATH);
			}
		}

		public static void Check<T>(string name, bool unique = true)
		{
			Check<T>(name, name, null, unique);
		}
		public static void Check<T>(string name, string vendor)
		{
			Check<T>(name, name, vendor, true);
		}
		public static void Check<T>(string name, string folder, string vendor)
		{
			Check<T>(name, folder, vendor, true);
		}
		public static void Check<T>(string name, string folder, string vendor, bool unique)
		{
			if (unique) CheckForDuplicity(name);
			CheckForWrongDirectoy(typeof(T), name, folder, vendor);
		}

		public static void Check<T>(System.Type versionClass)
		{
			Check<T>(versionClass, true);
		}
		public static void Check<T>(System.Type versionClass, bool unique = true)
		{
			string name = SystemTools.Reflection.Version.NameSpace(versionClass);
			string vendor = SystemTools.Reflection.Version.Vendor(versionClass);
			Check<T>(name, name, vendor, unique);
		}

		public static void Check<T>()
		{
			Check<T>(true);
		}
		public static void Check<T>(bool unique)
		{
			Type versionClass = SystemTools.Reflection.Version<T>.Class;
			Check<T>(versionClass, unique);
		}

		private static readonly String CheckForWrongDirectoy_PLUGINS = string.Format("{0}Plugins{1}", IO.Path.DirectorySeparatorStr, IO.Path.DirectorySeparatorStr);
		private static readonly String CheckForWrongDirectoy_PLUGIN = string.Format("{0}Plugins{1}", IO.Path.DirectorySeparatorStr, IO.Path.DirectorySeparatorStr);
		private static readonly String CheckForWrongDirectoy_DOT = string.Format("{0}.{1}", IO.Path.DirectorySeparatorStr, IO.Path.DirectorySeparatorStr);
		private static readonly String CheckForWrongDirectoy_GAMEDATA = string.Format("{0}GameData{1}", IO.Path.DirectorySeparatorStr, IO.Path.DirectorySeparatorStr);
		private static void CheckForWrongDirectoy(Type type, string name, string folder, string vendor)
		{
			string intendedPath = IO.Path.Combine(IO.Path.Origin(), "GameData");
			if (null != vendor) intendedPath = IO.Path.Combine(intendedPath, vendor);
			intendedPath = IO.Path.Combine(intendedPath, folder);
			intendedPath = IO.Path.GetFullPath(intendedPath, true);

			string installedDllPath = IO.Path.GetDirectoryName(IO.Path.GetFullPath(type.Assembly.Location));

			{
				// get rid of any Plugins or Plugin subdirs, but only inside the GameData
				int pos;
				if ((pos = installedDllPath.IndexOf(CheckForWrongDirectoy_GAMEDATA)) < 0)
					throw new WrongDirectoryInstallationException(name, intendedPath, type.Assembly.Location);
				pos += CheckForWrongDirectoy_GAMEDATA.Length;

				string baseIntendedPath = installedDllPath.Substring(0, pos);
				string postfixIntendedPath = installedDllPath.Substring(pos);
				postfixIntendedPath = postfixIntendedPath.Replace(CheckForWrongDirectoy_PLUGINS,CheckForWrongDirectoy_DOT).Replace(CheckForWrongDirectoy_PLUGIN,CheckForWrongDirectoy_DOT);
				installedDllPath = IO.Path.Combine(baseIntendedPath, postfixIntendedPath);
			}

			if (installedDllPath.StartsWith(intendedPath)) return;

			throw new WrongDirectoryInstallationException(name, intendedPath, installedDllPath);
		}

		private static void CheckForDuplicity(string name)
		{
			IEnumerable<KAssemblyLoader.LoadedAssembly> loaded = AssemblyLoader.Search.ByName(name);

			if (0 == loaded.Count()) throw new MissingDependencyInstallationException(name);
			if (1 != loaded.Count()) throw new DuplicityInstallationException(loaded.ToList());
		}
	}

	public static class Compatibility
	{
		public class Exception : InstallmentException
		{
			internal Exception(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
		}

		public class IncompatibleUnityException : Exception
		{
			private static readonly string message = @"Unfortunately {0} {1} is not compatible with currently running Unity {2}!

It will only run on the following Unity Versions [ {3} ] ! Install {0} on a KSP with a compatible Unity runtime : {4}.";
			private static readonly string shortMessage = "{0} {1} is incompatible  with Unity in use.";

			public readonly string name;
			public readonly string version;
			public readonly int[] desiredUnityVersions;

			internal IncompatibleUnityException(string name, string version, int[] desiredUnityVersions) : base(shortMessage, name, version)
			{
				this.name = name;
				this.version = version;
				this.desiredUnityVersions = desiredUnityVersions;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.name, this.version, UnityTools.UnityVersion, this.JoinUnityVersions(), this.JoinKSPVersions());
			}

			private string JoinUnityVersions()
			{
				return String.Join(", ", this.desiredUnityVersions.Select(p=>p.ToString()).ToArray());
			}

			private string JoinKSPVersions() // TODO: To filter KSP versions compatible with the Add'On!
			{
				List<KSP.Version> compatibleOnes = new List<KSP.Version>();
				foreach (int desiredUnityVersion in this.desiredUnityVersions)
					compatibleOnes.AddRange(KSP.Version.FindByUnity(desiredUnityVersion));
				return String.Join(", ", compatibleOnes.Select(p=>p.ToString()).ToArray());
			}
		}

		public class IncompatibleKSPException : Exception
		{
			private static readonly string message = @"Unfortunately {0} {1} is not compatible with currently running KSP {2}!

It will only run on the following KSP Versions [ {3} ] ! Install {0} on a compatible KSP.";
			private static readonly string shortMessage = "{0} {1} is incompatible  with KSP in use.";

			public readonly string name;
			public readonly string version;
			public readonly KSP.Version min;
			public readonly KSP.Version max;

			internal IncompatibleKSPException(string name, string version, KSP.Version min, KSP.Version max) : base(shortMessage, name, version)
			{
				this.name = name;
				this.version = version;
				this.min = min;
				this.max = max;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.name, this.version, KSP.Version.Current, this.JoinKSPVersions());
			}

			private string JoinKSPVersions()
			{
				return string.Format("{0} .. {1}", this.min, this.max);
			}
		}

		public abstract class IncompatibleArfefactException : Exception
		{
			internal IncompatibleArfefactException(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
		}

		public class ConflictTypeException : IncompatibleArfefactException
		{
			public readonly string offendedName;
			public readonly string offendedVersion;
			public readonly Type offender;

			private static readonly string message = @"{0} {1} found a conflicting Type installed!

The Type ""{2}"" from ""{3}"" is conflicting with {0}.

You need to <b>completely</b> remove ""{4}"" and its respective files and directories."
			;

			private static readonly string shortMessage = "{0} {1} found a conflicting type called {2}. The respective Add'On must be uninstalled.";

			internal ConflictTypeException(string offendedName, string offendedVersion, Type offender): base(shortMessage, offendedName, offendedVersion, offender.FullName)
			{
				this.offendedName = offendedName;
				this.offendedVersion = offendedVersion;
				this.offender = offender;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.offendedName, this.offendedVersion, this.offender.FullName, this.offender.Assembly.FullName, SIO.Path.GetDirectoryName(this.offender.Assembly.Location));
			}
		}

		public class ConflictAssemblyException : IncompatibleArfefactException
		{
			public readonly string offendedName;
			public readonly string offendedVersion;
			public readonly SReflection.Assembly offender;

			private static readonly string message = @"{0} {1} found an incompatible Assembly installed!

The Asssembly ""{2}"" is not compatible with {0}.

You need to <b>completely</b> remove ""{3}"" and its respective files and directories."
			;

			private static readonly string shortMessage = "{0} {1} found an incompatible type called {2}. The respective Add'On must be uninstalled.";

			internal ConflictAssemblyException(string offendedName, string offendedVersion, SReflection.Assembly offender): base(shortMessage, offendedName, offendedVersion, offender.FullName)
			{
				this.offendedName = offendedName;
				this.offendedVersion = offendedVersion;
				this.offender = offender;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.offendedName, this.offendedVersion, this.offender.FullName, SIO.Path.GetDirectoryName(this.offender.Location));
			}
		}

		public abstract class MissingDependencyException : Exception
		{
			internal MissingDependencyException(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
		}

		public class MissingDependencyTypeException : MissingDependencyException
		{
			public readonly string offendedName;
			public readonly string offendedVersion;
			public readonly string offenderName;

			private static readonly string message = @"{0} {1} didn't found a needed Type!

The Type ""{2}"" is missing on this Installment.

You need to install the Add'On than provides the missing Type ""{2}""."
			;

			private static readonly string shortMessage = "{0} {1} didn't found a needed Type called {2}. The respective Add'On must be installed.";

			internal MissingDependencyTypeException(string offendedName, string offendedVersion, string offenderName): base(shortMessage, offendedName, offendedVersion, offenderName)
			{
				this.offendedName = offendedName;
				this.offendedVersion = offendedVersion;
				this.offenderName = offenderName;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.offendedName, this.offendedVersion, this.offenderName);
			}
		}

		public class MissingDependencyAssemblyException : MissingDependencyException
		{
			public readonly string offendedName;
			public readonly string offendedVersion;
			public readonly string offenderName;

			private static readonly string message = @"{0} {1} didn't found a needed Assembly!

The Assembly ""{2}"" is missing on this Installment.

You need to install the Add'On than provides the missing Assembly ""{2}""."
			;

			private static readonly string shortMessage = "{0} {1} didn't found a needed Assembly called {2}. The respective Add'On must be installed.";

			internal MissingDependencyAssemblyException(string offendedName, string offendedVersion, string offenderName): base(shortMessage, offendedName, offendedVersion, offenderName)
			{
				this.offendedName = offendedName;
				this.offendedVersion = offendedVersion;
				this.offenderName = offenderName;
			}

			public override string ToLongMessage()
			{
				return string.Format(message, this.offendedName, this.offendedVersion, this.offenderName);
			}
		}


		public static void Check<T>(System.Type versionClass, System.Type configurationClass)
		{
			string name = SystemTools.Reflection.Version.NameSpace(versionClass);
			string versionText = SystemTools.Reflection.Version.Text(versionClass);

			{
				int[] list = SystemTools.Reflection.Configuration.Unity(configurationClass);
				UnityEngine.Debug.LogFormat("*** desiredunityVersions {0}", list.Length);
				CheckForCompatibleUnity<T>(name, versionText, list);
			}
			{
				KSPe.Util.KSP.Version min = SystemTools.Reflection.Configuration.KSP.Min(configurationClass);
				KSPe.Util.KSP.Version max = SystemTools.Reflection.Configuration.KSP.Max(configurationClass);
				UnityEngine.Debug.LogFormat("*** desiredKSP Versions [{0}...{1}]", min, max);
				CheckForCompatibleKSP<T>(name, versionText, min, max);
			}
			{
				string[] list = SystemTools.Reflection.Configuration.Dependencies.Assemblies(configurationClass);
				UnityEngine.Debug.LogFormat("*** dependencyAssemblies {0}", list.Length);
				CheckForDependencyAssemblies<T>(name, versionText, list);
			}
			{
				string[] list = SystemTools.Reflection.Configuration.Dependencies.Types(configurationClass);
				UnityEngine.Debug.LogFormat("*** dependencyTypes {0}", list.Length);
				CheckForDependencyTypes<T>(name, versionText, list);
			}
			{
				string[] list = SystemTools.Reflection.Configuration.Conflicts.Assemblies(configurationClass);
				UnityEngine.Debug.LogFormat("*** conflictAssemblies {0}", list.Length);
				CheckForConflictAssemblies<T>(name, versionText, list);
			}
			{
				string[] list = SystemTools.Reflection.Configuration.Conflicts.Types(configurationClass);
				UnityEngine.Debug.LogFormat("*** conflictTypes {0}", list.Length);
				CheckForConflictTypes<T>(name, versionText, list);
			}
		}

		public static void Check<T>()
		{
			Type versionClass = SystemTools.Reflection.Version<T>.Class;
			Type configurationClass = SystemTools.Reflection.Configuration<T>.Class;
			Check<T>(versionClass, configurationClass);
		}

		public static void CheckForCompatibleUnity<T>(string name, string version, int[] unityVersions)
		{
			if (0 == unityVersions.Length) return;
			foreach (int unityVersion in unityVersions)
				if (UnityTools.UnityVersion == unityVersion) return;

			throw new IncompatibleUnityException(name, version, unityVersions);
		}

		public static void CheckForCompatibleKSP<T>(string name, string version, KSP.Version min, KSP.Version max)
		{
			if (min <= KSP.Version.Current && KSP.Version.Current <= max) return;
			throw new IncompatibleKSPException(name, version, min, max);
		}

		public static void CheckForConflictTypes<T>(string name, string version, string[] types)
		{
			foreach (string s in types) if (SystemTools.Type.Finder.ExistsByQualifiedName(s))
			{
				System.Type offender = SystemTools.Type.Finder.FindByQualifiedName(s);
				throw new ConflictTypeException(name, version, offender);
			}
		}

		public static void CheckForConflictAssemblies<T>(string name, string version, string[] types)
		{
			foreach (string s in types) if (SystemTools.Assembly.Finder.ExistsByName(s))
			{
				SReflection.Assembly offender = SystemTools.Assembly.Finder.FindByName(s);
				throw new ConflictAssemblyException(name, version, offender);
			}
		}

		public static void CheckForDependencyTypes<T>(string name, string version, string[] types)
		{
			foreach (string s in types) if (!SystemTools.Type.Finder.ExistsByQualifiedName(s))
				throw new MissingDependencyTypeException(name, version, s);
		}

		public static void CheckForDependencyAssemblies<T>(string name, string version, string[] types)
		{
			foreach (string s in types) if (!SystemTools.Assembly.Finder.ExistsByName(s))
				throw new MissingDependencyAssemblyException(name, version, s);
		}
	}
}
