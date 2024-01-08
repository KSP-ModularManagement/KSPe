/*
	This file is part of KSPe.External, a component for KSP Enhanced /L
	unless when specified otherwise below this code is:
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
using SIO = System.IO;
using Tiny;

namespace KSPe.Util
{
	public static class AddOnVersionChecker
	{
		[System.Serializable]
		public class Repository
		{
			public string USERNAME;
			public string REPOSITORY;
			public bool ALLOW_PRE_RELEASE;
		}

		[System.Serializable]
		public class Versioning
		{
			public int MAJOR = 0;
			public int MINOR = 0;
			public int PATCH = 0;
			public int BUILD = 0;

			public Versioning(int major, int minor, int patch, int build)
			{
				this.MAJOR = major;
				this.MINOR = minor;
				this.PATCH = patch;
				this.BUILD = build;
			}

			public override int GetHashCode() => this.ToString().GetHashCode();
			public override bool Equals(object other)
			{
				if (null == other) return false;
				if (!(other is Versioning)) return false;
				Versioning o = (other as Versioning);
				return this.MAJOR == o.MAJOR
					&& this.MINOR == o.MINOR
					&& this.PATCH == o.PATCH
					&& this.BUILD == o.BUILD
					;
			}

			public static bool operator ==(Versioning v1, Versioning v2) => null != v1 && v1.Equals(v2);
			public static bool operator !=(Versioning v1, Versioning v2) => null == v1 || !v1.Equals(v2);

			public static bool operator <=(Versioning v1, Versioning v2)
			{
				if (null == v1 || null == v2) return false;

				if (v1.MAJOR > v2.MAJOR) return false;
				if (v1.MAJOR < v2.MAJOR) return true;
				// If we are there, both are equals

				if (v1.MINOR > v2.MINOR) return false;
				if (v1.MINOR < v2.MINOR) return true;
				// If we are there, both are equals

				if (v1.PATCH > v2.PATCH) return false;
				if (v1.PATCH < v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH > v2.PATCH) return false;
				if (v1.PATCH < v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH > v2.PATCH) return false;
				return true;
			}

			public static bool operator >=(Versioning v1, Versioning v2)
			{
				if (null == v1 || null == v2) return false;

				if (v1.MAJOR < v2.MAJOR) return false;
				if (v1.MAJOR > v2.MAJOR) return true;
				// If we are there, both are equals

				if (v1.MINOR < v2.MINOR) return false;
				if (v1.MINOR > v2.MINOR) return true;
				// If we are there, both are equals

				if (v1.PATCH < v2.PATCH) return false;
				if (v1.PATCH > v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH < v2.PATCH) return false;
				if (v1.PATCH > v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH < v2.PATCH) return false;
				return true;
			}

			public static bool operator <(Versioning v1, Versioning v2)
			{
				if (null == v1 || null == v2) return false;

				if (v1.MAJOR > v2.MAJOR) return false;
				if (v1.MAJOR < v2.MAJOR) return true;
				// If we are there, both are equals

				if (v1.MINOR > v2.MINOR) return false;
				if (v1.MINOR < v2.MINOR) return true;
				// If we are there, both are equals

				if (v1.PATCH > v2.PATCH) return false;
				if (v1.PATCH < v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH > v2.PATCH) return false;
				if (v1.PATCH < v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH >= v2.PATCH) return false;
				return true;
			}

			public static bool operator >(Versioning v1, Versioning v2)
			{
				if (null == v1 || null == v2) return false;

				if (v1.MAJOR < v2.MAJOR) return false;
				if (v1.MAJOR > v2.MAJOR) return true;
				// If we are there, both are equals

				if (v1.MINOR < v2.MINOR) return false;
				if (v1.MINOR > v2.MINOR) return true;
				// If we are there, both are equals

				if (v1.PATCH < v2.PATCH) return false;
				if (v1.PATCH > v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH < v2.PATCH) return false;
				if (v1.PATCH > v2.PATCH) return true;
				// If we are there, both are equals

				if (v1.PATCH <= v2.PATCH) return false;
				return true;
			}

			public override string ToString()
			{
				return (string.Format("v{0}.{1}.{2}.{3}", this.MAJOR, this.MINOR, this.PATCH, this.BUILD));
			}
		}

		[System.Serializable]
		public class Version
		{
			public string NAME;
			public string URL;
			public string DOWNLOAD;
			public string CHANGE_LOG;
			public string CHANGE_LOG_URL;
			public Repository GITHUB;
			public Versioning VERSION;
			public Versioning KSP_VERSION;
			public Versioning KSP_VERSION_MIN;
			public Versioning KSP_VERSION_MAX;
		}

		public static Version LoadVersion<T>() => LoadVersion(IO.Hierarchy<T>.GAMEDATA.Solve(typeof(T).Namespace));
		public static Version LoadVersion(string addonName, string vendor = null)
		{
			return LoadVersionFromFile(
				(
					(null != vendor)
						? IO.Hierarchy.GAMEDATA.Solve(vendor, addonName)
						: IO.Hierarchy.GAMEDATA.Solve(addonName)
				) + addonName + ".version"
			);
		}

		public static Version LoadVersionFromFile(string pathname)
		{
			string text = SIO.File.ReadAllText(pathname);
			Version r = Json.Decode<Version>(text);
			return r;
		}
	}
}
