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
			new Version(0,7,3,  "2011-0624", 4),
			new Version(0,8,0,  "2011-0711", 4),
			new Version(0,8,1,  "2011-0713", 4),
			new Version(0,8,2,  "2011-0713", 4),
			new Version(0,8,3,  "2011-0714", 4),
			new Version(0,8,4,  "2011-0714", 4),
			new Version(0,8,5,  "2011-0718", 4),
			new Version(0,9,0,  "2011-0812", 4),
			new Version(0,10,0, "2011-0910", 4),
			new Version(0,10,1, "2011-0913", 4),
			new Version(0,11,0, "2011-1012", 4),
			new Version(0,11,1, "2011-1013", 4),
			new Version(0,12,0, "2011-1111", 4),
			new Version(0,13,0, "2011-1216", 4),
			new Version(0,13,1, "2012-0111", 4),
			new Version(0,13,2, "2012-0126", 4),
			new Version(0,13,3, "2012-0303", 4),
			new Version(0,14,0, "2012-0303", 4),
			new Version(0,14,1, "2012-0327", 4),
			new Version(0,14,2, "2012-0327", 4),
			new Version(0,14,3, "2012-0403", 4),
			new Version(0,14,4, "2012-0403", 4),
			new Version(0,15,0, "2012-0517", 4),
			new Version(0,15,1, "2012-0531", 4),
			new Version(0,15,2, "2012-0601", 4),
			new Version(0,16,0, "2012-0720", 4),
			new Version(0,17,0, "2012-0919", 4),
			new Version(0,17,1, "2012-1030", 4),
			new Version(0,18,0, "2012-1201", 4),
			new Version(0,18,1, "2012-1203", 4),
			new Version(0,18,2, "2012-1220", 4),
			new Version(0,18,3, "2013-0212", 4),
			new Version(0,18,4, "2013-0214", 4),
			new Version(0,19,0, "2013-0316", 4),
			new Version(0,19,1, "2013-0318", 4),
			new Version(0,20,0, "2013-0521", 4),
			new Version(0,20,1, "2013-0529", 4),
			new Version(0,20,2, "2013-0530", 4),
			new Version(0,21,0, "2013-0724", 4),
			new Version(0,21,1, "2013-0724", 4),
			new Version(0,22,0, "2013-1016", 4),
			new Version(0,23,0, "2013-1217", 4),
			new Version(0,23,5, "2014-0401", 4),
			new Version(0,24,0, "2014-0717", 4),
			new Version(0,24,1, "2014-0724", 4),
			new Version(0,24,2, "2014-0725", 4),
			new Version(0,25,0, "2014-1007", 4),
			new Version(0,90,0, "2014-1215", 4),
			new Version(1,0,0,  "2015-0427", 4),
			new Version(1,0,1,  "2015-0501", 4),
			new Version(1,0,2,  "2015-0501", 4),
			new Version(1,0,3,  "2015-0622", 4),
			new Version(1,0,4,  "2015-0623", 4),
			new Version(1,0,5,  "2015-1109", 4),
			new Version(1,1,0,  "2016-0419", 5),
			new Version(1,1,1,  "2016-0429", 5),
			new Version(1,1,2,  "2016-0430", 5),
			new Version(1,1,3,  "2016-0621", 5),
			new Version(1,2,0,  "2016-1011", 5),
			new Version(1,2,1,  "2016-1101", 5),
			new Version(1,2,2,  "2016-1206", 5),
			new Version(1,3,0,  "2017-0525", 5),	// First release after TTI acquired KSP.
			new Version(1,3,1,  "2017-1005", 5),	// Last release before KSP2 kick off?
			new Version(1,4,0,  "2018-0306", 2017),
			new Version(1,4,1,  "2018-0313", 2017),
			new Version(1,4,2,  "2018-0328", 2017),
			new Version(1,4,3,  "2018-0427", 2017),
			new Version(1,4,4,  "2018-0621", 2017),
			new Version(1,4,4,  "2018-0626", 2017),
			new Version(1,5,0,  "2018-1015", 2017),
			new Version(1,5,1,  "2018-1017", 2017),
			new Version(1,6,0,  "2018-1219", 2017),
			new Version(1,6,1,  "2019-0109", 2017),
			new Version(1,7,1,  "2019-0430", 2017),
			new Version(1,7,2,  "2019-0612", 2017),
			new Version(1,7,3,  "2019-0711", 2017),
			new Version(1,8,0,  "2019-1016", 2019),
			new Version(1,8,1,  "2019-1029", 2019),
		};

		public class Version
		{
			public readonly int MAJOR;
			public readonly int MINOR;
			public readonly int PATCH;

			public readonly DateTime RELEASED_AT;

			public readonly int UNITY_VERSION;

			internal Version(int MAJOR, int MINOR, int PATCH, string releasedAt, int unityVersion)
			{
				this.MAJOR = MAJOR;
				this.MINOR = MINOR;
				this.PATCH = PATCH;
				this.RELEASED_AT = DateTime.ParseExact(releasedAt, "yyyy-MMdd", CultureInfo.InvariantCulture);
				this.UNITY_VERSION = unityVersion;
			}

			public override string ToString()
			{
				return string.Format("{0}.{1}.{2}", this.MAJOR, this.MINOR, this.PATCH);
			}

			internal static List<Version> FindByUnity(int desiredUnityVersion)
			{
				List<Version> r = new List<Version>();
				foreach (Version v in PUBLISHED_VERSIONS)
					if (v.UNITY_VERSION == desiredUnityVersion)
						r.Add(v);
				return r;
			}
		}
	}
}
