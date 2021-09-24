/*
	This file is part of KSPe, a component for KSP API Extensions/L
	unless when specified otherwise below this code is:

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

	You should have received a copy of the GNU General Public License 2.0
	along with KSPe API Extensions/L. If not, see <https://www.gnu.org/licenses/>.

*/
using System;
using System.Diagnostics;
using System.Reflection;
using TextureFormat = UnityEngine.TextureFormat;
using UTexture2D = UnityEngine.Texture2D;

namespace KSPe.Util.Image {
	public class Error : Exception
	{
		public readonly Exception ex;
		private readonly string message;
		private readonly object[] parameters;

		public Error(string message) : base(message)
		{
			this.ex = null;
			this.message = message;
			this.parameters = null;
		}

		public Error(string message, params object[] parameters) : base(string.Format(message, parameters))
		{
			this.message = message;
			this.parameters = parameters;
		}

		public Error(Exception ex, string message, params object[] parameters) : base(string.Format(message, parameters))
		{
			this.ex = ex;
			this.message = message;
			this.parameters = parameters;
		}

		public Error(Exception ex) : base(ex.Message)
		{
			this.ex = ex;
			this.message = ex.ToString();
			this.parameters = null;
		}

		public override string ToString()
		{
			return null == this.parameters ? this.message : string.Format(this.message, this.parameters);
		}
	}

	public static class File
	{
		public static bool Load(out UTexture2D tex, byte[] data, bool markNonReadable = false)
		{
			return Load(out tex, 16, 16, data, markNonReadable);
		}

		public static bool Load(out UTexture2D tex, int width, int height, byte[] data, bool markNonReadable = false)
		{
			tex = new UTexture2D(width, height, TextureFormat.ARGB32, false);
			switch (UnityTools.UnityVersion)
			{
				case 5:		return (bool)LoadImageMethod().Invoke(tex, new object[] { data });
				case 2017:
				case 2019:	return (bool)LoadImageMethod().Invoke(null, new object[] { tex, data, markNonReadable });
				default:	return false;
			}
		}

		private static MethodInfo _loadImageMethod = null;
		private static MethodInfo LoadImageMethod()
		{
			if (null == _loadImageMethod)
			{
				switch(UnityTools.UnityVersion)
				{
					case 5: {
						dbg("Unity 5 : KSP <= 1.3.1");
						_loadImageMethod = typeof(UTexture2D).GetMethod("LoadImage", new [] {typeof(byte[])});
						} break;

					case 2017: {
						dbg("Unity 2017 : 1.4 <= KSP < KSP 1.8");
						Assembly unityEngineAssembly = Assembly.Load("UnityEngine");
						Type imageConversionClass = unityEngineAssembly.GetType("UnityEngine.ImageConversion");
						_loadImageMethod = imageConversionClass.GetMethod(
									"LoadImage", 
									BindingFlags.Static | BindingFlags.Public, 
									null,  
									new [] {typeof(UTexture2D), typeof(byte[]), typeof(bool)}, 
									null
								);
						} break;

					case 2019: {
						dbg("Unity 2019 : KSP >= 1.8");
						Assembly unityEngineAssembly = Assembly.Load("UnityEngine");
						Type imageConversionClass = unityEngineAssembly.GetType("UnityEngine.ImageConversion");
						_loadImageMethod = imageConversionClass.GetMethod(
									"LoadImage", 
									BindingFlags.Static | BindingFlags.Public, 
									null,  
									new [] {typeof(UTexture2D), typeof(byte[]), typeof(bool)}, 
									null
								);
						} break;

					default:
						// Houston, we have a problem!
						dbg("Unity Version not recognized!");
						break;
				}
			}
			return _loadImageMethod;
		}

		[ConditionalAttribute("DEBUG")]
		private static void dbg(string msg, params object[] p)
		{
			KSPe.Log.debug("Util.Image.File: " + msg, p);
		}

		[ConditionalAttribute("DEBUG")]
		private static void dbg(Exception ex)
		{
			KSPe.Log.error(ex, "Util.Image.File: {0}", ex);
		}
	}

	public static class Screenshot
	{
		public interface Interface
		{
			void Capture(string pathname);
			void Capture(string pathname, int superSampleValue);
		}

		private class Fallback : Interface
		{
			void Interface.Capture(string pathname)
			{
				Capture(pathname, 1);
			}

			void Interface.Capture(string pathname, int superSampleValue)
			{
				ScreenMessages.PostScreenMessage ("No KSPe Screenshot support installed.");
				KSPe.Log.warn("Util.Image.Screenshot: Screenshot support not properly initialized! Screenshot {0} not taken!", pathname);
			}
		}

		#region Abstracted Calls

		public static void Capture(string pathname)								{ INSTANCE.Capture(pathname); }
		public static void Capture(string pathname, int superSampleValue)		{ INSTANCE.Capture(pathname, superSampleValue); }

		#endregion

		private static readonly Interface INSTANCE;
		private static Interface GetInstance()
		{
			Interface r = (Interface)Multiplatform.Tools.CreateInstanceByInterface("KSPe.Util.Image.Screenshot+Interface");
			if (null != r) return r;

			KSPe.Log.warn("Util.Image.Screenshot: No realisation for the abstract Interface found! Using a fallback one!");
			return (Interface) new Fallback();
		}
		static Screenshot()
		{
			INSTANCE = GetInstance();
		}
	}

}