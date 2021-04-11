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

namespace KSPe
{
	internal static class SanityChecks
	{
		internal static void DoIt()
		{
			CheckForPwd();
		}

		private static void CheckForPwd()
		{
			string pwd = KSPe.IO.Path.EnsureTrailingSeparatorOnDir(System.IO.Directory.GetCurrentDirectory(), true);
			string origin = KSPe.IO.Path.Origin();

			// Naivelly comparing the paths is borking on Windows, as this thingy uses case insensity pathnames by default.
			// if (!pwd.Equals(origin)) FatalErrors.PwdIsNotOrigin.Show(pwd, origin);

			// So we need a mechanism that would solve this on Windows without breaking Linux and MacOS, ideally without creating
			// system specific support code. The solution for this is using URIs.
			// Let the runtime do the dirty work for us.
			{
				Uri uri_pwd = new Uri(System.IO.Path.Combine(pwd, "dummy.txt"));
				Uri uri_origin = new Uri(System.IO.Path.Combine(origin, "dummy.txt"));
				if (uri_pwd != uri_origin) FatalErrors.PwdIsNotOrigin.Show(pwd, origin);
			}
		}
	}
}
