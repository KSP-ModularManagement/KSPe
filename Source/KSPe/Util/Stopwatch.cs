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
namespace KSPe.Util
{
	public class Stopwatch
	{
		public double Value { get; private set;}
		public bool IsRunning => null != this.timer && this.timer.IsRunning;
		public bool IsValid => null == this.timer && this.Value >= 0.0f;
		private System.Diagnostics.Stopwatch timer = null;

		public Stopwatch()
		{
			this.Value = -1;
		}

		public void Start()
		{
			if (this.IsRunning) return;
			this.Value = -1;
			this.timer = new System.Diagnostics.Stopwatch();
			this.timer.Start();
		}

		public void Stop()
		{
			if (null == this.timer) return;
			this.timer.Stop();
			this.Value = (this.timer.ElapsedMilliseconds) / 1000.0f;
			this.timer = null;
		}

		public static implicit operator float(Stopwatch o) => (float)o.Value;
		public static implicit operator double(Stopwatch o) => o.Value;
		public static implicit operator string(Stopwatch o) => o.Value.ToString("F3") + "s";
	}
}
