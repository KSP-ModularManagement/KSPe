/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;
using SIO = System.IO;
using System.Collections.Generic;
using System.Linq;

namespace KSPe.InstallChecker
{
	public static class SanityLib
	{
		private const string DELETEME = ".delete-me";

		/**
		 * If you are interested only on assemblies that were properly loaded by KSP, this is the one you want.
		 */
		internal static IEnumerable<AssemblyLoader.LoadedAssembly> FetchLoadedAssembliesByName(string assemblyName)
		{ 
			return from a in AssemblyLoader.loadedAssemblies
					let ass = a.assembly
					where assemblyName == ass.GetName().Name
					orderby a.path ascending
					select a
				;
		}

		internal struct UpdateData
		{
			public readonly string name;
			public readonly string sourceFilename;
			public readonly string targetFilename;

			internal UpdateData(string name, string sourceFilename, string targetFilename)
			{
				this.name = name;
				this.sourceFilename = sourceFilename;
				this.targetFilename = targetFilename;
			}
		}
		internal static string UpdateIfNeeded(UpdateData ud)
		{
			string sourceFilename = SIO.Path.Combine(SanityLib.CalcGameData(), ud.sourceFilename);
			string targetFilename = SIO.Path.Combine(SanityLib.CalcGameData(), ud.targetFilename);

			{ 
				string tempFilename = targetFilename + DELETEME;
				if (SIO.File.Exists(tempFilename)) SIO.File.Delete(tempFilename);
			}

			Log.debug("UpdateIfNeeded from {0} to {1}", sourceFilename, targetFilename);
			if (SIO.File.Exists(sourceFilename))
			{
				if (SIO.File.Exists(targetFilename))
				{
					{ 
						System.Diagnostics.FileVersionInfo sourceVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(sourceFilename);
						System.Diagnostics.FileVersionInfo targetVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(targetFilename);

						Log.debug("src:{0} ; tgt:{1}", sourceVersionInfo, targetVersionInfo);

						bool sane = sourceVersionInfo.ProductName.Equals(targetVersionInfo.ProductName);
						sane &= sourceVersionInfo.LegalCopyright.Equals(targetVersionInfo.LegalCopyright);
						sane &= sourceVersionInfo.LegalTrademarks.Equals(targetVersionInfo.LegalTrademarks);
						sane &= sourceVersionInfo.CompanyName.Equals(targetVersionInfo.CompanyName);

						if (!sane)
						{
							Log.info("File {0} is not compatible with {1}. This is going to cause trouble, replacing it!", targetFilename, ud.name);
							Delete(targetFilename);	// Remove the file and update it no matter what!
							return Update(ud.name, sourceFilename, targetFilename);
						}
					}

					if (ShouldUpdate(sourceFilename, targetFilename))
					{ 
						Log.info("File {0} is older then {1}. This is going to cause trouble, updating it!", targetFilename, ud.name);
						Delete(targetFilename);	// Remove the file or the update will not work.
						return Update(ud.name, sourceFilename, targetFilename);
					}
					else
					{
						Delete(sourceFilename);
						return null;
					}
				}
				else return SanityLib.Update(ud.name, sourceFilename, targetFilename);
			}
			// Nothing to do. If this is an error, someone else will yell about.
			return null;
		}

		internal static bool CheckInstalled(string directory)
		{
			string pathname = SIO.Path.Combine(CalcGameData(), directory);
			return SIO.Directory.Exists(pathname);
		}

		internal static bool KillMyself(string dllName)
		{
			string pathname = SIO.Path.Combine(CalcGameData(), dllName);
			bool r = SIO.File.Exists(pathname);
			if (r) Delete(pathname);
			else
			{
				string tempname = pathname + DELETEME;
				if (SIO.File.Exists(tempname)) SIO.File.Delete(tempname);
			}
			return r;
		}

		private static bool ShouldUpdate(string srcfn, string tgtfn)
		{
			// Squad, In your endless eagerness to do anything but the technically correct solution, royally screwed up the
			// Assembly Loader/Resolver defeating any attempt to check for the Assemblies' metadata.
			//
			// You can load the Asm into a byte array if you want, the thing will be shortcircuited no matter what somewhere
			// in the loading process to whatever was loaded first.
			//
			// So we will rely on file versions, what's semantically incorrect but it should work as long no one forks KSPe e changes something.
			if (1 == Versioning.version_major && Versioning.version_minor >= 8)
			{
				System.Diagnostics.FileVersionInfo sourceVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(srcfn);
				System.Diagnostics.FileVersionInfo targetVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(tgtfn);

				Log.debug("1.12.x src:{0} ; tgt:{1}", sourceVersionInfo, targetVersionInfo);
				// We already know the files are "sane", otherwise we would not be here. So let's check only the version:
				return 0 != sourceVersionInfo.FileVersion.CompareTo(targetVersionInfo.FileVersion);
			}

			System.Reflection.Assembly sourceAsm = System.Reflection.Assembly.ReflectionOnlyLoad(SIO.File.ReadAllBytes(srcfn));
			System.Reflection.Assembly targetAsm = System.Reflection.Assembly.ReflectionOnlyLoad(SIO.File.ReadAllBytes(tgtfn));

			Log.debug("src:{0} ; tgt:{1}", sourceAsm, targetAsm);

			return !sourceAsm.GetName().Version.Equals(targetAsm.GetName().Version);
		}

		private static string Update(string name, string sourceFilename, string targetFilename)
		{
			Log.debug("Update from {0} to {1}", sourceFilename, targetFilename);
			try
			{
				Copy(sourceFilename, targetFilename);
				Delete(sourceFilename);
				return string.Format("{0} was updated.", name);
			}
			catch (Exception e)
			{
				Log.error("Error while installing {0}", sourceFilename); // the Exception is logged on the caller's handler
				throw e;
			}
		}

		private static void Copy(string sourceFilename, string targetFilename)
		{
			Log.debug("Copying {0} to {1}", sourceFilename, targetFilename);
			SIO.File.Copy(sourceFilename, targetFilename);
		}

		private static void Delete(string filename)
		{
			Log.debug("Deleting {0}", filename);
			if (SIO.File.Exists(filename)) try
			{
				SIO.File.Delete(filename);
			}
			catch (Exception e) when (e is System.UnauthorizedAccessException || e is System.Security.SecurityException)
			{
				// Oukey, we are in Windows and it locks the DLL file once it's loaded.
				// But we can rename it, and delete it later.
				string tempname = filename + DELETEME;
				if (SIO.File.Exists(tempname)) SIO.File.Delete(tempname);
				SIO.File.Move(filename, tempname);
			}
		}

		private static string GAMEDATA = null;
		private static string CalcGameData()
		{
			if (null != GAMEDATA) return GAMEDATA;
			System.Reflection.Assembly asm = System.Reflection.Assembly.GetAssembly(typeof(UnityEngine.MonoBehaviour));
			string path = SIO.Path.GetDirectoryName(asm.Location);
			string candidate = SIO.Path.Combine(path, "GameData");
			try
			{
				while (!SIO.Directory.Exists(candidate))
				{
					path = SIO.Path.GetDirectoryName(path);
					candidate = SIO.Path.Combine(path, "GameData");
				}
				Log.debug("GameData found on {0}", candidate);
				return (GAMEDATA = candidate);
			}
			catch (Exception e)
			{
				Log.error("Error while looking for the GameData! : {0}", e.ToString());
				throw e;
			}
		}
	}
}
