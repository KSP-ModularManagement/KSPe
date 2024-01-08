/*
	This file is part of KSPe, a component for KSP Enhanced /L
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
namespace KSPe
{
	// TODO: Remove on Version 2.6
	[System.Obsolete("KSPe.KspConfig is deprecated, please use KSPe.IO.KspConfigNode instead.")]
	public class KspConfig : AbstractConfig
	{
		public KspConfig(string name) : base(name)
		{
			this.Path = GeneratePathname(name + ".cfg");
		}

		public KspConfig(string name, string filename) : base(name)
		{
			this.Path = GeneratePathname(filename);
		}

		public new KspConfig Load()
		{
			return (KspConfig)base.Load();
		}
		
		protected static string GeneratePathname(string filename)
		{
			string fn = KSPe.IO.Hierarchy.ROOT.Solve(filename);
			return fn;
		}

	}
}
