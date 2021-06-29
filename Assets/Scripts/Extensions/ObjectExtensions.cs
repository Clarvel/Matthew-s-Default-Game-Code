namespace SoulFlare.Extensions {
	public static class ObjectExtensions {
		public static void NullCoalesce<T>(ref T obj, T val){
			if(obj == null) {
				obj = val;
			}
		}
	}
}