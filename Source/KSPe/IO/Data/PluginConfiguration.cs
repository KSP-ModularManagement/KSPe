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
using SIO = System.IO;
using KSP;

namespace KSPe.IO.Data
{
	// TODO: Eliminate on Vesion 3
	[System.Obsolete("KSPe.IO.Data.PluginConfiguration is deprecated, please use KSPe.IO.Data<T>.PluginConfiguration instead.")]
	public class PluginConfiguration : KSP.IO.PluginConfiguration
	{
		protected readonly string pathname;
		protected PluginConfiguration(string pathname) : base(pathname) {
			this.pathname = pathname;
		}

		public bool exists()
		{
			return SIO.File.Exists(this.pathname);
		}

		public void delete()
		{
			if (SIO.File.Exists(this.pathname))
				SIO.File.Delete(this.pathname);
		}
		
		public new PluginConfiguration load()
		{
			base.load();
			return this;
		}

		public static PluginConfiguration CreateForType<T>(string filename)
		{
			string path = File<T>.Data.FullPathName(true, filename);
			return new PluginConfiguration(path);
		}

		public static PluginConfiguration CreateForType<T>(string fn, params string[] fns)
		{
			string path = File<T>.Data.FullPathName(true, fn, fns);
			return new PluginConfiguration(path);
		}

		public static new PluginConfiguration CreateForType<T>(Vessel flight)
		{
			Type target = typeof(T);
			return CreateForType<T>((flight ? flight.GetName() + "." + target.Name : target.Name) + ".xml");
		}

		public static PluginConfiguration CreateForType<T>(Vessel flight, params string[] fns)
		{
			Type target = typeof(T);
			string path = fns[0];
			string[] ffns = new string[fns.Length];
			Array.Copy(fns, 1, ffns, 0, ffns.Length);
			foreach (string s in ffns)
				path = Path.Combine(path, s);
			path = Path.Combine(path, (flight ? flight.GetName() + "." + target.Name : target.Name) + ".xml");
			return CreateForType<T>(path);
		}

		public static PluginConfiguration CreateForType<T>()
		{
			return CreateForType<T>((Vessel)null);
		}
	}
}
