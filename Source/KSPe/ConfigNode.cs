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

using KSPe.Util;

namespace KSPe
{
	// FIXME: Implementar todas as chamadas, e não apenas o GetNode! Tornar a abstração 100% transparente!
	public class ConfigNodeWithSteroids : global::ConfigNode
	{
		private delegate object GetValueCall(ConfigNodeWithSteroids cns, string name);
		private static readonly DictionaryValueList<Type, GetValueCall> CUSTOM_TYPES = new DictionaryValueList<Type, GetValueCall>();
		private readonly global::ConfigNode source;

		public ConfigNodeWithSteroids() : base() { }
		public ConfigNodeWithSteroids(string name) : base(name) { }
		public ConfigNodeWithSteroids(string name, string vcomment) : base(name, vcomment) { }

		static ConfigNodeWithSteroids()
		{
			CUSTOM_TYPES[typeof(UnityEngine.Vector2)] = GetValueVector2;
			CUSTOM_TYPES[typeof(UnityEngine.Vector2d)] = GetValueVector2d;
			CUSTOM_TYPES[typeof(UnityEngine.Vector3)] = GetValueVector3;
			CUSTOM_TYPES[typeof(global::Vector3d)] = GetValueVector3d;
			CUSTOM_TYPES[typeof(UnityEngine.Vector4)] = GetValueVector4;
			CUSTOM_TYPES[typeof(UnityEngine.Vector4d)] = GetValueVector4d;
			CUSTOM_TYPES[typeof(UnityEngine.Quaternion)] = GetValueQuaternion;
			CUSTOM_TYPES[typeof(UnityEngine.QuaternionD)] = GetValueQuaternionD;
		}

		private ConfigNodeWithSteroids(global::ConfigNode source) : base()
		{
			this.source = source;
			this.Rollback();
		}

		public static ConfigNodeWithSteroids from(global::ConfigNode scn)
		{
			ConfigNodeWithSteroids r = new ConfigNodeWithSteroids(scn);
			return r;
		}

		public T GetValue<T>(string name)
		{
			if (CUSTOM_TYPES.Contains(typeof(T)))
				return (T)CUSTOM_TYPES[typeof(T)](this, name);

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

		public void Commit()		{ this.source.ClearData(); this.CopyTo(this.source); }
		public void Rollback()		{ this.ClearData(); this.source.CopyTo(this); }

		private static object GetValueVector2(ConfigNodeWithSteroids cns, string name)
		{
			float[] array = cns.GetArrayOf<float>(name);
			return Create.Vector2.from(array);
		}

		private static object GetValueVector2d(ConfigNodeWithSteroids cns, string name)
		{
			double[] array = cns.GetArrayOf<double>(name);
			return Create.Vector2d.from(array);
		}

		private static object GetValueVector3(ConfigNodeWithSteroids cns, string name)
		{
			float[] array = cns.GetArrayOf<float>(name);
			return Create.Vector3.from(array);
		}

		private static object GetValueVector3d(ConfigNodeWithSteroids cns, string name)
		{
			double[] array = cns.GetArrayOf<double>(name);
			return Create.Vector3d.from(array);
		}

		private static object GetValueVector4(ConfigNodeWithSteroids cns, string name)
		{
			float[] array = cns.GetArrayOf<float>(name);
			return Create.Vector4.from(array);
		}

		private static object GetValueVector4d(ConfigNodeWithSteroids cns, string name)
		{
			double[] array = cns.GetArrayOf<double>(name);
			return Create.Vector4d.from(array);
		}

		private static object GetValueQuaternion(ConfigNodeWithSteroids cns, string name)
		{
			float[] array = cns.GetArrayOf<float>(name);
			return Create.Quaternion.from(array);
		}

		private static object GetValueQuaternionD(ConfigNodeWithSteroids cns, string name)
		{
			double[] array = cns.GetArrayOf<double>(name);
			return Create.QuaternionD.from(array);
		}

#if false
		// Old code, preserved here for sentimental reasons. :)
		// I discovered ConfigNode.CopyTo later.
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
#endif
	}
}
