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
/*
	Based on previous work by Remi Gillig, https://github.com/speps/XInputDotNet,
	lincensed under	The MIT License

	Copyright (c) 2009 Remi Gillig <remigillig@gmail.com>
*/
using System.Runtime.InteropServices;

namespace KSPe.HMI.Multiplatform.XInput.GamePad
{
	public struct State
	{
		[StructLayout(LayoutKind.Sequential)]
		internal struct RawState
		{
			public uint dwPacketNumber;
			public GamePad Gamepad;

			[StructLayout(LayoutKind.Sequential)]
			public struct GamePad
			{
				public ushort wButtons;
				public byte bLeftTrigger;
				public byte bRightTrigger;
				public short sThumbLX;
				public short sThumbLY;
				public short sThumbRX;
				public short sThumbRY;
			}
		}

		bool isConnected;
		uint packetNumber;
		Buttons buttons;
		DPad dPad;
		ThumbSticks thumbSticks;
		Triggers triggers;

		enum ButtonsConstants
		{
			DPadUp = 0x00000001,
			DPadDown = 0x00000002,
			DPadLeft = 0x00000004,
			DPadRight = 0x00000008,
			Start = 0x00000010,
			Back = 0x00000020,
			LeftThumb = 0x00000040,
			RightThumb = 0x00000080,
			LeftShoulder = 0x0100,
			RightShoulder = 0x0200,
			Guide = 0x0400,
			A = 0x1000,
			B = 0x2000,
			X = 0x4000,
			Y = 0x8000
		}

		internal State(bool isConnected, RawState rawState, DeadZone deadZone)
		{
			this.isConnected = isConnected;

			if (!isConnected)
			{
				rawState.dwPacketNumber = 0;
				rawState.Gamepad.wButtons = 0;
				rawState.Gamepad.bLeftTrigger = 0;
				rawState.Gamepad.bRightTrigger = 0;
				rawState.Gamepad.sThumbLX = 0;
				rawState.Gamepad.sThumbLY = 0;
				rawState.Gamepad.sThumbRX = 0;
				rawState.Gamepad.sThumbRY = 0;
			}

			packetNumber = rawState.dwPacketNumber;
			buttons = new Buttons(
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.Start) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.Back) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.LeftThumb) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.RightThumb) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.LeftShoulder) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.RightShoulder) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.Guide) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.A) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.B) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.X) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.Y) != 0 ? ButtonState.Pressed : ButtonState.Released
			);
			dPad = new DPad(
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.DPadUp) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.DPadDown) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.DPadLeft) != 0 ? ButtonState.Pressed : ButtonState.Released,
				(rawState.Gamepad.wButtons & (uint)ButtonsConstants.DPadRight) != 0 ? ButtonState.Pressed : ButtonState.Released
			);

			thumbSticks = new ThumbSticks(
				Utils.ApplyLeftStickDeadZone(rawState.Gamepad.sThumbLX, rawState.Gamepad.sThumbLY, deadZone),
				Utils.ApplyRightStickDeadZone(rawState.Gamepad.sThumbRX, rawState.Gamepad.sThumbRY, deadZone)
			);
			triggers = new Triggers(
				Utils.ApplyTriggerDeadZone(rawState.Gamepad.bLeftTrigger, deadZone),
				Utils.ApplyTriggerDeadZone(rawState.Gamepad.bRightTrigger, deadZone)
			);
		}

		public uint PacketNumber
		{
			get { return packetNumber; }
		}

		public bool IsConnected
		{
			get { return isConnected; }
		}

		public Buttons Buttons
		{
			get { return buttons; }
		}

		public DPad DPad
		{
			get { return dPad; }
		}

		public Triggers Triggers
		{
			get { return triggers; }
		}

		public ThumbSticks ThumbSticks
		{
			get { return thumbSticks; }
		}
	}
}
