/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-19 Lisias T : http://lisias.net <support@lisias.net>

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
using System.Linq;
using SIO = System.IO;

namespace KSPe.Util
{
	public static class Installaltion
	{
		public class InstallationException : AbstractException
		{
			internal InstallationException(string shortMessage, params object[] @params) : base(shortMessage, @params) { }
		}

		public class WrongDirectoryInstallationException : InstallationException
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
				return string.Format(message, name, intendedPath, installedDllPath);
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
			string kspRoot = SIO.Path.GetFullPath(KSPUtil.ApplicationRootPath);

			string intendedPath = SIO.Path.Combine(kspRoot, "GameData");
			if (null != vendor)	intendedPath = SIO.Path.Combine(intendedPath, vendor);
			intendedPath = SIO.Path.Combine(intendedPath, name) + "/";
			int dashCount = intendedPath.Length - intendedPath.Replace("/", "").Length;

			string installedDllPath =  SIO.Path.GetDirectoryName(SIO.Path.GetFullPath(type.Assembly.Location)) + "/";
			installedDllPath = string.Join("/", installedDllPath.Split('/').Take(dashCount).ToArray()) + "/";

			if (installedDllPath.StartsWith(intendedPath)) return;

			throw new WrongDirectoryInstallationException(name, intendedPath, installedDllPath);
		}
	}
}
