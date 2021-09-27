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
using System.Collections;
using System.Collections.Generic;

namespace KSPe.System.Collections.Generic
{
	public class Queue<T> : IEnumerable<T>, ICollection
	{
		private readonly List<T> queue;

		public Queue()
		{
			this.queue = new List<T>();
		}

		public Queue(Int32 size)
		{
			this.queue = new List<T>(size);
		}

		public Queue(IEnumerable<T> enumerable)
		{
			this.queue = new List<T>(enumerable);
		}

		public int Count => this.queue.Count;
		int ICollection.Count => this.queue.Count;
		bool ICollection.IsSynchronized => false;
		object ICollection.SyncRoot => this.queue;

		void ICollection.CopyTo(Array array, int index) { this.queue.CopyTo((T[])array, index); }
		IEnumerator IEnumerable.GetEnumerator()			{ return this.queue.GetEnumerator(); }
		IEnumerator<T> IEnumerable<T>.GetEnumerator()	{ return this.queue.GetEnumerator(); }

		public void Clear()				{ this.queue.Clear();	}
		public bool Contains(T item)	{ return this.queue.Contains(item); }

		public T Dequeue()
		{
			T r = this.queue[0];
			this.queue.RemoveAt(0);
			return r;
		}

		public void Enqueue(T item)		{ this.queue.Add(item); }
		public T Peek()					{ return this.queue[0]; }
		public T[] ToArray()			{ return this.queue.ToArray(); }
		public void TrimExcess()		{ this.queue.TrimExcess(); }

		public bool TryDequeue(out T result)
		{
			if (queue.Count < 1)
			{
				result = default;
				return false;
			}
			result = this.Dequeue();
			return true;
		}

		public bool TryPeek(out T result)
		{
			if (queue.Count < 1)
			{
				result = default;
				return false;
			}
			result = this.Peek();
			return true;
		}
	}
}

