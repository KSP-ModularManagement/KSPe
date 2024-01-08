/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

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
using System.Linq;
using System.Reflection;

namespace KSPe
{
	// FIXME: Implementar todas as chamadas, e não apenas o GetNode! Tornar a abstração 100% transparente!
	public class ConfigNodeWithSteroids : global::ConfigNode
	{
		public ConfigNodeWithSteroids() : base() { }
		public ConfigNodeWithSteroids(string name) : base(name) { }
		public ConfigNodeWithSteroids(string name, string vcomment) : base(name, vcomment) { }

		public static ConfigNodeWithSteroids from(global::ConfigNode obj)
		{
			ConfigNodeWithSteroids r = new ConfigNodeWithSteroids();
			obj.CopyTo(r);
			//r.CopyFrom(obj);
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

		public bool SetArrayOf<T>(string name, T[] values)
		{
			string value = string.Join(", ", values.Select(s => s.ToString()).ToArray());
			return base.SetValue(name, value);
		}

		public T[] GetArrayOf<T>(string name, T[] defaultValue) => GetArrayOf<T>(name)??defaultValue;
		public T[] GetArrayOf<T>(string name)
		{
			if (!base.HasValue(name)) return null;
			try
			{
				string value = base.GetValue(name);
				return value.Split(',').Select(s => (T)Convert.ChangeType(s, typeof(T))).ToArray();
			}
			catch (Exception e)
			{
				if (!(e is InvalidCastException || e is FormatException || e is OverflowException || e is ArgumentNullException))
					throw;
				Log.warn("Failed to convert string value \"{0}\" to type {1}", name, typeof(T).Name);
				return null;
			}
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
