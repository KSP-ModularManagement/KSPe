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
namespace KSPe.HMI.Multiplatform.XInput.GamePad
{
	public struct ThumbSticks
	{
		public struct StickValue
		{
			float x, y;

			internal StickValue(float x, float y)
			{
				this.x = x;
				this.y = y;
			}

			public float X
			{
				get { return x; }
			}

			public float Y
			{
				get { return y; }
			}
		}

		StickValue left, right;

		internal ThumbSticks(StickValue left, StickValue right)
		{
			this.left = left;
			this.right = right;
		}

		public StickValue Left
		{
			get { return left; }
		}

		public StickValue Right
		{
			get { return right; }
		}
	}

}
