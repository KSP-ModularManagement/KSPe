/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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
using KAssemblyLoader = AssemblyLoader;
using SIO = System.IO;

namespace KSPe.Util
{
	public static class ModuleManagerTools
	{
		private const string MMEXPCACHE = "PluginData/ModuleManager/ConfigCache.cfg";
		private const string MMCACHE = "GameData/ModuleManager.ConfigCache";

		public static bool IsLoadedFromCache => GetLoadedFromCache();
		private static bool? _LoadedFromCache = null;
		private static bool GetLoadedFromCache()
		{
			if (null == _LoadedFromCache)
			{
				try
				{
					// Check if we are using MM /Experimental. Use its attribute if yes.
					foreach (KAssemblyLoader.LoadedAssembly asm in KAssemblyLoader.loadedAssemblies) if (null != asm && asm.name.Equals("ModuleManager"))
						foreach (Type type in asm.assembly.GetTypes()) if (type.IsClass && type.FullName.Equals("ModuleManager.ModuleManager"))
						{
							UnityEngine.Object o = UnityEngine.Object.FindObjectOfType(type);
							if(null == o) goto BREAK;
							System.Reflection.PropertyInfo p = type.GetProperty("IsLoadedFromCache", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
							if(null == p) goto BREAK;
							return (bool)(_LoadedFromCache = (bool)p.GetValue(null, null));
						}
				}
				catch (Exception e)
				{
					KSPe.Log.error("Error [{0}] while finding Object Module Manager! Going to the brute force way.", e);
				}
				BREAK:

				// Otherwise, let's go for the brute force method: check the ConfigCaches timestamps.

				KSPe.Log.debug("Checking Module Manager's ConfigCache...");
				try
				{
					double hours = double.MaxValue;
					{
						string path = IO.Path.Combine(KSPUtil.ApplicationRootPath, MMEXPCACHE);
						if (SIO.File.Exists(path))
						{
							SIO.FileInfo fi = new SIO.FileInfo(path);
							System.DateTime lastmodified = fi.LastWriteTime;
							double h = (System.DateTime.Now - lastmodified).TotalHours;
							hours = Math.Min(hours, h);
						}
					}
					{
						string path = SIO.Path.Combine(KSPUtil.ApplicationRootPath, MMCACHE);
						if (SIO.File.Exists(path))
						{
							SIO.FileInfo fi = new SIO.FileInfo(path);
							System.DateTime lastmodified = fi.LastWriteTime;
							double h = (System.DateTime.Now - lastmodified).TotalHours;
							hours = Math.Min(hours, h);
						}
					}

					// Let's take caches with more than one hour an indication that we are using them.
					// I have notice of people taking up to 40 minutes to load KSP, so it's better to be conservative
					// and be absolutely sure the messages will be issued at least once.
					_LoadedFromCache = (hours > 1);
				}
				catch (Exception e)
				{
					KSPe.Log.error("Error [{0}] while checking Module Manager's cache age!", e);
					_LoadedFromCache = false;
				}
			}
			return (bool)_LoadedFromCache;
		}
	}
}
