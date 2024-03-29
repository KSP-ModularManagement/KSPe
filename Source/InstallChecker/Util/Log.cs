/*
	This file is part of KSPe.InstallChecker, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

	KSP Enhanced /L is licensed as follows:
		* SKL 1.0 : https://ksp.lisias.net/SKL-1_0.txt

	And you are allowed to choose the License that better suit your needs.

	KSP Enhanced /L is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the SKL Standard License 1.0
	along with KSP Enhanced /L. If not, see <https://ksp.lisias.net/SKL-1_0.txt>.
*/
using System.Diagnostics;

namespace KSPe
{
	public static class Log
	{
		internal static void force(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.InstallChecker] " + msg, @params);
		}

		internal static void info(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.InstallChecker] INFO: " + msg, @params);
		}

		internal static void warn(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogWarningFormat("[KSPe.InstallChecker] WARNING: " + msg, @params);
		}

		internal static void detail(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.InstallChecker] DETAIL: " + msg, @params);
		}

		internal static void trace(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.InstallChecker] TRACE: " + msg, @params);
		}

		internal static void error(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogErrorFormat("[KSPe.InstallChecker] ERROR: " + msg, @params);
		}

		[ConditionalAttribute("DEBUG")]
		internal static void debug(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.InstallChecker] TRACE: " + msg, @params);
		}
	}
}
