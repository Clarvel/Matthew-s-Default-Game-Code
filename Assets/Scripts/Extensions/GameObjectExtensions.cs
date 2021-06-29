using UnityEngine;

namespace SoulFlare.Extensions {
	public static class GameObjectExtensions {
		/// <summary>
		/// Moves <paramref name="transform"/> and all its children to the specified <paramref name="layer"/> recursively
		/// </summary>
		/// <param name="transform"></param>
		/// <param name="layer"></param>
		public static void MoveToLayer(this Transform transform, int layer) {
			transform.gameObject.layer = layer;
			foreach(Transform child in transform) {
				child.MoveToLayer(layer);
			}
		}

		/// <summary>
		/// Moves the <paramref name="gameObject"/> to the specified <paramref name="layer"/> recursively
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="layer"></param>
		public static void MoveToLayer(this GameObject gameObject, int layer) {
			gameObject.transform.MoveToLayer(layer);
		}

		/// <summary>
		/// Returns true if <paramref name="gameObject"/>'s layer is in <paramref name="layerMask"/>
		/// </summary>
		/// <param name="gameObject"></param>
		/// <param name="layerMask"></param>
		/// <returns></returns>
		public static bool IsInLayerMask(this GameObject gameObject, LayerMask layerMask) {
			return ((layerMask.value & (1 << gameObject.layer)) > 0);
		}

		/// <summary>
		/// Returns the attached <typeparamref name="T"/> if it exists, otherwise it is created and returned
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component {
			return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
		}

		/// <summary>
		/// <paramref name="gameObject"/>.Getcomponent<<typeparamref name="T"/>>() != null
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="gameObject"></param>
		/// <returns></returns>
		public static bool HasComponent<T>(this GameObject gameObject) where T : Component {
			return gameObject.GetComponent<T>() != null;
		}
	}
}
