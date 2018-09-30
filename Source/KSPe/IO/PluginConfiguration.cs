/*
    This file is part of KSPe, a component for KSP API Extensions/L
    (C) 2018 Lisias T : http://lisias.net <support@lisias.net>

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

using KSP;
using System;
using System.IO;

namespace KSPe.IO
{
	public class PluginConfiguration : KSP.IO.PluginConfiguration
	{
		protected PluginConfiguration(string pathToFile) : base(pathToFile) { }

		public static PluginConfiguration CreateForType<T>(string filename)
		{
			Type target = typeof(T);
			string fn = Path.Combine(KSPUtil.ApplicationRootPath, "PluginData");
			fn = Path.Combine(fn, target.Namespace);

			// TODO: Checar como funciona o Vessel. Possivelmente nesse caso o XML deve estar no savegame!
			fn = Path.Combine(fn, filename);
			{
				string d = System.IO.Path.GetDirectoryName(fn);
				if (!System.IO.Directory.Exists(d))
					System.IO.Directory.CreateDirectory(d);
			}
			return new PluginConfiguration(fn);
		}

		public static new PluginConfiguration CreateForType<T>(Vessel flight)
		{
			Type target = typeof(T);
			return CreateForType<T>((flight ? flight.GetName() + "." + target.Name : target.Name) + ".xml");
		}

		public static PluginConfiguration CreateForType<T>()
		{
			return CreateForType<T>((Vessel)null);
		}
	}
}
