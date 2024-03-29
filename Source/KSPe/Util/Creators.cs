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
		public static class Matrix4x4
		{ 
			public static UnityEngine.Matrix4x4 from(int[] array)
			{
				UnityEngine.Matrix4x4 r = new UnityEngine.Matrix4x4();
				for (int i = 0; i < array.Length; ++i) r[i] = array[i];
				return r;
			}

			public static UnityEngine.Matrix4x4 from(long[] array)
			{
				UnityEngine.Matrix4x4 r = new UnityEngine.Matrix4x4();
				for (int i = 0; i < array.Length; ++i) r[i] = array[i];
				return r;
			}

			public static UnityEngine.Matrix4x4 from(float[] array)
			{
				UnityEngine.Matrix4x4 r = new UnityEngine.Matrix4x4();
				for (int i = 0; i < array.Length; ++i) r[i] = array[i];
				return r;
			}

			public static UnityEngine.Matrix4x4 from(double[] array)
			{
				UnityEngine.Matrix4x4 r = new UnityEngine.Matrix4x4();
				for (int i = 0; i < array.Length; ++i) r[i] = (float)array[i];
				return r;
			}

			public static UnityEngine.Matrix4x4 from(UnityEngine.Matrix4x4 v)
			{
				UnityEngine.Matrix4x4 r = new UnityEngine.Matrix4x4();
				for (int i = 0; i < 16; ++i) r[i] = v[i];
				return r;
			}

			public static UnityEngine.Matrix4x4 from(global::Matrix4x4D v)
			{
				UnityEngine.Matrix4x4 r = new UnityEngine.Matrix4x4();
				int i = 0;
				r[i++] = (float)v.m00;
				r[i++] = (float)v.m01;
				r[i++] = (float)v.m02;
				r[i++] = (float)v.m03;
				r[i++] = (float)v.m10;
				r[i++] = (float)v.m11;
				r[i++] = (float)v.m12;
				r[i++] = (float)v.m13;
				r[i++] = (float)v.m20;
				r[i++] = (float)v.m21;
				r[i++] = (float)v.m22;
				r[i++] = (float)v.m23;
				r[i++] = (float)v.m30;
				r[i++] = (float)v.m31;
				r[i++] = (float)v.m32;
				r[i++] = (float)v.m33;
				return r;
			}
		}
		public static class Matrix4x4d
		{ 
			public static global::Matrix4x4D from(int[] array)
			{
				global::Matrix4x4D r = new global::Matrix4x4D();
				int i = 0;
				r.m00 = array[i++];
				r.m01 = array[i++];
				r.m02 = array[i++];
				r.m03 = array[i++];
				r.m10 = array[i++];
				r.m11 = array[i++];
				r.m12 = array[i++];
				r.m13 = array[i++];
				r.m20 = array[i++];
				r.m21 = array[i++];
				r.m22 = array[i++];
				r.m23 = array[i++];
				r.m30 = array[i++];
				r.m31 = array[i++];
				r.m32 = array[i++];
				r.m33 = array[i++];
				return r;
			}

			public static global::Matrix4x4D from(long[] array)
			{
				global::Matrix4x4D r = new global::Matrix4x4D();
				int i = 0;
				r.m00 = array[i++];
				r.m01 = array[i++];
				r.m02 = array[i++];
				r.m03 = array[i++];
				r.m10 = array[i++];
				r.m11 = array[i++];
				r.m12 = array[i++];
				r.m13 = array[i++];
				r.m20 = array[i++];
				r.m21 = array[i++];
				r.m22 = array[i++];
				r.m23 = array[i++];
				r.m30 = array[i++];
				r.m31 = array[i++];
				r.m32 = array[i++];
				r.m33 = array[i++];
				return r;
			}

			public static global::Matrix4x4D from(float[] array)
			{
				global::Matrix4x4D r = new global::Matrix4x4D();
				int i = 0;
				r.m00 = array[i++];
				r.m01 = array[i++];
				r.m02 = array[i++];
				r.m03 = array[i++];
				r.m10 = array[i++];
				r.m11 = array[i++];
				r.m12 = array[i++];
				r.m13 = array[i++];
				r.m20 = array[i++];
				r.m21 = array[i++];
				r.m22 = array[i++];
				r.m23 = array[i++];
				r.m30 = array[i++];
				r.m31 = array[i++];
				r.m32 = array[i++];
				r.m33 = array[i++];
				return r;
			}

			public static global::Matrix4x4D from(double[] array)
			{
				global::Matrix4x4D r = new global::Matrix4x4D();
				int i = 0;
				r.m00 = array[i++];
				r.m01 = array[i++];
				r.m02 = array[i++];
				r.m03 = array[i++];
				r.m10 = array[i++];
				r.m11 = array[i++];
				r.m12 = array[i++];
				r.m13 = array[i++];
				r.m20 = array[i++];
				r.m21 = array[i++];
				r.m22 = array[i++];
				r.m23 = array[i++];
				r.m30 = array[i++];
				r.m31 = array[i++];
				r.m32 = array[i++];
				r.m33 = array[i++];
				return r;
			}

			public static global::Matrix4x4D from(global::Matrix4x4D v)
			{
				global::Matrix4x4D r = new global::Matrix4x4D();
				r.m00 = (float)v.m00;
				r.m01 = (float)v.m01;
				r.m02 = (float)v.m02;
				r.m03 = (float)v.m03;
				r.m10 = (float)v.m10;
				r.m11 = (float)v.m11;
				r.m12 = (float)v.m12;
				r.m13 = (float)v.m13;
				r.m20 = (float)v.m20;
				r.m21 = (float)v.m21;
				r.m22 = (float)v.m22;
				r.m23 = (float)v.m23;
				r.m30 = (float)v.m30;
				r.m31 = (float)v.m31;
				r.m32 = (float)v.m32;
				r.m33 = (float)v.m33;
				return r;
			}
		}
	}
}
