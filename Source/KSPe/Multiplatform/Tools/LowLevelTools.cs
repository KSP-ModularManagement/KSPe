/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2026 LisiasT : http://lisias.net <support@lisias.net>

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
		public static bool IsThisUnix => 4 == (int)System.Environment.OSVersion.Platform	// MacOS is triggering this value, damn!
										|| 128 == (int)System.Environment.OSVersion.Platform
										|| 6 == (int)System.Environment.OSVersion.Platform	// It's not working, it should had detected MacOS :(
									;
		public static bool IsThisLinux => Linux.IsThisLinux;

		// Reference: https://github.com/mono/mono/blob/5d2e3bc3b3c8184d35b2f7801e88d96470d367c4/mcs/class/corlib/Test/System.Security.AccessControl/MutexSecurityTest.cs
		public static bool IsThisMacOS =>
											PlatformID.MacOSX == System.Environment.OSVersion.Platform
										||
											MacOS.IsThisMacOS
									;
	}

	public static class Windows
	{
		// Reference https://github.com/mono/mono/blob/main/mcs/class/referencesource/mscorlib/system/platformid.cs
		// and https://github.com/mono/mono/blob/main/mcs/class/corlib/System/Environment.cs#L234
		public static bool IsThisWindows => ((int)System.Environment.OSVersion.Platform < 4);
	}

	public static class MacOS
	{
		private const string UNAME = "/usr/bin/uname";
		private const string SW_VARS = "/usr/bin/sw_vers";
		private static bool? isThisMacOS = null;
		private static string version = null;

		public static bool IsThisMacOS { get
		{
			if (null != isThisMacOS) return (bool)isThisMacOS;

			bool r = Unix.IsThisUnix;

			r &= File.Exists(UNAME);
			if (r)
			{
				string data = Shell.command(UNAME, "").Replace("\n", "");
				r &= "Darwin".Equals(data);
			}

			r &= File.Exists(SW_VARS);
			if (r)
			{
				string data = Shell.command(SW_VARS, "");
				foreach (string entry in data.Split('\n')) if (entry.Contains(":"))
				{
					string[] kv = entry.Split(':');
					if ("ProductVersion".Equals(kv[0]))
					{
						version = kv[1].Trim();
						break;
					}
				}
			}

			return (bool)(isThisMacOS = r);
		} }

		public static string Version {  get
		{
			if (null != version) return version;
			bool r = IsThisMacOS;
			return r ? version : "N/A";
		} }
	}

	public static class Linux
	{
		private const string RELEASE_FILE = "/etc/os-release";
		private static bool? isThisLinux = null;
		private static string distribution = null;
		private static string version = null;

		public static bool IsThisLinux { get
		{
			if (null != isThisLinux) return (bool)isThisLinux;

			bool r = Unix.IsThisUnix;

			r &= File.Exists(RELEASE_FILE);
			if (r)
			{
				using (StreamReader reader = new StreamReader(RELEASE_FILE))
				{
					string entry;
					while (null != (entry = reader.ReadLine()))
					{
						if (!entry.Contains("=")) continue;
						string[] kv = entry.Split('=');
						switch (kv[0])
						{
							case "NAME":
								distribution = kv[1].Trim();
								break;

							case "VERSION_ID":
								version = kv[1].Trim();
								break;
						}
					}
				}
			}
			return (bool)(isThisLinux = r);
		} }

		public static string Distribution {  get
		{
			if (null != distribution) return distribution;
			bool r = IsThisLinux;
			return r ? distribution : "N/A";
		} }

		public static string Version {  get
		{
			if (null != version) return version;
			bool r = IsThisLinux;
			return r ? version : "N/A";
		} }
	}

	public static class SteamOS
	{
		private static bool? isThisSteamOS = null;

		public static bool IsThisSteamOS { get
		{
			if (null != isThisSteamOS) return (bool)isThisSteamOS;

			bool r = Unix.IsThisLinux;
			r &= "SteeamOS".Equals(Linux.Distribution);
			return (bool)(isThisSteamOS = r);
		} }

		private static bool? isGameMode = null;
		public static bool IsGameMode()
		// Returns True if Deck is in Game Mode, False if in Desktop mode
		{
			if (null != isGameMode) return (bool)isGameMode;

			bool r = IsThisSteamOS;
			r &=	("1" == Environment.GetEnvironmentVariable("SteamDeck"))
					||
					"gamescope".Equals(Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP"))
				;

			return (bool)(isGameMode = r);
		}

		public static string Version {  get
		{
			bool r = IsThisSteamOS;
			return r ? Linux.Version : "N/A";
		} }
	}

	public static class SteamDeck
	{
		private static readonly string BOARD_VENDOR = "/sys/devices/virtual/dmi/id/board_vendor";
		private static readonly string BOARD_NAME = "/sys/devices/virtual/dmi/id/board_name";
		private static bool? isSteamDeck = null;
		public static bool IsThisSteamDeck {  get 
		{
			if (null != isSteamDeck) return (bool)isSteamDeck;

			bool r = Unix.IsThisUnix;
			r &= File.Exists(BOARD_VENDOR);
			if (r)
			{
				string data = File.ReadAllText(BOARD_VENDOR);
				r &= "Valve".Equals(data);
			}
			if (r)
			{
				string data = File.ReadAllText(BOARD_NAME);
				r &= "Jupiter".Equals(data);
			}
			return (bool)(isSteamDeck = r);
		} }

		private static bool? isGameMode = null;
		public static bool IsRunningGameMode {  get
		// Returns True if Deck is in Game Mode, False if in Desktop mode
		{
			if (null != isGameMode) return (bool)isGameMode;

			bool r = Unix.IsThisUnix;
			r &=	("1" == Environment.GetEnvironmentVariable("SteamDeck"))
					||
					"gamescope".Equals(Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP"))
				;

			return (bool)(isGameMode = r);
		} }
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
				// Windows is a mess, I couldn't found a reliable way to prevent false positives.
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
