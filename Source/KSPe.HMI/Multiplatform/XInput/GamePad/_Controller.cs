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
/*
	Based on previous work by Remi Gillig, https://github.com/speps/XInputDotNet,
	lincensed under	The MIT License

	Copyright (c) 2009 Remi Gillig <remigillig@gmail.com>
*/
using DLLIFC = KSPe.HMI.Multiplatform.XInput.dlls.Ifc;

namespace KSPe.HMI.Multiplatform.XInput.GamePad
{
	public static class Controller
	{
		private static DLLIFC DLL;
		static Controller()
		{
			// TODO: Code to select the appropriate native access
			//DLL = new dlls.X86_64();
			//DLL = new dlls.X86();
			//DLL = new dlls.CIL();
			DLL = new dlls.NULL();	// Fallback.
		}

		public static State GetState(PlayerIndex playerIndex)
		{
			return GetState(playerIndex, DeadZone.IndependentAxes);
		}

		public static State GetState(PlayerIndex playerIndex, DeadZone deadZone)
		{
			State.RawState state;
			uint result = DLL.XInputGamePadGetState((uint)playerIndex, out state);
			return new State(result == Utils.Success, state, deadZone);
		}

		public static void SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			DLL.XInputGamePadSetState((uint)playerIndex, leftMotor, rightMotor);
		}
	}
}
