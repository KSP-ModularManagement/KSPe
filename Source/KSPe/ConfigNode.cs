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
using System.Reflection;

namespace KSPe
{
	// FIXME: Implementar todas as chamadas, e não apenas o GetNode! Tornar a abstração 100% transparente!
	public class ConfigNodeWithSteroids : global::ConfigNode
	{
		public ConfigNodeWithSteroids() : base() { }

		public static ConfigNodeWithSteroids from (global::ConfigNode obj)
		{
			ConfigNodeWithSteroids r = new ConfigNodeWithSteroids();
			r.CopyFrom(obj);
			return r;
		}

		public T GetValue<T>(string name)
		{
			string value = base.GetValue(name);
			return (T)Convert.ChangeType(value, typeof(T));
		}

		public T GetValue<T>(string name, T defaultValue)
		{
			return base.HasValue(name) ? this.GetValue<T>(name) : defaultValue;
		}

		public new ConfigNodeWithSteroids GetNode(string name)
		{
			return ConfigNodeWithSteroids.from(base.GetNode(name));
		}

		private void CopyFrom(Object source)
		{
			Type sourceType = source.GetType();
			Type destinationType = this.GetType().BaseType;

			foreach (FieldInfo sourceField in sourceType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
			{
				FieldInfo destinationField = destinationType.GetField(sourceField.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				if (destinationField != null)
					destinationField.SetValue(this, sourceField.GetValue(source));
			}
		}
	}
}
