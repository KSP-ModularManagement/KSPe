/*
	This file is part of KSPe.UI, a component for KSP Enhanced /L
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
using System;
using ST = KSPe.Util.SystemTools;

namespace KSPe.Common.Dialogs
{
	public class ExceptionHandler
	{
		public static void Show<T>(ST.Assembly.Loader.AlreadyLoadedException ex)
		{
			ex.offendedName = ex.offendedName ?? ST.Reflection.Version<T>.FriendlyName;
			ErrorHandling.AlreadyLoadedAlertBox.Show(ex);
		}

		public static void Show<T>(ST.Assembly.Exception ex)
		{
			ex.offendedName = ex.offendedName ?? ST.Reflection.Version<T>.FriendlyName;
			ErrorHandling.MissingDependencyErrorBox.Show(ex);
		}

		public static void Show<T>(Util.Compatibility.Exception ex)
		{
			ex.offendedName = ex.offendedName ?? ST.Reflection.Version<T>.FriendlyName;
			ErrorHandling.FaultyCompatibilityAdviceBox.Show(ex);
		}

		public static void Show<T>(Util.Installation.Exception ex)
		{
			ex.offendedName = ex.offendedName ?? ST.Reflection.Version<T>.FriendlyName;
			ErrorHandling.FaultyInstallationErrorBox.Show(ex);
		}

		public static void Show<T>(Util.AbstractException ex)
		{
			ex.offendedName = ex.offendedName ?? ST.Reflection.Version<T>.FriendlyName;
			ErrorHandling.UnhandledAlertBox.Show(ex);
		}

		public static void Show<T>(System.DllNotFoundException ex)
		{
			string offendedName = ST.Reflection.Version<T>.FriendlyName;
			ErrorHandling.MissingDLLErrorBox.Show(ex, offendedName);
		}

		public static void Show<T>(System.Exception ex)
		{
			if		(ex is ST.Assembly.Loader.AlreadyLoadedException)	Show<T>((ST.Assembly.Loader.AlreadyLoadedException) ex);
			else if	(ex is ST.Assembly.Exception)						Show<T>((ST.Assembly.Exception) ex);
			else if	(ex is ST.Exception)								Show<T>((ST.Exception) ex);
			else if	(ex is Util.Compatibility.Exception)				Show<T>((Util.Compatibility.Exception) ex);
			else if	(ex is Util.Installation.Exception)					Show<T>((Util.Installation.Exception) ex);
			else if	(ex is Util.AbstractException)						Show<T>((Util.AbstractException) ex);
			else if	(ex is System.DllNotFoundException)					Show<T>((System.DllNotFoundException) ex);
			else
			{
				string offendedName = ST.Reflection.Version<T>.FriendlyName;
				ErrorHandling.UnhandledAlertBox.Show(ex, offendedName);
			}
		}
	}
}