/*
   This file is part of KSPe, a component for KSP API Extensions/L
   © 2018-21 Lisias T : http://lisias.net <support@lisias.net>

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
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace KSPe.Multiplatform.LowLevelTools {

	public static class Windows
	{
		public static bool IsThisWindows => ((int)System.Environment.OSVersion.Platform < 4);
	}

	// Source : https://stackoverflow.com/a/33487494
	public static class Windows32	
	{
		private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

		private const uint FILE_READ_EA = 0x0008;
		private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x2000000;

		[DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern uint GetFinalPathNameByHandle(IntPtr hFile, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszFilePath, uint cchFilePath, uint dwFlags);

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
				throw new Win32Exception();

			try
			{
				StringBuilder sb = new StringBuilder(1024);
				uint res = GetFinalPathNameByHandle(h, sb, 1024, 0);
				if (res == 0)
					throw new Win32Exception();

				return sb.ToString();
			}
			finally
			{
				CloseHandle(h);
			}
		}
	}
}
