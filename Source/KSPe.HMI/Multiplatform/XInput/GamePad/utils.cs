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
/*
	Based on previous work by Remi Gillig, https://github.com/speps/XInputDotNet,
	lincensed under	The MIT License

	Copyright (c) 2009 Remi Gillig <remigillig@gmail.com>
*/
using System;

namespace KSPe.HMI.Multiplatform.XInput.GamePad
{
	public static class Utils
	{
		public const uint Success = 0x000;
		public const uint NotConnected = 0x000;

		private const int LeftStickDeadZone = 7849;
		private const int RightStickDeadZone = 8689;
		private const int TriggerDeadZone = 30;

		public static float ApplyTriggerDeadZone(byte value, DeadZone deadZoneMode)
		{
			if (deadZoneMode == DeadZone.None)
			{
				return ApplyDeadZone(value, byte.MaxValue, 0.0f);
			}
			else
			{
				return ApplyDeadZone(value, byte.MaxValue, TriggerDeadZone);
			}
		}

		public static ThumbSticks.StickValue ApplyLeftStickDeadZone(short valueX, short valueY, DeadZone deadZoneMode)
		{
			return ApplyStickDeadZone(valueX, valueY, deadZoneMode, LeftStickDeadZone);
		}

		public static ThumbSticks.StickValue ApplyRightStickDeadZone(short valueX, short valueY, DeadZone deadZoneMode)
		{
			return ApplyStickDeadZone(valueX, valueY, deadZoneMode, RightStickDeadZone);
		}

		private static ThumbSticks.StickValue ApplyStickDeadZone(short valueX, short valueY, DeadZone deadZoneMode, int deadZoneSize)
		{
			if (deadZoneMode == DeadZone.Circular)
			{
				// Cast to long to avoid int overflow if valueX and valueY are both 32768, which would result in a negative number and Sqrt returns NaN
				float distanceFromCenter = (float)Math.Sqrt((long)valueX * (long)valueX + (long)valueY * (long)valueY);
				float coefficient = ApplyDeadZone(distanceFromCenter, short.MaxValue, deadZoneSize);
				coefficient = coefficient > 0.0f ? coefficient / distanceFromCenter : 0.0f;
				return new ThumbSticks.StickValue(
					Clamp(valueX * coefficient),
					Clamp(valueY * coefficient)
				);
			}
			else if (deadZoneMode == DeadZone.IndependentAxes)
			{
				return new ThumbSticks.StickValue(
					ApplyDeadZone(valueX, short.MaxValue, deadZoneSize),
					ApplyDeadZone(valueY, short.MaxValue, deadZoneSize)
				);
			}
			else
			{
				return new ThumbSticks.StickValue(
					ApplyDeadZone(valueX, short.MaxValue, 0.0f),
					ApplyDeadZone(valueY, short.MaxValue, 0.0f)
				);
			}
		}

		private static float Clamp(float value)
		{
			return value < -1.0f ? -1.0f : (value > 1.0f ? 1.0f : value);
		}

		private static float ApplyDeadZone(float value, float maxValue, float deadZoneSize)
		{
			if (value < -deadZoneSize)
			{
				value += deadZoneSize;
			}
			else if (value > deadZoneSize)
			{
				value -= deadZoneSize;
			}
			else
			{
				return 0.0f;
			}

			value /= maxValue - deadZoneSize;

			return Clamp(value);
		}
	}
}
