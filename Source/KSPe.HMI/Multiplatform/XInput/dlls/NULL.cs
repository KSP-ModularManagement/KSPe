/*
	This file is part of KSPe.HMI, a component for KSP Enhanced /L
		© 2018-2024 LisiasT : http://lisias.net <support@lisias.net>

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
using GamePadState = KSPe.HMI.Multiplatform.XInput.GamePad.State;

namespace KSPe.HMI.Multiplatform.XInput.dlls
{
	internal class NULL : Ifc
	{
		private static readonly GamePadState.RawState dummy;
		uint Ifc.XInputGamePadGetState(uint playerIndex, out GamePadState.RawState state)
		{
			state = dummy;
			return 1167; // ERROR_DEVICE_NOT_CONNECTED
		}

		void Ifc.XInputGamePadSetState(uint playerIndex, float leftMotor, float rightMotor) {}
	}
}
