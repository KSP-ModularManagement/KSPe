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
using System;
using System.Collections.Generic;
using System.Linq;

using IO = KSPe.IO;

namespace KSPe.Util
{
	public class InstallmentException : AbstractException
	{
		internal InstallmentException(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
	}

	public static class Installation
	{
		public class WrongDirectoryInstallationException : InstallmentException
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

		public class MissingDependencyInstallationException : InstallmentException
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

		public class DuplicityInstallationException : InstallmentException
		{
			public readonly string assemblyName;
			public readonly string[] paths;

			private static readonly string message = @"{0} is installed on multiple places on this installment!

{1}
(On KSP 1.8 and newer, this list is not accurate and you must find the copies manually)

Your KSP is running on [{2}]. Check {0}'s INSTALL instructions."
			;

			private static readonly string shortMessage = "There are {1} instances of the Add'On {0}. Only one must exist.";

			internal DuplicityInstallationException(List<AssemblyLoader.LoadedAssembly> loaded): base(shortMessage, loaded[0].name, loaded.Count)
			{
				this.assemblyName = loaded[0].name;
				List<string> paths = new List<string>();
				foreach (AssemblyLoader.LoadedAssembly l in loaded) paths.Add(l.path);
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
			string name = typeof(T).Namespace;
			try
			{
				name = versionClass.GetField("Namespace").GetValue(null).ToString();
			}
			catch (ArgumentNullException) { }
			catch (NotSupportedException) { }
			catch (FieldAccessException) { }
			catch (ArgumentException) { }
			catch (NullReferenceException) { }

			string vendor = null;
			try
			{
				vendor = versionClass.GetField("Vendor").GetValue(null).ToString();
			}
			catch (ArgumentNullException) { }
			catch (NotSupportedException) { }
			catch (FieldAccessException) { }
			catch (ArgumentException) { }
			catch (NullReferenceException) { }

			Check<T>(name, name, vendor, unique);
		}

		public static void Check<T>()
		{
			Check<T>(true);
		}
		public static void Check<T>(bool unique)
		{
			Type versionClass = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
					from tt in assembly.GetTypes()
					where tt.Namespace == typeof(T).Namespace && tt.Name == "Version"
					select tt).FirstOrDefault();
			Check<T>(versionClass, unique);
		}

		private static void CheckForWrongDirectoy(Type type, string name, string folder, string vendor)
		{
			string intendedPath = IO.Path.Combine(IO.Path.Origin(), "GameData");
			if (null != vendor) intendedPath = IO.Path.Combine(intendedPath, vendor);
			intendedPath = IO.Path.Combine(intendedPath, folder);
			intendedPath = IO.Path.GetFullPath(intendedPath);

			string installedDllPath = IO.Path.GetDirectoryName(IO.Path.GetFullPath(type.Assembly.Location.Replace("Plugins",".").Replace("Plugin",".")));
			if (installedDllPath.StartsWith(intendedPath)) return;

			throw new WrongDirectoryInstallationException(name, intendedPath, installedDllPath);
		}

		private static void CheckForDuplicity(string name)
		{
			IEnumerable<AssemblyLoader.LoadedAssembly> loaded =
				from a in AssemblyLoader.loadedAssemblies
					let ass = a.assembly
					where name == ass.GetName().Name
					orderby a.path ascending
					select a;

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

		public static void Check<T>(System.Type versionClass, System.Type compatibilityClass)
		{
			string name = typeof(T).Namespace;
			try
			{
				name = versionClass.GetField("Namespace").GetValue(null).ToString();
			}
			catch (ArgumentNullException) { }
			catch (NotSupportedException) { }
			catch (FieldAccessException) { }
			catch (ArgumentException) { }
			catch (NullReferenceException) { }

			string versionText = null;
			try
			{
				versionText = versionClass.GetField("Text").GetValue(null).ToString();
			}
			catch (ArgumentNullException) { }
			catch (NotSupportedException) { }
			catch (FieldAccessException) { }
			catch (ArgumentException) { }
			catch (NullReferenceException) { }

			int[] desiredunityVersions;
			try
			{
				desiredunityVersions = (int[])compatibilityClass.GetField("Unity").GetValue(null);
				CheckForCompatibleUnity<T>(name, versionText, desiredunityVersions);
			}
			catch (ArgumentNullException) { }
			catch (NotSupportedException) { }
			catch (FieldAccessException) { }
			catch (ArgumentException) { }
			catch (NullReferenceException) { }
		}

		public static void Check<T>()
		{
			Type versionClass = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
					from tt in assembly.GetTypes()
					where tt.Namespace == typeof(T).Namespace && tt.Name == "Version"
					select tt).FirstOrDefault();
			Type compatibilityClass = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
					from tt in assembly.GetTypes()
					where tt.Namespace == typeof(T).Namespace && tt.Name == "Compatibility"
					select tt).FirstOrDefault();
			Check<T>(versionClass, compatibilityClass);
		}

		public static void CheckForCompatibleUnity<T>(string name, string version, int[] unityVersions)
		{
			foreach (int unityVersion in unityVersions)
				if (UnityTools.UnityVersion == unityVersion) return;

			throw new IncompatibleUnityException(name, version, unityVersions);
		}
	}
}
