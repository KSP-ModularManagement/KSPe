/*
	This file is part of KSPe, a component for KSP Enhanced /L
		© 2018-2023 LisiasT : http://lisias.net <support@lisias.net>

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

namespace KSPe.Util.Log {

	internal class UnityUiThreadSafeLogDecorator : UnityEngine.MonoBehaviour, UnityEngine.ILogHandler
	{
		internal static UnityUiThreadSafeLogDecorator instance;
		internal static UnityUiThreadSafeLogDecorator INSTANCE {
			get {
				if (null == instance){
					UnityEngine.GameObject obj = new UnityEngine.GameObject();
					obj.AddComponent<UnityUiThreadSafeLogDecorator>();
					instance = obj.GetComponent<UnityUiThreadSafeLogDecorator>();
				}
				return instance;
			}
		}

		private interface AbstractLogMsg
		{
			void execute();
		}

		private class LogExceptionMsg : AbstractLogMsg
		{
			internal readonly System.Exception exception;
			internal readonly UnityEngine.Object context;

			internal LogExceptionMsg(System.Exception exception, UnityEngine.Object context)
			{
				this.exception = exception;
				this.context = context;
			}

			void AbstractLogMsg.execute()
			{
				UnityUiThreadSafeLogDecorator.INSTANCE.upstream.LogException(this.exception, this.context);
			}
		}

		private class LogFormatMsg : AbstractLogMsg
		{
			internal readonly UnityEngine.LogType logType;
			internal readonly UnityEngine.Object context;
			internal readonly string format;
			internal readonly object[] formatArgs;

			internal LogFormatMsg(UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] formatArgs)
			{
				this.logType = logType;
				this.context = context;
				this.format = format;
				this.formatArgs = formatArgs;
			}

			void AbstractLogMsg.execute()
			{
				UnityUiThreadSafeLogDecorator.INSTANCE.upstream.LogFormat(this.logType, this.context, this.format, this.formatArgs);
			}
		}

		private readonly System.Collections.Generic.Queue<AbstractLogMsg> logQueue = new System.Collections.Generic.Queue<AbstractLogMsg>();
		private readonly UnityEngine.ILogHandler upstream;
		private static readonly object MUTEX = new object();

		internal UnityUiThreadSafeLogDecorator()
		{
			this.upstream = UnityEngine.Debug.logger.logHandler;
		}

		void UnityEngine.ILogHandler.LogException(System.Exception exception, UnityEngine.Object context)
		{
			lock (MUTEX)
			{
				this.logQueue.Enqueue(new LogExceptionMsg(exception, context));
				this.enabled = true; // Preventing some idiot from blowing up the heap by shutting us down.
			}
		}

		void UnityEngine.ILogHandler.LogFormat(UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] formatArgs)
		{
			lock (MUTEX)
			{
				this.logQueue.Enqueue(new LogFormatMsg(logType, context, format, formatArgs));
				this.enabled = true; // Preventing some idiot from blowing up the heap by shutting us down.
			}
		}

		internal void LateUpdate()
		{
			if (0 == this.logQueue.Count) return; // Fast bail out if there's nothing on the queue. False negatives will be handled on the next frame.
			lock (MUTEX) // Ugly doing it on every frame, but there's no other safe way.
			{
				if (0 == this.logQueue.Count)			// This one is safe, we are inside the critical section. No false negatives possible.
				{
					this.enabled = false;				// Being that idiot I mentioned above and saving some CPU cycles by deactivating this and,
					return;								// so, preventingn the LateUpdate from being called when we know for sure there's nothing to be logged.
				}
				if (this.logQueue.Count > 128)				// If we have so many messages since the last frame, boy, we have a problem here...
					while (this.logQueue.Count > 1)			// The frame rate is the least of the user's worries by now, flush the buffer to prevent exploding the heap.
						this.logQueue.Dequeue().execute();	// as well to prevent losing any message that could help on the diagnosing.
				this.logQueue.Dequeue().execute();
			}
		}

		internal void OnDestroy()
		{
			this.enabled = false;
			while (this.logQueue.Count > 0)
				this.logQueue.Dequeue().execute();
		}
	}
}
