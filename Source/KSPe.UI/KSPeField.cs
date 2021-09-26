/*
	This file is part of KSPe, a component for KSP API Extensions/L
	(C) 2018-21 Lisias T : http://lisias.net <support@lisias.net>

	KSPe API Extensions/L is double licensed, as follows:

	* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt
	* GPL 2.0 : https://www.gnu.org/licenses/gpl-2.0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe API Extensions/L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe API Extensions/L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.

	You should have received a copy of the GNU General public new License 2.0
	along with KSPe API Extensions/L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
namespace KSPe.UI
{
	public class KSPeField:KSPField
	{
		public new bool isPersistant
		{
			get => base.isPersistant;
			set => base.isPersistant = value;
		}

		public new bool guiActive
		{
			get => base.guiActive;
			set => base.guiActive = value;
		}

		public new bool guiActiveEditor
		{
			get => base.guiActiveEditor;
			set => base.guiActiveEditor = value;
		}

		public new string guiName
		{
			get => base.guiName;
			set => base.guiName = value;
		}

		public new string guiUnits		// This changed to a property i KSP 1.8!!
		{
			get => this.getStringSmart("guiUnits");
			set => this.setStringSmart("guiUnits", value);
		}

		public new string guiFormat
		{
			get => base.guiFormat;
			set => base.guiFormat = value;
		}

		public new string category
		{
			get => base.category;
			set => base.category = value;
		}

		public new bool advancedTweakable
		{
			get => base.advancedTweakable;
			set => base.advancedTweakable = value;
		}


		// 1.8

#pragma warning disable CS0109 // Member does not hide an inherited member; new keyword is not required
		public new bool guiActiveUnfocused
		{
			get => this.getBool("guiActiveUnfocused");
			set => this.setBool("guiActiveUnfocused", value);
		}

		public new float unfocusedRange
		{
			get => this.getFloat("unfocusedRange");
			set => this.setFloat("unfocusedRange", value);
		}

		public new string groupName
		{
			get => this.getString("groupName");
			set => this.setString("groupName", value);
		}

		public new string groupDisplayName
		{
			get => this.getString("groupDisplayName");
			set => this.setString("groupDisplayName", value);
		}

		public new bool groupStartCollapsed
		{
			get => this.getBool("groupStartCollapsed");
			set => this.setBool("groupStartCollapsed", value);
		}
#pragma warning restore CS0109 // Member does not hide an inherited member; new keyword is not required

		private void setBool(string v, bool value)
		{
			System.Reflection.FieldInfo fi = typeof(KSPField).GetField(v, System.Reflection.BindingFlags.Instance);
			if (null != fi) fi.SetValue(this, value);
		}

		private bool getBool(string v)
		{
			System.Reflection.PropertyInfo pi = typeof(KSPField).GetProperty(v, System.Reflection.BindingFlags.Instance);
			if (null != pi) return (bool) pi.GetValue(this, null);
			return false;
		}

		private void setFloat(string v, float value)
		{
			System.Reflection.FieldInfo fi = typeof(KSPField).GetField(v, System.Reflection.BindingFlags.Instance);
			if (null != fi) fi.SetValue(this, value);
		}

		private float getFloat(string v)
		{
			System.Reflection.PropertyInfo pi = typeof(KSPField).GetProperty(v, System.Reflection.BindingFlags.Instance);
			if (null != pi) return (float) pi.GetValue(this, null);
			return float.NaN;
		}

		private string getString(string v) {
			System.Reflection.PropertyInfo pi = typeof(KSPField).GetProperty(v, System.Reflection.BindingFlags.Instance);
			if (null != pi) return (string) pi.GetValue(this, null);
			return "";
		}

		private void setString(string v, string value)
		{
			System.Reflection.FieldInfo fi = typeof(KSPField).GetField(v, System.Reflection.BindingFlags.Instance);
			if (null != fi) fi.SetValue(this, value);
		}

		private string getStringSmart(string v) {
			System.Reflection.FieldInfo fi = typeof(KSPField).GetField(v, System.Reflection.BindingFlags.Instance);
			if (null != fi) return (string) fi.GetValue(this);
			System.Reflection.PropertyInfo pi = typeof(KSPField).GetProperty(v, System.Reflection.BindingFlags.Instance);
			if (null != pi) return (string) pi.GetValue(this, null);
			return "";
		}

		private void setStringSmart(string v, string value)
		{
			System.Reflection.FieldInfo fi = typeof(KSPField).GetField(v, System.Reflection.BindingFlags.Instance);
			if (null != fi) fi.SetValue(this, value);
			System.Reflection.PropertyInfo pi = typeof(KSPField).GetProperty(v, System.Reflection.BindingFlags.Instance);
			if (null != pi) pi.SetValue(this, value, null);
		}

	}
}
