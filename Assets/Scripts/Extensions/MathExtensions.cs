using UnityEngine;

namespace SoulFlare.Extensions {
	public static class MathExtensions {
		/// <summary>
		/// Normalizes a degree value to the range 0-360
		/// </summary>
		/// <param name="degree"></param>
		/// <returns></returns>
		public static float NormalizeDegree(float degree) {
			return Mod(degree, 360f);
		}

		/// <summary>
		/// Returns the positive modulo, not remainder like % does
		/// </summary>
		/// <param name="value"></param>
		/// <param name="mod"></param>
		/// <returns></returns>
		public static float Mod(float value, float mod) {
			if(mod <= 0f) { throw new System.ArgumentOutOfRangeException("Modulo cannot be 0 or less"); }
			float v = value % mod;
			return v >= 0 ? v : (v + mod);
		}

		/// <summary>
		/// Returns the positive modulo, not remainder like % does
		/// </summary>
		/// <param name="value"></param>
		/// <param name="mod"></param>
		/// <returns></returns>
		public static float Mod(int value, uint mod) {
			if(mod <= 0) { throw new System.ArgumentOutOfRangeException("Modulo cannot be 0 or less"); }
			float v = value % mod;
			return v >= 0 ? v : (v + mod);
		}

		/// <summary>
		/// Compares 2 values, returns true if the comparison is less than <paramref name="distance"/>
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="distance"></param>
		/// <returns></returns>
		public static bool RangedApproximately(float a, float b, float distance) {
			return Mathf.Abs(b - a) < distance;
		}
	}
}
