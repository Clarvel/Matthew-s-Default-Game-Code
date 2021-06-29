using System;

namespace SoulFlare.Extensions {
	public static class TimeExtensions {
		public static string StopwatchFormat(float time) {
			int minutes = (int)(time / 60);
			int seconds = (int)(time % 60);
			int millis = (int)((time * 1000) % 1000);
			return String.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, millis);
		}
	}
}
