using UnityEngine;

namespace SoulFlare.Singleton { 
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{
		static T instance;
		static object lockObject = new object();
		static bool appIsQuitting = false;

		public static T Instance {
			get {
				if(appIsQuitting) {
					Debug.LogWarning("[Singleton] Instance \"" + typeof(T) + "\" already destroyed on application quit.");
					return null;
				}
				lock(lockObject) {
					if(instance == null) {
						T[] instances = FindObjectsOfType<T>();
						if(instances.Length == 0) {
							GameObject singleton = new GameObject("[Singleton " + typeof(T) + "]");
							instance = singleton.AddComponent<T>();
							DontDestroyOnLoad(singleton);
						} else {
							if(instances.Length > 1) {
								throw new System.Exception("[Singleton] Discovered multiple Singleton \"" + typeof(T) + "\" instances.");
							}
							instance = instances[0];
						}
					}
					return instance;
				}
			}
		}

		void OnDestroy() {
			appIsQuitting = true;
		}
	}
}
