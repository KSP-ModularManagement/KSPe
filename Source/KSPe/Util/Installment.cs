/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-20 Lisias T : http://lisias.net <support@lisias.net>

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
		public class Exception : InstallmentException
		{
			internal Exception(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
		}

		public class WrongDirectoryInstallationException : Exception
		{
			private static readonly string message = @"Unfortunately {0} is installed on the wrong Directory!

It should be installed on {1} but it's currently installed on {2} ! Delete the latter and be sure to install {0} on the former.";
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
				return string.Format(message, this.name, this.intendedPath, this.installedDllPath);
			}
		}

		public static void Check<T>(string name, string vendor = null)
		{
			CheckForWrongDirectoy(typeof(T), name, vendor);
		}

		public static void Check<T>(System.Type versionClass)
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

			Check<T>(name, vendor);
		}

		public static void Check<T>()
		{
			Type versionClass = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
					from tt in assembly.GetTypes()
					where tt.Namespace == typeof(T).Namespace && tt.Name == "Version"
					select tt).FirstOrDefault();
			Check<T>(versionClass);
		}

		private static void CheckForWrongDirectoy(Type type, string name, string vendor)
		{
			string intendedPath = IO.Path.Combine(IO.Path.Origin(), "GameData");
			if (null != vendor) intendedPath = IO.Path.Combine(intendedPath, vendor);
			intendedPath = IO.Path.Combine(intendedPath, name);
			intendedPath = IO.Path.GetFullPath(intendedPath);

			string installedDllPath = IO.Path.GetDirectoryName(IO.Path.GetFullPath(type.Assembly.Location.Replace("Plugins",".").Replace("Plugin",".")));
			if (installedDllPath.StartsWith(intendedPath)) return;

			throw new WrongDirectoryInstallationException(name, intendedPath, installedDllPath);
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
