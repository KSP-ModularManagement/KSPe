/*
	This file is part of KSPe, a component for KSP API Extensions/L
		© 2018-22 LisiasT : http://lisias.net <support@lisias.net>

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
namespace KSPe.Util.Log
{
	internal class UnityLogDecorator : UnityEngine.ILogHandler
	{
		internal static readonly UnityEngine.ILogHandler ORIGINAL_LOGGER =  UnityEngine.Debug.logger.logHandler;

		internal static UnityEngine.ILogHandler instance;
		internal static UnityEngine.ILogHandler INSTANCE {
			get {
				if (null != instance) return instance;
#if false
				// I was wrong on thinking Unity had a problem on concurrency on logging.
				// The source of the problem is KSP and, at least up to KSP 1.11.1, the problem persists.
				instance = (UnityTools.UnityVersion >= 2019)
						? ORIGINAL_LOGGER
						: new UnityLogDecorator();
#else
				instance = new UnityLogDecorator();
#endif
				return instance;
			}
		}

		private readonly UnityEngine.ILogHandler upstream;
		private static readonly object MUTEX = new object();

		private UnityLogDecorator()
		{
			this.upstream = UnityEngine.Debug.logger.logHandler;
			UnityEngine.Debug.logger.logHandler = this;
		}

		void UnityEngine.ILogHandler.LogException(Exception exception, UnityEngine.Object context)
		{
			lock (MUTEX)
			{
				this.upstream.LogException(exception, context);
			}
		}

		void UnityEngine.ILogHandler.LogFormat(UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
			lock (MUTEX)
			{
				this.upstream.LogFormat(logType, context, format, args);
			}
		}
	}
}
