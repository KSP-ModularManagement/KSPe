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
namespace KSPe.Util.TextProcessing
{
	// Rules at: https://www.grammarbook.com/numbers/numbers.asp
	public static class Number
	{
		public static class To
		{
			// Based on previous work: https://www.c-sharpcorner.com/blogs/convert-number-to-words-in-c-sharp
			// Updated to follow rules above mentioned.
			private readonly static String[] units = {
					"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
					, "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
				};
			private readonly static String[] tens = {
					"", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
				};

			public static String Text(double amount, bool startUpcase=false, string zero = null)
			{
				try
				{
					long amount_int = (long)amount;
					long amount_dec = (long)Math.Round((amount - (double)(amount_int)) * 100);
					string r = (0 == amount_int && 0 == amount_dec)
						? Text(0, startUpcase, zero)
						: Text(amount_int) + (amount_dec == 0 ? "" : " point " + Text(amount_dec));
					return startUpcase ? r[0].ToString().ToUpper() + r.Substring(1) : r;
				}
				catch (Exception e)
				{
					KSPe.Log.error(e, "Error \"{0}\" while converting value {1}", e.Message, amount);
				}
				return "NaN";
			}

			public static String Text(long i, bool startUpcase=false, string zero = null)
			{
				string r;
				if (0 == i)					r = zero??units[0];
				else if (i < 20)			r = units[i];
				else if (i < 100)			r = tens[i / 10] + ((i % 10 > 0) ? "-" + Text(i % 10) : "");
				else if (i < 1000)			r = units[i / 100] + " hundred" + ((i % 100 > 0) ? " and " + Text(i % 100) : "");
				else if (i < 1000000)		r = Text(i / 1000) + " thousand"  + ((i % 1000 > 0) ? ", " + Text(i % 1000) : "");
				else if (i < 1000000000)	r = Text(i / 1000000) + " million" + ((i % 1000000 > 0) ? ", " + Text(i % 1000000) : "");
				else if (i < 1000000000000)	r = Text(i / 1000000000) + " billion" + ((i % 1000000000 > 0) ? ", " + Text(i % 1000000000) : "");
				else						r = Text(i / 1000000000000) + " trillion" + ((i % 1000000000000 > 0) ? ", " + Text(i % 1000000000000) : "");
				return startUpcase ? r[0].ToString().ToUpper() + r.Substring(1) : r;
			}
		}
	}
}
