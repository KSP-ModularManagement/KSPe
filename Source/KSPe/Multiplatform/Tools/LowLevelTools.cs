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
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.Principal;

namespace KSPe.Multiplatform.LowLevelTools {

	public static class Unix
	{
		// Reference: https://github.com/mono/mono/blob/main/mcs/class/System.Core/System/Util.cs
		public static bool IsThisUnix => 4 == (int)System.Environment.OSVersion.Platform
										|| 128 == (int)System.Environment.OSVersion.Platform
										|| 6 == (int)System.Environment.OSVersion.Platform
									;
		// Reference: https://github.com/mono/mono/blob/5d2e3bc3b3c8184d35b2f7801e88d96470d367c4/mcs/class/corlib/Test/System.Security.AccessControl/MutexSecurityTest.cs
		public static bool IsThisMacOS => PlatformID.MacOSX == System.Environment.OSVersion.Platform;
    }

	public static class Windows
	{
		// Reference https://github.com/mono/mono/blob/main/mcs/class/referencesource/mscorlib/system/platformid.cs
		// and https://github.com/mono/mono/blob/main/mcs/class/corlib/System/Environment.cs#L234
		public static bool IsThisWindows => ((int)System.Environment.OSVersion.Platform < 4);
	}

	// Source : https://stackoverflow.com/a/33487494
	public static class Windows32	
	{
		private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		private const uint FILE_READ_EA = 0x0008;
		private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x2000000;

		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern uint GetFinalPathNameByHandle(
			IntPtr hFile,
			[MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszFilePath,
			uint cchFilePath,
			uint dwFlags);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool CloseHandle(IntPtr hObject);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CreateFile(
				[MarshalAs(UnmanagedType.LPTStr)] string filename,
				[MarshalAs(UnmanagedType.U4)] uint access,
				[MarshalAs(UnmanagedType.U4)] FileShare share,
				IntPtr securityAttributes, // optional SECURITY_ATTRIBUTES struct or IntPtr.Zero
				[MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
				[MarshalAs(UnmanagedType.U4)] uint flagsAndAttributes,
				IntPtr templateFile);

		public static string GetFinalPathName(string path)
		{
			IntPtr h = CreateFile(path,
				FILE_READ_EA,
				FileShare.ReadWrite | FileShare.Delete,
				IntPtr.Zero,
				FileMode.Open,
				FILE_FLAG_BACKUP_SEMANTICS,
				IntPtr.Zero);

			if (h == INVALID_HANDLE_VALUE)
			{
				int error = Marshal.GetLastWin32Error();
				throw new Win32Exception(string.Format("Got a invalid handle while CreateFile for {0} with errno {1} - '{2}' !!", path, error, new Win32Exception(error).Message));
			}

			try
			{
				StringBuilder sb = new StringBuilder(1023);
				uint res = GetFinalPathNameByHandle(h, sb, (uint)(1+sb.Capacity), 0); // size of the StringBuilder buffer plus the null terminating zero.
				if (res == 0)
				{ 
					int error = Marshal.GetLastWin32Error();
					throw new Win32Exception(string.Format("Got a 0 == res while GetFinalPathNameByHandle for {0} with errno {1} - '{2}' !!", path, error, new Win32Exception(error).Message));
				}

				return sb.ToString();
			}
			finally
			{
				CloseHandle(h);
			}
		}
	}

	public static class Security
	{
		// Source : https://stackoverflow.com/questions/5953240/check-for-administrator-privileges-in-c-sharp
		public static bool isElevated
		{
			get
			{
				if (Windows.IsThisWindows) return false;

				using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
				{
					WindowsPrincipal principal = new WindowsPrincipal(identity);
					return principal.IsInRole(WindowsBuiltInRole.Administrator);
				}
			}
		}
	}
}
