/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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

using System.Collections.Generic;
using System.Linq;

namespace KSPe.InstallChecker
{
	public static class SanityLib
	{
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
			string sourceFilename = System.IO.Path.Combine(SanityLib.CalcGameData(), ud.sourceFilename);
			string targetFilename = System.IO.Path.Combine(SanityLib.CalcGameData(), ud.targetFilename);

			Log.debug("UpdateIfNeeded from {0} to {1}", sourceFilename, targetFilename);
			if (System.IO.File.Exists(sourceFilename))
			{
				if (System.IO.File.Exists(targetFilename))
				{
					{ 
						System.Diagnostics.FileVersionInfo sourceVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(sourceFilename);
						System.Diagnostics.FileVersionInfo targetVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(targetFilename);

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
					{
						System.Reflection.Assembly sourceAsm = System.Reflection.Assembly.LoadFile(sourceFilename);
						System.Reflection.Assembly targetAsm = System.Reflection.Assembly.LoadFile(targetFilename);
						if (!sourceAsm.GetName().Version.Equals(targetAsm.GetName().Version))
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
				}
				else return SanityLib.Update(ud.name, sourceFilename, targetFilename);
			}
			// Nothing to do. If this is an error, someone else will yell about.
			return null;
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
			System.IO.File.Copy(sourceFilename, targetFilename);
		}

		private static void Delete(string filename)
		{
			Log.debug("Deleting {0}", filename);
			if (System.IO.File.Exists(filename))
				System.IO.File.Delete(filename);
		}

		private static string GAMEDATA = null;
		private static string CalcGameData()
		{
			if (null != GAMEDATA) return GAMEDATA;
			System.Reflection.Assembly asm = System.Reflection.Assembly.GetAssembly(typeof(UnityEngine.MonoBehaviour));
			string path = System.IO.Path.GetDirectoryName(asm.Location);
			string candidate = System.IO.Path.Combine(path, "GameData");
			try
			{
				while (!System.IO.Directory.Exists(candidate))
				{
					path = System.IO.Path.GetDirectoryName(path);
					candidate = System.IO.Path.Combine(path, "GameData");
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
