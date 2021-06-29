using System.Collections.Generic;
using System;
using System.Linq;

namespace SoulFlare.Extensions {
	public static class IEnumExtensions {
		public static T[] GetEnumArray<T>() {
			return (T[])GetBaseEnumArray<T>();
		}

		public static IEnumerable<T> GetEnumValues<T>() {
			return GetBaseEnumArray<T>().Cast<T>();
		}

		private static Array GetBaseEnumArray<T>() {
			if(typeof(T).BaseType != typeof(Enum)) {
				throw new Exception("T must be of type System.Enum");
			}
			return Enum.GetValues(typeof(T));
		}
	}
}