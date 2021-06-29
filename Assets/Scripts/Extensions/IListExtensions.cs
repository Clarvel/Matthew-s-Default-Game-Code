using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace SoulFlare.Extensions.Collections {
	public static class IListExtensions {
		/// <summary>
		/// Randomly shuffles the list in place via Fisher-Yates algorithm
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		public static void Shuffle<T>(this IList<T> list) {
			/// <summary>
			/// Shuffles the element list in place
			/// </summary>
			for(int a = list.Count; a > 1; a--) {
				list._Swap(a - 1, Random.Range(0, a));
			}
		}

		/// <summary>
		/// Swaps the values at <param name="a"></param> and <param name="b"></param>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		public static void Swap<T>(this IList<T> list, int a, int b) {
			/// <summary>
			/// Swaps the values at the 2 indexes
			/// </summary>
			if(a < 0 || b < 0 || a >= list.Count || b >= list.Count) { throw new System.IndexOutOfRangeException(); }
			_Swap(list, a, b);
		}

		/// <summary>
		/// Returns a prettily formatted string representation of the list
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns>A prettily formatted string representation</returns>
		public static string ToString<T>(this IList<T> list, bool separateViaNewline = false) {
			if(list.Count == 0) { return ""; }
			string sep = separateViaNewline ? "\n" : ", ";
			StringBuilder sb = new StringBuilder();
			for(int a = 0; a < list.Count; a++) {
				sb.Append(sep + list[a].ToString());
			}
			return sb.ToString();
		}

		/// <summary>
		/// Returns a random item from the <paramref name="list"/>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static T GetRandom<T>(this IList<T> list) {
			return list[Random.Range(0, list.Count)];
		}

		static void _Swap<T>(this IList<T> list, int a, int b) {
			T tmp = list[a];
			list[a] = list[b];
			list[b] = tmp;
		}
	}
}
