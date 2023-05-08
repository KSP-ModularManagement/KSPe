/*
	This file is part of KSPe, a component for KSP Enhanced /L
	unless when specified otherwise below this code is:
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
namespace KSPe.Util
{
	public abstract class AbstractException : System.Exception
	{
		private readonly object[] parms;

		public Action lambda { get; set; }
		public string actionMessage { get; set; }
		public string customPreamble { get; set; }
		public string customEpilogue { get; set; }
		public string offendedName { get; set; }

		protected AbstractException(string message, params object[] @params):base(message)
		{
			this.parms = @params;
			this.lambda = null;
			this.actionMessage = null;
			this.customPreamble = null;
			this.customEpilogue = null;
		}

		public override string Message => this.ToString();

		public virtual string ToShortMessage()
		{
			return this.ToString();
		}

		public virtual string ToLongMessage()
		{
			return this.ToString();
		}

		public override string ToString()
		{
			return string.Format(base.Message,  this.parms);
		}
	}
}
