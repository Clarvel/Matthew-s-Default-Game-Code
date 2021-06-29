using UnityEngine;

namespace SoulFlare.Extensions {
	public static class InputExtensions {
		/// <summary>
		/// Only returns true if its a keypress. No mouse!
		/// </summary>
		/// <returns></returns>
		public static bool AnyKeyDown() {
			return Input.anyKeyDown && !(AnyMouseDown() || Input.GetKeyDown(KeyCode.Escape));
		}

		/// <summary>
		/// Returns true if a mouse button was pressed
		/// </summary>
		/// <returns></returns>
		public static bool AnyMouseDown() {
			return Input.GetKeyDown(KeyCode.Mouse0) ||
					Input.GetKeyDown(KeyCode.Mouse1) ||
					Input.GetKeyDown(KeyCode.Mouse2) ||
					Input.GetKeyDown(KeyCode.Mouse3) ||
					Input.GetKeyDown(KeyCode.Mouse4) ||
					Input.GetKeyDown(KeyCode.Mouse5) ||
					Input.GetKeyDown(KeyCode.Mouse6);
		}

		/// <summary>
		/// Only returns true if its a keypress. No mouse!
		/// </summary>
		/// <returns></returns>
		public static bool AnyKey() {
			return Input.anyKey && !(AnyMouse() || Input.GetKey(KeyCode.Escape));
		}

		/// <summary>
		/// Returns true if a mouse button was pressed
		/// </summary>
		/// <returns></returns>
		public static bool AnyMouse() {
			return Input.GetKey(KeyCode.Mouse0) ||
					Input.GetKey(KeyCode.Mouse1) ||
					Input.GetKey(KeyCode.Mouse2) ||
					Input.GetKey(KeyCode.Mouse3) ||
					Input.GetKey(KeyCode.Mouse4) ||
					Input.GetKey(KeyCode.Mouse5) ||
					Input.GetKey(KeyCode.Mouse6);
		}
	}
}