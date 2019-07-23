/*
	This file is part of KSPe, a component for KSP API Extensions/L
	unless when specified otherwise below this code is:

	(C) 2018-19 Lisias T : http://lisias.net <support@lisias.net>

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
    		bool unity5 = typeof(UTexture2D).GetMethod("LoadImage") != null;
			tex = new UTexture2D(16, 16, TextureFormat.ARGB32, false);
			return unity5
				? (bool)loadImageMethod(unity5).Invoke(tex, new object[] { data })
				: (bool)loadImageMethod(unity5).Invoke(null, new object[] { tex, data, markNonReadable });
		}

		private static MethodInfo _loadImageMethod = null;
		private static MethodInfo loadImageMethod(bool unity5)
		{
			if (null == _loadImageMethod)
			{
				if (unity5)
				{
					dbg("Unity 5 : KSP <= 1.3.1");
					_loadImageMethod = typeof(UTexture2D).GetMethod("LoadImage");
				}
				else
				{
					dbg("Unity 2018 : KSP >= 1.4");
					Assembly unityEngineAssembly = Assembly.Load("UnityEngine");
					Type imageConversionClass = unityEngineAssembly.GetType("UnityEngine.ImageConversion");
					_loadImageMethod = imageConversionClass.GetMethod(
								"LoadImage", 
								BindingFlags.Static | BindingFlags.Public, 
								null,  
								new [] {typeof(UTexture2D), typeof(byte[]), typeof(bool)}, 
								null
							);
					}
			}
			return _loadImageMethod;
		}

		[ConditionalAttribute("DEBUG")]
		private static void dbg(string msg, params object[] p)
		{
			UnityEngine.Debug.LogFormat("KSPe.Util.Image.File: " + msg, p);
		}

		[ConditionalAttribute("DEBUG")]
		private static void dbg(Exception ex)
		{
			UnityEngine.Debug.LogError("KSPe.Util.Image.File: " + ex.ToString());
			UnityEngine.Debug.LogException(ex);
		}
	}
}