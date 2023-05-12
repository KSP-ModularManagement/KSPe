/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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
using System.Globalization;

using SIO = System.IO;
using SType = System.Type;
using SReflection = System.Reflection;
using KAssemblyLoader = AssemblyLoader;

namespace KSPe.Util
{
	public static class KSP
	{
		// Source : https://wiki.kerbalspaceprogram.com/wiki/Version_history
		// Unity: https://unity3d.com/get-unity/download/archive
		// Unity: https://answers.unity.com/questions/474716/unity-3d-releases-history-with-release-dates.html
		public static readonly Version[] PUBLISHED_VERSIONS = {
			new Version(0,0,0,  "2010-0401", 3,		-1, "<unk>", 30),	// Synthetic version, to prevent errors on FindByVersion. Using the date in which Monkey Squad hired HarvesteR
			new Version(0,7,3,  "2011-0624", 3,		-1, "<unk>", 30),	// Prior to 3.4.0, no info on https://unity3d.com/get-unity/download/archive
			new Version(0,8,0,  "2011-0711", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,8,1,  "2011-0713", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,8,2,  "2011-0713", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,8,3,  "2011-0714", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,8,4,  "2011-0714", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,8,5,  "2011-0718", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,9,0,  "2011-0812", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,10,0, "2011-0910", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,10,1, "2011-0913", 3,		-1, "<unk>", 30),	// 3.4.0?
			new Version(0,11,0, "2011-1012", 3,		-1, "<unk>", 30),	// 3.4.1?
			new Version(0,11,1, "2011-1013", 3,		-1, "<unk>", 30),	// 3.4.1?
			new Version(0,12,0, "2011-1111", 3,		-1, "<unk>", 30),	// 3.4.1?
			new Version(0,13,0, "2011-1216", 3,		-1, "<unk>", 30),	// 3.4.1? Can be 3.4.2 by the date.
			new Version(0,13,1, "2012-0111", 3,		4, "3.4.2", 30),	// Estimated using the release date and readme.txt contents
			new Version(0,13,2, "2012-0126", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,13,3, "2012-0303", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,14,0, "2012-0303", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,14,1, "2012-0327", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,14,2, "2012-0327", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,14,3, "2012-0403", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,14,4, "2012-0403", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,15,0, "2012-0517", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,15,1, "2012-0531", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,15,2, "2012-0601", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,16,0, "2012-0720", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,17,0, "2012-0919", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,17,1, "2012-1030", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,18,0, "2012-1201", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,18,1, "2012-1203", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,18,2, "2012-1220", 3,		4, "3.4.2", 30),	// Estimated
			new Version(0,18,3, "2013-0212", 4,		0, "4.0.1f2", 30),
			new Version(0,18,4, "2013-0214", 4,		-1, "<unk>", 30),	// 4.0.1?
			new Version(0,19,0, "2013-0316", 4,		-1, "<unk>", 30),	// 4.1.0?
			new Version(0,19,1, "2013-0318", 4,		-1, "<unk>", 30),	// 4.1.0?
			new Version(0,20,0, "2013-0521", 4,		-1, "<unk>", 30),	// 4.1.3?
			new Version(0,20,1, "2013-0529", 4,		-1, "<unk>", 30),	// 4.1.3?
			new Version(0,20,2, "2013-0530", 4,		-1, "<unk>", 30),	// 4.1.3?
			new Version(0,21,0, "2013-0724", 4,		-1, "<unk>", 30),	// 4.2.0?
			new Version(0,21,1, "2013-0724", 4,		-1, "<unk>", 30),	// 4.2.0?
			new Version(0,22,0, "2013-1016", 4,		3, "4.3.3f1", 30),	// C# used on compiling is 3.0.40818
			new Version(0,23,0, "2013-1217", 4,		3, "4.3.3f1", 30),
			new Version(0,23,5, "2014-0401", 4,		3, "4.3.3f1", 30),
			new Version(0,24,0, "2014-0717", 4,		-1, "<unk>", 30),	// 4.5.2?
			new Version(0,24,1, "2014-0724", 4,		-1, "<unk>", 30),	// 4.5.2?
			new Version(0,24,2, "2014-0725", 4,		5, "4.5.2f1", 30),	// C# used on compiling is 3.0.40818
			new Version(0,25,0, "2014-1007", 4,		5, "4.5.3p2", 30),	// C# used on compiling is 3.0.40818
			new Version(0,90,0, "2014-1215", 4,		5, "4.5.5f1", 30),	// C# used on compiling is 3.0.40818
			new Version(1,0,0,  "2015-0427", 4,		-1, "<unk>", 30),	// 4.6.4?
			new Version(1,0,1,  "2015-0501", 4,		-1, "<unk>", 30),	// 4.6.4?
			new Version(1,0,2,  "2015-0501", 4,		-1, "<unk>", 30),	// 4.6.4?
			new Version(1,0,3,  "2015-0622", 4,		-1, "<unk>", 30),	// 4.6.4?
			new Version(1,0,4,  "2015-0623", 4,		-1, "<unk>", 30),	// 4.6.4?
			new Version(1,0,5,  "2015-1109", 4,		6, "4.6.4f1", 30),	// C# used on compiling is 3.0.40818
			new Version(1,1,0,  "2016-0419", 5,		2, "5.2.4f1", 30),	// C# used on compiling is 3.0.40818
			new Version(1,1,1,  "2016-0429", 5,		2, "5.2.4f1", 30),
			new Version(1,1,2,  "2016-0430", 5,		2, "5.2.4f1", 30),	// Last release before HarvesteR (Felipe Falanghe) quits
			new Version(1,1,3,  "2016-0621", 5,		2, "5.2.4f1", 30),	// First release after HarvesteR (Felipe Falanghe) had leaved
			new Version(1,2,0,  "2016-1011", 5,		4, "5.4.0p4", 30),	// C# used on compiling is 3.0.40818
			new Version(1,2,1,  "2016-1101", 5,		4, "5.4.0p4", 30),
			new Version(1,2,2,  "2016-1206", 5, 	4, "5.4.0p4", 30),
			new Version(1,3,0,  "2017-0525", 5, 	4, "5.4.0p4", 30),	// First release after TTI acquired KSP.
			new Version(1,3,1,  "2017-1005", 5, 	4, "5.4.0p4", 30),	// Last release before KSP2 kick off?
			new Version(1,4,0,  "2018-0306", 2017,	1, "2017.1.3p1", 35),	// C# used on compiling is 3.5.21022
			new Version(1,4,1,  "2018-0313", 2017,	1, "2017.1.3p1", 35),
			new Version(1,4,2,  "2018-0328", 2017,	1, "2017.1.3p1", 35),
			new Version(1,4,3,  "2018-0427", 2017,	1, "2017.1.3p1", 35),
			new Version(1,4,4,  "2018-0621", 2017,	1, "2017.1.3p1", 35),
			new Version(1,4,5,  "2018-0626", 2017,	1, "2017.1.3p1", 35),
			new Version(1,5,0,  "2018-1015", 2017,	1, "2017.1.3p1", 35),
			new Version(1,5,1,  "2018-1017", 2017,	1, "2017.1.3p1", 35),
			new Version(1,6,0,  "2018-1219", 2017,	1, "2017.1.3p1", 35),
			new Version(1,6,1,  "2019-0109", 2017,	1, "2017.1.3p1", 35),
			new Version(1,7,0,  "2019-0410", 2017,	1, "2017.1.3p1", 35),
			new Version(1,7,1,  "2019-0430", 2017,	1, "2017.1.3p1", 35),
			new Version(1,7,2,  "2019-0612", 2017,	1, "2017.1.3p1", 35),
			new Version(1,7,3,  "2019-0711", 2017,	1, "2017.1.3p1", 35),
			new Version(1,8,0,  "2019-1016", 2019,	2, "2019.2.2f1", 46),	// C# used on compiling is 4.6.57
			new Version(1,8,1,  "2019-1029", 2019,	2, "2019.2.2f1", 46),
			new Version(1,9,0,  "2020-0212", 2019,	2, "2019.2.2f1", 46),
			new Version(1,9,1,  "2020-0227", 2019,	2, "2019.2.2f1", 46),
			new Version(1,10,0, "2020-0701", 2019,	2, "2019.2.2f1", 46),
			new Version(1,10,1, "2020-0728", 2019,	2, "2019.2.2f1", 46),
			new Version(1,11,0, "2020-1218", 2019,	2, "2019.2.2f1", 46),
			new Version(1,11,1, "2021-0128", 2019,	2, "2019.2.2f1", 46),
			new Version(1,11,2, "2021-0316", 2019,	2, "2019.2.2f1", 46),
			new Version(1,12,0, "2021-0624", 2019,	4, "2019.4.18f1", 46),
			new Version(1,12,1, "2021-0629", 2019,	4, "2019.4.18f1", 46),
			new Version(1,12,2, "2021-0803", 2019,	4, "2019.4.18f1", 46),
			new Version(1,12,3, "2021-1213", 2019,	4, "2019.4.18f1", 46),
			new Version(1,12,4, "2022-1102", 2019,	4, "2019.4.18f1", 46),
			new Version(1,12,5, "2023-0111", 2019,	4, "2019.4.18f1", 46),
		};

		public class Version
		{
			public readonly int MAJOR;
			public readonly int MINOR;
			public readonly int PATCH;

			public readonly DateTime RELEASED_AT;

			public readonly int UNITY_VERSION;
			public readonly int UNITY_VERSION_MINOR;
			public readonly string UNITY_VERSION_STRING;
			public readonly int CSHARP_VERSION;

			internal Version(int MAJOR, int MINOR, int PATCH)
			{
				this.MAJOR = MAJOR;
				this.MINOR = MINOR;
				this.PATCH = PATCH;
				this.RELEASED_AT = DateTime.Now;		// Fake a release date as being today
				this.UNITY_VERSION = 2019;				// Hope we are using 2019.2, as this is probably a newer KSP version still unknown.
				this.UNITY_VERSION_MINOR = 4;			// Hope we are using 2019.2, as this is probably a newer KSP version still unknown.
				this.UNITY_VERSION_STRING = "2019";		// Hope we are using 2019.2, as this is probably a newer KSP version still unknown.
				this.CSHARP_VERSION = 46;				// Ditto
			}

			internal Version(int MAJOR, int MINOR, int PATCH, Version v)
			{
				this.MAJOR = MAJOR;
				this.MINOR = MINOR;
				this.PATCH = PATCH;
				this.RELEASED_AT = v.RELEASED_AT.AddDays(1);	// Fake a release date as being one day later the reference
				this.UNITY_VERSION = v.UNITY_VERSION;
				this.UNITY_VERSION_MINOR = v.UNITY_VERSION_MINOR;
				this.UNITY_VERSION_STRING = v.UNITY_VERSION_STRING;
				this.CSHARP_VERSION = v.CSHARP_VERSION;
			}

			internal Version(int MAJOR, int MINOR, int PATCH, string releasedAt, int unityVersion, int unityVersionMinor, string unityVerString, int csharpVersion)
			{
				this.MAJOR = MAJOR;
				this.MINOR = MINOR;
				this.PATCH = PATCH;
				this.RELEASED_AT = DateTime.ParseExact(releasedAt, "yyyy-MMdd", CultureInfo.InvariantCulture);
				this.UNITY_VERSION = unityVersion;
				this.UNITY_VERSION_MINOR = unityVersionMinor;
				this.UNITY_VERSION_STRING = unityVerString;
				this.CSHARP_VERSION = csharpVersion;
			}

			public override string ToString()
			{
				return string.Format("{0}.{1}.{2}", this.MAJOR, this.MINOR, this.PATCH);
			}

			public string ToStringExtended()
			{
				return string.Format("{0}.{1}.{2} from {3} using Unity {4} and C# {5}", this.MAJOR, this.MINOR, this.PATCH, this.RELEASED_AT.ToString(), this.UNITY_VERSION, this.CSHARP_VERSION);
			}

			public override int GetHashCode()
			{
				int hash = 7;
				hash = 31 * hash + this.MAJOR;
				hash = 31 * hash + this.MINOR;
				hash = 31 * hash + this.PATCH;
				return hash;
			}

			public override bool Equals(object other)
			{
				bool r = base.Equals(other);
				if (other is Version)
				{
					Version o = other as Version;
					r = r || (this.MAJOR == o.MAJOR && this.MINOR == o.MINOR && this.PATCH == o.PATCH);
				}
				return r;
			}

			public bool Equals(int major, int minor, int patch)
			{
				return this.MAJOR == major && this.MINOR == minor && this.PATCH == patch;
			}

			public bool Equals(int major, int minor)
			{
				return this.MAJOR == major && this.MINOR == minor;
			}

			public bool Equals(int major)
			{
				return this.MAJOR == major;
			}

			public static bool operator <(Version one, Version other)
			{
				bool r =  (one.MAJOR < other.MAJOR);
				r = r || (one.MAJOR == other.MAJOR && one.MINOR < other.MINOR);
				r = r || (one.MAJOR == other.MAJOR && one.MINOR == other.MINOR && one.PATCH < other.PATCH);
				return r;
			}

			public static bool operator >(Version one, Version other)
			{
				bool r = (one.MAJOR > other.MAJOR);
				r = r || (one.MAJOR == other.MAJOR && one.MINOR > other.MINOR);
				r = r || (one.MAJOR == other.MAJOR && one.MINOR == other.MINOR && one.PATCH > other.PATCH);
				return r;
			}

			public static bool operator <=(Version one, Version other)
			{
				bool r = (one == other);
				r = r || (one < other);
				return r;
			}

			public static bool operator >=(Version one, Version other)
			{
				bool r = (one == other);
				r = r || (one > other);
				return r;
			}

			internal static List<Version> FindByUnity(int desiredUnityVersion)
			{
				List<Version> r = new List<Version>();
				foreach (Version v in PUBLISHED_VERSIONS)
					if (v.UNITY_VERSION == desiredUnityVersion)
						r.Add(v);
				return r;
			}

			/// <summary>
			/// </summary>
			/// <param name="major"></param>
			/// <param name="minor"></param>
			/// <param name="patch"></param>
			/// <returns>An existent KSP version, or ArgumentOutOfRangeException</returns>
			public static Version GetVersion(int major, int minor, int patch)
			{
				foreach (Version v in PUBLISHED_VERSIONS)
					if (v.MAJOR == major && v.MINOR == minor && v.PATCH == patch)
						return v;
				throw new ArgumentOutOfRangeException(String.Format("{0}.{1}.{2}", major, minor, patch));
			}

			/// <summary>
			/// </summary>
			/// <param name="major"></param>
			/// <param name="minor"></param>
			/// <param name="patch"></param>
			/// <returns>A existent KSP version, or a reasonably synthetic one using the provided information</returns>
			public static Version FindByVersion(int major, int minor, int patch)
			{
				try
				{
					return GetVersion(major, minor, patch);
				}
				catch (ArgumentOutOfRangeException)
				{
					Version r = null;
					for (int i = 0; i < PUBLISHED_VERSIONS.Length; ++i)
					{
						Version v = PUBLISHED_VERSIONS[i];
						if (v.MAJOR < major) continue;
						if (v.MAJOR == major && v.MINOR < minor) continue;
						if (v.MAJOR == major && v.MINOR == minor && v.PATCH < patch) continue;
						r = new Version(major, minor, patch, PUBLISHED_VERSIONS[i-1]);
					}
					r = r ?? new Version(major, minor, patch);
					KSPe.Log.warn("KSP Version {0}.{1}.{2} not localized. Returning synthetic one.", major, minor, patch);
					return r;
				}
			}

			private static Version CURRENT = null;
			public static Version Current { 
				get {
					if (null == CURRENT)
						try
						{
							CURRENT = GetVersion(Versioning.version_major, Versioning.version_minor, Versioning.Revision);
						}
						catch (ArgumentOutOfRangeException)
						{
							CURRENT = new Version(Versioning.version_major, Versioning.version_minor, Versioning.Revision);
						}
					return CURRENT;
				}
			}
		}
	}

	// Since I don't now for sure if ConfigNode.CreateCopy is deep or shallow, I decided to reimplement the thing
	// and gain control about it.
	public static class ConfigNode
	{
		/// <summary>
		/// Creates a Deep Copy of the ConfigNode `source`.
		/// Everything is duplicated from the root to the leaves.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static global::ConfigNode CreateDeepCopy(global::ConfigNode source)
		{
			global::ConfigNode r = new global::ConfigNode
			{
				id = source.id,
				name = source.name,
				comment = source.comment
			};
			{
				for (int i = 0; i < source.values.Count; ++i)
				{
					global::ConfigNode.Value ov = source.values[i];
					global::ConfigNode.Value nv = new global::ConfigNode.Value(ov.name, ov.value, ov.comment);
					r.values.Add(nv);
				}

				for (int i = 0; i < source.nodes.Count; ++i)
				{
					global::ConfigNode o = source.nodes[i];
					global::ConfigNode n = CreateDeepCopy(o);
					r.nodes.Add(n);
				}
			}
			return r;
		}

		/// <summary>
		/// Creates a Shallow Copy of the ConfigNode `source`.
		/// Only the values are duplicated, any nodes are just relinked into the new copy.
		/// Any changes to values will preserve the origin from changes, but any change on the subnodes will affect the originals.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static global::ConfigNode CreateShallowCopy(global::ConfigNode source)
		{
			global::ConfigNode r = new global::ConfigNode
			{
				id = source.id,
				name = source.name,
				comment = source.comment
			};
			{
				for (int i = 0; i < source.values.Count; ++i)
				{
					global::ConfigNode.Value ov = source.values[i];
					global::ConfigNode.Value nv = new global::ConfigNode.Value(ov.name, ov.value, ov.comment);
					r.values.Add(nv);
				}

				for (int i = 0; i < source.nodes.Count; ++i)
				{
					global::ConfigNode o = source.nodes[i];
					r.nodes.Add(o);
				}
			}
			return r;
		}

		/// <summary>
		/// Creates a Flat Copy for the ConfigNode `Source`
		/// Everything is relinked into the new ConfigNode, nothing is duplicated.
		/// Any changes on the current contents will affect the originals, but additional values or nodes will not.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static global::ConfigNode CreateFlatCopy(global::ConfigNode source)
		{
			global::ConfigNode r = new global::ConfigNode
			{
				id = source.id,
				name = source.name,
				comment = source.comment
			};
			{
				for (int i = 0; i < source.values.Count; ++i)
				{
					global::ConfigNode.Value ov = source.values[i];
					r.values.Add(ov);
				}

				for (int i = 0; i < source.nodes.Count; ++i)
				{
					global::ConfigNode o = source.nodes[i];
					r.nodes.Add(o);
				}
			}
			return r;
		}
	}

	public static class AssemblyLoader
	{
		public static class Finder
		{
			private static readonly Dictionary<string, KAssemblyLoader.LoadedAssembly> ASSEMBLIES = new Dictionary<string, KAssemblyLoader.LoadedAssembly>();
			public static bool ExistsByName(string qn)
			{
				lock (ASSEMBLIES)
				{
					if (ASSEMBLIES.ContainsKey(qn)) return true;
					foreach (KAssemblyLoader.LoadedAssembly assembly in KAssemblyLoader.loadedAssemblies) if (assembly.assembly.GetName().Name.Equals(qn))
					{
						ASSEMBLIES.Add(qn, assembly);
						return true;
					}
				}
				return false;
			}
			public static KAssemblyLoader.LoadedAssembly FindByName(string qn)
			{
				lock(ASSEMBLIES)
				{ 
					if (ASSEMBLIES.ContainsKey(qn)) return ASSEMBLIES[qn];
					foreach (KAssemblyLoader.LoadedAssembly assembly in KAssemblyLoader.loadedAssemblies) if (assembly.assembly.GetName().Name.Equals(qn))
					{
						ASSEMBLIES.Add(qn, assembly);
						return assembly;
					}
				}
				throw new DllNotFoundException("An Add'On Support DLL was not loaded. Missing LoadedAssembly : " + qn);
			}
		}

		public static class Search
		{
			public static IEnumerable<KAssemblyLoader.LoadedAssembly> ByName(string name)
			{
				foreach (KAssemblyLoader.LoadedAssembly assembly in KAssemblyLoader.loadedAssemblies)
					if (assembly.assembly.GetName().Name.Equals(name))
						yield return assembly;
			}
		}


		private static readonly System.Uri BASEURI = new System.Uri(IO.Path.AppRoot());
		/* I CAN'T MAKE THIS THING TO WORK! */
		internal static KAssemblyLoader.LoadedAssembly LoadPlugin(string searchPath, string asmName, string asmFile)
		{
			string asmPath = SIO.Path.Combine(searchPath, asmFile);
			Uri uri = new Uri(BASEURI, asmPath);
			SIO.FileInfo fi = new SIO.FileInfo(uri.AbsolutePath);
			//LOG.debug("fi {0} -- url {1}", fi.FullName, uri.AbsoluteUri);
			global::ConfigNode cn = new global::ConfigNode();
			if (!KAssemblyLoader.LoadPlugin(fi, uri.AbsoluteUri, cn)) // If the return value of this thing working as expected?
				throw new DllNotFoundException(string.Format("Could not load {0} from {1}!", asmName, asmFile));
			foreach (KAssemblyLoader.LoadedAssembly a in KAssemblyLoader.loadedAssemblies) if (a.name.Equals(asmName))
			{
				a.Load();
				return a;
			}
			throw new DllNotFoundException(string.Format("Could not load {0} from {1}!", asmName, asmFile));
		}

		internal class Loader : SystemTools.Assembly.Loader
		{
			protected Loader() : base() { }
			public Loader(params string[] subdirs) : base(subdirs) { }

			public KAssemblyLoader.LoadedAssembly LoadAndStartup(string asmName, string asmFile)
			{
				return LoadPlugin(this.searchPath, asmName, asmFile);
			}
		}

		public class Loader<T> : SystemTools.Assembly.Loader<T>
		{
			private readonly SType type;
			public Loader(params string[] subdirs) : base(subdirs) { }

			public KAssemblyLoader.LoadedAssembly LoadAndStartup(string asmName, string asmFile)
			{
				return LoadPlugin(this.searchPath, asmName, asmFile);
			}
		}
	}
}
