/*
	This file is part of KSPe, a component for KSP Enhanced /L
	unless when specified otherwise below this code is:
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
namespace KSPe.Util
{
	public static class Create
	{
		public static class Vector2
		{ 
			public static UnityEngine.Vector2 from(int[] array) => new UnityEngine.Vector2(array[0], array[1]);
			public static UnityEngine.Vector2 from(long[] array) => new UnityEngine.Vector2(array[0], array[1]);
			public static UnityEngine.Vector2 from(float[] array) => new UnityEngine.Vector2(array[0], array[1]);
			public static UnityEngine.Vector2 from(double[] array) => new UnityEngine.Vector2((float)array[0], (float)array[1]);
			public static UnityEngine.Vector2 from(UnityEngine.Vector2 v) => new UnityEngine.Vector2(v.x, v.y);
			public static UnityEngine.Vector2 from(UnityEngine.Vector2d v) => new UnityEngine.Vector2((float)v.x, (float)v.y);
		}
		public static class Vector2d
		{ 
			public static UnityEngine.Vector2d from(int[] array) => new UnityEngine.Vector2d(array[0], array[1]);
			public static UnityEngine.Vector2d from(long[] array) => new UnityEngine.Vector2d(array[0], array[1]);
			public static UnityEngine.Vector2d from(float[] array) => new UnityEngine.Vector2d(array[0], array[1]);
			public static UnityEngine.Vector2d from(double[] array) => new UnityEngine.Vector2d(array[0], array[1]);
			public static UnityEngine.Vector2d from(UnityEngine.Vector2 v) => new UnityEngine.Vector2d(v.x, v.y);
			public static UnityEngine.Vector2d from(UnityEngine.Vector2d v) => new UnityEngine.Vector2d(v.x, v.y);
		}
		public static class Vector3
		{ 
			public static UnityEngine.Vector3 from(int[] array) => new UnityEngine.Vector3(array[0], array[1], array[2]);
			public static UnityEngine.Vector3 from(long[] array) => new UnityEngine.Vector3(array[0], array[1], array[2]);
			public static UnityEngine.Vector3 from(float[] array) => new UnityEngine.Vector3(array[0], array[1], array[2]);
			public static UnityEngine.Vector3 from(double[] array) => new UnityEngine.Vector3((float)array[0], (float)array[1], (float)array[2]);
			public static UnityEngine.Vector3 from(UnityEngine.Vector3 v) => new UnityEngine.Vector3(v.x, v.y, v.z);
			public static UnityEngine.Vector3 from(global::Vector3d v) => new UnityEngine.Vector3((float)v.x, (float)v.y, (float)v.z);
		}
		public static class Vector3d
		{ 
			public static global::Vector3d from(int[] array) => new global::Vector3d(array[0], array[1], array[2]);
			public static global::Vector3d from(long[] array) => new global::Vector3d(array[0], array[1], array[2]);
			public static global::Vector3d from(float[] array) => new global::Vector3d(array[0], array[1], array[2]);
			public static global::Vector3d from(double[] array) => new global::Vector3d(array[0], array[1], array[2]);
			public static global::Vector3d from(UnityEngine.Vector3 v) => new global::Vector3d(v.x, v.y, v.z);
			public static global::Vector3d from(global::Vector3d v) => new global::Vector3d(v.x, v.y, v.z);
		}
		public static class Vector4
		{ 
			public static UnityEngine.Vector4 from(int[] array) => new UnityEngine.Vector4(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Vector4 from(long[] array) => new UnityEngine.Vector4(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Vector4 from(float[] array) => new UnityEngine.Vector4(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Vector4 from(double[] array) => new UnityEngine.Vector4((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
			public static UnityEngine.Vector4 from(UnityEngine.Vector4 v) => new UnityEngine.Vector4(v.x, v.y, v.z, v.w);
			public static UnityEngine.Vector4 from(UnityEngine.Vector4d v) => new UnityEngine.Vector4((float)v.x, (float)v.y, (float)v.z, (float)v.w);
			public static UnityEngine.Vector4 from(UnityEngine.Quaternion v) => new UnityEngine.Vector4(v.x, v.y, v.z, v.w);
			public static UnityEngine.Vector4 from(UnityEngine.QuaternionD v) => new UnityEngine.Vector4((float)v.x, (float)v.y, (float)v.z, (float)v.w);
		}
		public static class Vector4d
		{ 
			public static UnityEngine.Vector4d from(int[] array) => new UnityEngine.Vector4d(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Vector4d from(long[] array) => new UnityEngine.Vector4d(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Vector4d from(float[] array) => new UnityEngine.Vector4d(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Vector4d from(double[] array) => new UnityEngine.Vector4d(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Vector4d from(UnityEngine.Vector4 v) => new UnityEngine.Vector4d(v.x, v.y, v.z, v.w);
			public static UnityEngine.Vector4d from(UnityEngine.Vector4d v) => new UnityEngine.Vector4d(v.x, v.y, v.z, v.w);
			public static UnityEngine.Vector4d from(UnityEngine.Quaternion v) => new UnityEngine.Vector4d(v.x, v.y, v.z, v.w);
			public static UnityEngine.Vector4d from(UnityEngine.QuaternionD v) => new UnityEngine.Vector4d(v.x, v.y, v.z, v.w);
		}
		public static class Quaternion
		{ 
			public static UnityEngine.Quaternion from(int[] array) => new UnityEngine.Quaternion(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Quaternion from(long[] array) => new UnityEngine.Quaternion(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Quaternion from(float[] array) => new UnityEngine.Quaternion(array[0], array[1], array[2], array[3]);
			public static UnityEngine.Quaternion from(double[] array) => new UnityEngine.Quaternion((float)array[0], (float)array[1], (float)array[2], (float)array[3]);
			public static UnityEngine.Quaternion from(UnityEngine.Vector4 v) => new UnityEngine.Quaternion(v.x, v.y, v.z, v.w);
			public static UnityEngine.Quaternion from(UnityEngine.Vector4d v) => new UnityEngine.Quaternion((float)v.x, (float)v.y, (float)v.z, (float)v.w);
			public static UnityEngine.Quaternion from(UnityEngine.Quaternion v) => new UnityEngine.Quaternion(v.x, v.y, v.z, v.w);
			public static UnityEngine.Quaternion from(UnityEngine.QuaternionD v) => new UnityEngine.Quaternion((float)v.x, (float)v.y, (float)v.z, (float)v.w);
		}
		public static class QuaternionD
		{ 
			public static UnityEngine.QuaternionD from(int[] array) => new UnityEngine.QuaternionD(array[0], array[1], array[2], array[3]);
			public static UnityEngine.QuaternionD from(long[] array) => new UnityEngine.QuaternionD(array[0], array[1], array[2], array[3]);
			public static UnityEngine.QuaternionD from(float[] array) => new UnityEngine.QuaternionD(array[0], array[1], array[2], array[3]);
			public static UnityEngine.QuaternionD from(double[] array) => new UnityEngine.QuaternionD(array[0], array[1], array[2], array[3]);
			public static UnityEngine.QuaternionD from(UnityEngine.Vector4 v) => new UnityEngine.QuaternionD(v.x, v.y, v.z, v.w);
			public static UnityEngine.QuaternionD from(UnityEngine.Vector4d v) => new UnityEngine.QuaternionD(v.x, v.y, v.z, v.w);
			public static UnityEngine.QuaternionD from(UnityEngine.Quaternion v) => new UnityEngine.QuaternionD(v.x, v.y, v.z, v.w);
			public static UnityEngine.QuaternionD from(UnityEngine.QuaternionD v) => new UnityEngine.QuaternionD(v.x, v.y, v.z, v.w);
		}
	}
}
