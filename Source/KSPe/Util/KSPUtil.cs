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
using SIO = System.IO;
using SIR = System.Reflection;
namespace KSPe
{
	/***
	 * Brute force replacement for global::KSPUtil from Squad, allowing it to work outside the UnityEngine (sometimes arbitrary
	 * and useless) limitations.
	 * 
	 * Currently `internal`, as I'm unsure if I really should mangle so deep in the stack and bring over me so much responsability. KSPe.IO
	 * already have more than adequated ways to get the same information, so I need a strong reason that `ApplicationRootPath` to go
	 * down this rabbit role.
	 */
	internal static class KSPUtil
	{
		private static string __ApplicationRootPath = null;
		public static string ApplicationRootPath => __ApplicationRootPath ?? (__ApplicationRootPath = calculateApplicationRootPath());

		private static string calculateApplicationRootPath()
		{
			string r;
			try
			{
				r = global::KSPUtil.ApplicationRootPath;

				// Playing safe
				if (string.IsNullOrEmpty(r)) throw new System.NullReferenceException("global::KSPUtil.ApplicationRootPath returned an empty string!");
			}
			catch (System.Exception e)
			{
#if DEBUG
				UnityEngine.Debug.LogWarningFormat("Coldn't rely on KSPUtil.ApplicationRootPath due [[{0}]]. Going brute force.", e.Message);
#endif
				r = SIO.Path.GetDirectoryName(System.Environment.GetCommandLineArgs()[0]);
				if (r.Contains("KSP.app"))
					r = r.Substring(0, r.IndexOf("KSP.app"));
			}
#if DEBUG
			UnityEngine.Debug.LogFormat("Calculated ApplicationRootPath {0}.", r);
#endif
			return r;
		}
	}
}
