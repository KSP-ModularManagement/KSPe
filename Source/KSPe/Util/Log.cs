/*
	This file is part of KSPe.Loader, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

	KSPe.Loader is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSPe.Loader is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSPe.Loader. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System.Diagnostics;

namespace KSPe.Loader
{
	internal static class Log
	{
		internal static void force(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.Loader] " + msg, @params);
		}

		internal static void info(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.Loader] INFO: " + msg, @params);
		}

		internal static void detail(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.Loader] DETAIL: " + msg, @params);
		}

		internal static void trace(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.Loader] TRACE: " + msg, @params);
		}

		internal static void error(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogErrorFormat("[KSPe.Loader] ERROR: " + msg, @params);
		}

		internal static void error(System.Exception ex)
		{
			UnityEngine.Debug.LogErrorFormat("[KSPe.Loader] ERROR: {0}\n{1}", ex.Message, ex.StackTrace);
		}

		[ConditionalAttribute("DEBUG")]
		internal static void debug(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.Loader] TRACE: " + msg, @params);
		}
	}
}
