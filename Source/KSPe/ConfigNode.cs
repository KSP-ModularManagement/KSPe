using System;
using System.Reflection;
using UnityEngine.Rendering;

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
		
		public ConfigNodeWithSteroids GetNode(string name)
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
