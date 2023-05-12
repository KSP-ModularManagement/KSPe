/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe.InstallChecker is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System;
using SIO = System.IO;

namespace KSPe.InstallChecker
{
	public class Globals
	{
		private static Globals INSTANCE = null;
		public static Globals Instance => INSTANCE ?? (INSTANCE = new Globals());

		public readonly DateTime LastCkanMessage;

		private readonly static string DIRNAME = SIO.Path.Combine(KSPUtil.ApplicationRootPath, "PluginData");
		private readonly static string PATHNAME = SIO.Path.Combine(DIRNAME,	"KSPe.InstallChecker.cfg");

		private Globals()
		{
			try
			{
				ConfigNode cn = ConfigNode.Load(PATHNAME).GetNode("KSPe.InstallChecker");;
				string value = cn.GetValue("LastCkanMessage");
				this.LastCkanMessage = DateTime.Parse(value);
			}
			catch (Exception)
			{
				this.LastCkanMessage = DateTime.Parse("1900-01-01 00:00:00Z");
			}
		}

		internal void HitCkanMessage()
		{
			ConfigNode cn = ConfigNode.Load(PATHNAME) ?? new ConfigNode("KSPe.InstallChecker");
			if (!cn.HasNode("KSPe.InstallChecker")) cn.SetNode("KSPe.InstallChecker", new ConfigNode("KSPe.InstallChecker"), true);
			ConfigNode ccn = cn.GetNode("KSPe.InstallChecker");
			ccn.SetValue("LastCkanMessage", DateTime.Now.ToUniversalTime().ToString("u"), true);
			if (!SIO.Directory.Exists(DIRNAME))
				SIO.Directory.CreateDirectory(DIRNAME);
			cn.Save(PATHNAME);
			INSTANCE = null;
		}
	}
}
