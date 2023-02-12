/*
	This file is part of KSPe.HMI, a component for KSP Enhanced /L
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
/*
	Based on previous work by Remi Gillig, https://github.com/speps/XInputDotNet,
	lincensed under	The MIT License

	Copyright (c) 2009 Remi Gillig <remigillig@gmail.com>
*/
namespace KSPe.HMI.Multiplatform.XInput.GamePad
{
	public struct Buttons
	{
		ButtonState start, back, leftStick, rightStick, leftShoulder, rightShoulder, guide, a, b, x, y;

		internal Buttons(ButtonState start, ButtonState back, ButtonState leftStick, ButtonState rightStick,
								ButtonState leftShoulder, ButtonState rightShoulder, ButtonState guide,
								ButtonState a, ButtonState b, ButtonState x, ButtonState y)
		{
			this.start = start;
			this.back = back;
			this.leftStick = leftStick;
			this.rightStick = rightStick;
			this.leftShoulder = leftShoulder;
			this.rightShoulder = rightShoulder;
			this.guide = guide;
			this.a = a;
			this.b = b;
			this.x = x;
			this.y = y;
		}

		public ButtonState Start
		{
			get { return start; }
		}

		public ButtonState Back
		{
			get { return back; }
		}

		public ButtonState LeftStick
		{
			get { return leftStick; }
		}

		public ButtonState RightStick
		{
			get { return rightStick; }
		}

		public ButtonState LeftShoulder
		{
			get { return leftShoulder; }
		}

		public ButtonState RightShoulder
		{
			get { return rightShoulder; }
		}

		public ButtonState Guide
		{
			get { return guide; }
		}

		public ButtonState A
		{
			get { return a; }
		}

		public ButtonState B
		{
			get { return b; }
		}

		public ButtonState X
		{
			get { return x; }
		}

		public ButtonState Y
		{
			get { return y; }
		}
	}
}
