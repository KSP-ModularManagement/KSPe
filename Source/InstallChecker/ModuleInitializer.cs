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
namespace KSPe.InstallChecker
{
	public static class ModuleInitializer
	{
		public static void Initialize()
		{
			if (!SanityLib.CheckInstalled("000_KSPe"))
			{
				Log.warn("KSPe's directory was removed. The bootstrap is removing itself from the Loading System, but you may need to delete manually some `*.delete-me` files in your `GameData`. Nothing bad will happen by leaving them there, however.");
				if (!SanityLib.KillMyself("001_KSPe.dll"))
					SanityLib.KillMyself("000_KSPe.dll");
				return;
			}
			Checker o = new Checker();
			o.CheckKSPe();
		}
	}
}
