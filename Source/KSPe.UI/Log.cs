/*
	This file is part of KSPe, a component for KSP API Extensions/L
	© 2018-21 Lisias T : http://lisias.net <support@lisias.net>

	THIS FILE is licensed to you under:

	* WTFPL - http://www.wtfpl.net
		* Everyone is permitted to copy and distribute verbatim or modified
 			copies of this license document, and changing it is allowed as long
			as the name is changed.

	THIS FILE is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/
using System;
using KSPe.Util.Log;
using System.Diagnostics;

namespace KSPe.UI
{
	public static class Log
	{
		private static readonly Logger log = Logger.CreateForType<Startup>(false, 1);

		public static void force (string msg, params object [] @params)
		{
			log.force (msg, @params);
		}

		public static void info(string msg, params object[] @params)
		{
			log.info(msg, @params);
		}

		public static void warn(string msg, params object[] @params)
		{
			log.warn(msg, @params);
		}

		public static void detail(string msg, params object[] @params)
		{
			log.detail(msg, @params);
		}

		public static void trace(string msg, params object[] @params)
		{
			log.trace(msg, @params);
		}

		public static void error(string msg, params object[] @params)
		{
			log.error(msg, @params);
		}

		public static void error(Exception e, object offended)
		{
			log.error(offended, e);
		}

		public static void error(Exception e, string msg, params object[] @params)
		{
			log.error(e, msg, @params);
		}

		[ConditionalAttribute("DEBUG")]
		public static void debug(string msg, params object[] @params)
		{
			log.trace(msg, @params);
		}
	}
}
