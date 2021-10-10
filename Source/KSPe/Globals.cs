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
using System.Collections.Generic;
using KSPE = KSPe;

namespace KSPe
{
	// CAUTION! This class CAN NOT use the KSPe Logging Facilities, as it's a dependency from it!!
	public class Globals
	{
		public class LogConfig
		{
			public readonly Util.Log.Level Level;
			public readonly bool ThreadSafe;

			private LogConfig(Util.Log.Level Level, bool ThreadSafe)
			{
				this.Level = Level;
				this.ThreadSafe = ThreadSafe;
			}

			public override string ToString()
			{
				return string.Format("{{ Level:{0} ThreadSafe:{1} }}", this.Level, this.ThreadSafe);
			}

			internal static LogConfig from(KSPe.ConfigNodeWithSteroids node)
			{
				int defaultLevel = GameSettings.VERBOSE_DEBUG_LOG ? (int)Util.Log.Level.DETAIL : (int)Util.Log.Level.INFO;
				int level = node.GetValue<int>("LogLevel", defaultLevel);
				level = Math.Max(0, Math.Min(5, level));
				return new LogConfig(
					(Util.Log.Level)level,
					node.GetValue<bool>("ThreadSafe", false)
				);
			}

			internal static LogConfig createDefault()
			{
				Util.Log.Level defaultLevel = GameSettings.VERBOSE_DEBUG_LOG ? Util.Log.Level.DETAIL : Util.Log.Level.INFO;
				return new LogConfig(defaultLevel, false);
			}
		}

		public readonly bool DebugMode;
		public readonly LogConfig Log;

		private Globals(bool DebugMode, LogConfig Log)
		{
			this.DebugMode = DebugMode;
			this.Log = Log;
		}

		public override string ToString()
		{
			return String.Format("{{ DebugMode:{0} Log:{1} }}", this.DebugMode, this.Log);
		}

		internal static Globals from(KSPe.ConfigNodeWithSteroids node)
		{
			return new Globals(
				node.GetValue<bool>("DebugMode", false),
				LogConfig.from(node.GetNode("Log"))
			);
		}

		internal static Globals createDefault()
		{
			return new Globals(false, LogConfig.createDefault());
		}


		internal static Globals _default;
		internal static Dictionary<string, Globals> _locals = new Dictionary<string, Globals>();
		internal static void Init()
		{
			if (!System.IO.File.Exists("PluginData/KSPe.cfg"))
			{
				LOG.info("KSPe.cfg does not exists. Using defaults.");
				_default = Globals.createDefault();
			}
			else try
			{
				ConfigNode node = ConfigNode.Load("PluginData/KSPe.cfg");
				KSPE.ConfigNodeWithSteroids sn = KSPE.ConfigNodeWithSteroids.from(node);
				sn = sn.GetNode("KSPe");
				_default = Globals.from(sn);
				LOG.info("Globals: Default {0} ", _default);
				if (sn.HasNode("LOCAL"))
					foreach (ConfigNode n in sn.GetNode("LOCAL").nodes)
						try
						{
							_locals.Add(n.name, Globals.from(KSPE.ConfigNodeWithSteroids.from(n)));
							LOG.info("Globals: {0} {1} ", n.name, _locals[n.name]);
						}
						catch (Exception e)
						{
							LOG.error(e, "Error trying to read Node {0} : {1}", n.name);
						}
				LOG.info("KSPe.cfg loaded.");
			}
			catch (Exception e)
			{
				LOG.error(e, "Error on reading KSPe.cfg dur '{0}'. Using defaults.", e.Message);
				_default = Globals.createDefault();
			}
		}
		internal static Globals Get(Type type)
		{
			return Get(type.Namespace);
		}
		private static Globals Get(string nameSpace)
		{
			if (null == _default) Init();
			return (_locals.ContainsKey(nameSpace) ? _locals[nameSpace] : _default);
		}
	}

	public class Globals<T>
	{
		public static bool DebugMode { get {
			return Get(typeof(T)).DebugMode;
		} }

		public static Globals.LogConfig Log { get {
			return Get(typeof(T)).Log;
		} }

		private static Globals Get(Type type)
		{
			if (null == Globals._default) Globals.Init();
			if (Globals._locals.ContainsKey(type.Namespace)) return Globals._locals[typeof(T).Namespace];
			List<string> names = new List<string>(typeof(T).Namespace.Split('.'));
			int i = names.Count - 1;
			for (; i > 0 ; --i)
			{
				string subnamespace = String.Join(".", names.GetRange(0, i).ToArray());
				if (Globals._locals.ContainsKey(subnamespace)) return Globals._locals[subnamespace];
			}
			return Globals._default;
		}
	}

	// Internal Globals LOGging, not to be reused by anyone.
	internal static class LOG
	{
		[System.Diagnostics.Conditional("DEBUG")]
		internal static void debug(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.Globals] DEBUG: " + msg, @params);
		}

		internal static void error(Exception e, string msg, params object[] @params)
		{
			UnityEngine.Debug.LogErrorFormat("[KSPe.Globals] ERROR: " + msg, @params);
			UnityEngine.Debug.LogException(e);
		}

		internal static void info(string msg)
		{
			UnityEngine.Debug.Log("[KSPe.Globals] INFO: " + msg);
		}

		internal static void info(string msg, params object[] @params)
		{
			UnityEngine.Debug.LogFormat("[KSPe.Globals] INFO: " + msg, @params);
		}
	}

}
