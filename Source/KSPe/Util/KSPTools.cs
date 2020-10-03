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
using System.Globalization;

namespace KSPe.Util
{
	public static class KSP
	{
		// Source : https://wiki.kerbalspaceprogram.com/wiki/Version_history
		public static readonly Version[] PUBLISHED_VERSIONS = {
			new Version(0,7,3,  "2011-0624", 4, 35),
			new Version(0,8,0,  "2011-0711", 4, 35),
			new Version(0,8,1,  "2011-0713", 4, 35),
			new Version(0,8,2,  "2011-0713", 4, 35),
			new Version(0,8,3,  "2011-0714", 4, 35),
			new Version(0,8,4,  "2011-0714", 4, 35),
			new Version(0,8,5,  "2011-0718", 4, 35),
			new Version(0,9,0,  "2011-0812", 4, 35),
			new Version(0,10,0, "2011-0910", 4, 35),
			new Version(0,10,1, "2011-0913", 4, 35),
			new Version(0,11,0, "2011-1012", 4, 35),
			new Version(0,11,1, "2011-1013", 4, 35),
			new Version(0,12,0, "2011-1111", 4, 35),
			new Version(0,13,0, "2011-1216", 4, 35),
			new Version(0,13,1, "2012-0111", 4, 35),
			new Version(0,13,2, "2012-0126", 4, 35),
			new Version(0,13,3, "2012-0303", 4, 35),
			new Version(0,14,0, "2012-0303", 4, 35),
			new Version(0,14,1, "2012-0327", 4, 35),
			new Version(0,14,2, "2012-0327", 4, 35),
			new Version(0,14,3, "2012-0403", 4, 35),
			new Version(0,14,4, "2012-0403", 4, 35),
			new Version(0,15,0, "2012-0517", 4, 35),
			new Version(0,15,1, "2012-0531", 4, 35),
			new Version(0,15,2, "2012-0601", 4, 35),
			new Version(0,16,0, "2012-0720", 4, 35),
			new Version(0,17,0, "2012-0919", 4, 35),
			new Version(0,17,1, "2012-1030", 4, 35),
			new Version(0,18,0, "2012-1201", 4, 35),
			new Version(0,18,1, "2012-1203", 4, 35),
			new Version(0,18,2, "2012-1220", 4, 35),
			new Version(0,18,3, "2013-0212", 4, 35),
			new Version(0,18,4, "2013-0214", 4, 35),
			new Version(0,19,0, "2013-0316", 4, 35),
			new Version(0,19,1, "2013-0318", 4, 35),
			new Version(0,20,0, "2013-0521", 4, 35),
			new Version(0,20,1, "2013-0529", 4, 35),
			new Version(0,20,2, "2013-0530", 4, 35),
			new Version(0,21,0, "2013-0724", 4, 35),
			new Version(0,21,1, "2013-0724", 4, 35),
			new Version(0,22,0, "2013-1016", 4, 35),
			new Version(0,23,0, "2013-1217", 4, 35),
			new Version(0,23,5, "2014-0401", 4, 35),
			new Version(0,24,0, "2014-0717", 4, 35),
			new Version(0,24,1, "2014-0724", 4, 35),
			new Version(0,24,2, "2014-0725", 4, 35),
			new Version(0,25,0, "2014-1007", 4, 35),
			new Version(0,90,0, "2014-1215", 4, 35),
			new Version(1,0,0,  "2015-0427", 4, 35),
			new Version(1,0,1,  "2015-0501", 4, 35),
			new Version(1,0,2,  "2015-0501", 4, 35),
			new Version(1,0,3,  "2015-0622", 4, 35),
			new Version(1,0,4,  "2015-0623", 4, 35),
			new Version(1,0,5,  "2015-1109", 4, 35),
			new Version(1,1,0,  "2016-0419", 5, 35),
			new Version(1,1,1,  "2016-0429", 5, 35),
			new Version(1,1,2,  "2016-0430", 5, 35),	// Last release before HarvesteR (Felipe Falanghe) quits
			new Version(1,1,3,  "2016-0621", 5, 35),	// First release after HarvesteR (Felipe Falanghe) had leaved
			new Version(1,2,0,  "2016-1011", 5, 35),
			new Version(1,2,1,  "2016-1101", 5, 35),
			new Version(1,2,2,  "2016-1206", 5, 35),
			new Version(1,3,0,  "2017-0525", 5, 35),	// First release after TTI acquired KSP.
			new Version(1,3,1,  "2017-1005", 5, 35),	// Last release before KSP2 kick off?
			new Version(1,4,0,  "2018-0306", 2017, 35),
			new Version(1,4,1,  "2018-0313", 2017, 35),
			new Version(1,4,2,  "2018-0328", 2017, 35),
			new Version(1,4,3,  "2018-0427", 2017, 35),
			new Version(1,4,4,  "2018-0621", 2017, 35),
			new Version(1,4,5,  "2018-0626", 2017, 35),
			new Version(1,5,0,  "2018-1015", 2017, 35),
			new Version(1,5,1,  "2018-1017", 2017, 35),
			new Version(1,6,0,  "2018-1219", 2017, 35),
			new Version(1,6,1,  "2019-0109", 2017, 35),
			new Version(1,7,0,  "2019-0410", 2017, 35),
			new Version(1,7,1,  "2019-0430", 2017, 35),
			new Version(1,7,2,  "2019-0612", 2017, 35),
			new Version(1,7,3,  "2019-0711", 2017, 35),
			new Version(1,8,0,  "2019-1016", 2019, 47),
			new Version(1,8,1,  "2019-1029", 2019, 47),
			new Version(1,9,0,  "2020-0212", 2019, 47),
			new Version(1,9,1,  "2020-0227", 2019, 47),
			new Version(1,10,0, "2020-0701", 2019, 47),
			new Version(1,10,1, "2020-0728", 2019, 47),
		};

		public class Version
		{
			public readonly int MAJOR;
			public readonly int MINOR;
			public readonly int PATCH;

			public readonly DateTime RELEASED_AT;

			public readonly int UNITY_VERSION;
			public readonly int CSHARP_VERSION;

			internal Version(int MAJOR, int MINOR, int PATCH)
			{
				this.MAJOR = MAJOR;
				this.MINOR = MINOR;
				this.PATCH = PATCH;
				this.RELEASED_AT = DateTime.Now;	// Fake a release date as being today
				this.UNITY_VERSION = 2019;			// Hope we are using 2019, as this is probably a newer KSP version still unknown.
				this.CSHARP_VERSION = 47;			// Ditto
			}

			internal Version(int MAJOR, int MINOR, int PATCH, string releasedAt, int unityVersion, int csharpVersion)
			{
				this.MAJOR = MAJOR;
				this.MINOR = MINOR;
				this.PATCH = PATCH;
				this.RELEASED_AT = DateTime.ParseExact(releasedAt, "yyyy-MMdd", CultureInfo.InvariantCulture);
				this.UNITY_VERSION = unityVersion;
				this.CSHARP_VERSION = csharpVersion;
			}

			public override string ToString()
			{
				return string.Format("{0}.{1}.{2}", this.MAJOR, this.MINOR, this.PATCH);
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

			public static Version FindByVersion(int major, int minor, int patch)
			{
				foreach (Version v in PUBLISHED_VERSIONS)
					if (v.MAJOR == major && v.MINOR == minor && v.PATCH == patch)
						return v;
				throw new ArgumentOutOfRangeException(String.Format("{0}.{1}.{2}", major, minor, patch));
			}

			private static Version CURRENT = null;
			public static Version Current { 
				get {
					if (null == CURRENT)
						try
						{
							CURRENT = FindByVersion(Versioning.version_major, Versioning.version_minor, Versioning.Revision);
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
}
