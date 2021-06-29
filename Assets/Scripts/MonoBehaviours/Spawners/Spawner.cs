using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SoulFlare.Extensions;
using SoulFlare.Extensions.Collections;

namespace SoulFlare.Spawners {

	[System.Serializable]
	public class SpawnEvent : UnityEvent<GameObject> { }

	public class Spawner : MonoBehaviour {
		[SerializeField] GameObject[] prefabs;

		public Vector2 minMaxSpawnDelay = new Vector2();

		[Tooltip("Should the spawner automatically set the instance's parent to itself")]
		public bool autoParentChild = true;
		[Tooltip("Should the spawner automatically set the instance's layer to the spawner's layer")]
		public bool autoLayerChild = false;
		[Tooltip("Should the spawner wait to spawn a new object until the old one is/are removed or inactive")]
		public bool spawnDiscrete = false;

		public SpawnEvent OnSpawnObject = new SpawnEvent();
		public UnityEvent OnStartSpawning = new UnityEvent();
		public UnityEvent OnStopSpawning = new UnityEvent();

		List<GameObject> instances = new List<GameObject>();
		float timer = float.PositiveInfinity;

		// Update is called once per frame
		void Update() {
			if(Time.time >= timer) {
				Spawn();
			}
		}

		public GameObject Spawn() {
			GameObject g = GetSpawnableObject();
			SetTransform(g);
			OnSpawnObject.Invoke(g);
			if(spawnDiscrete) {
				StopSpawning();
			} else {
				StartSpawning();
			}
			return g;
		}

		protected virtual void SetTransform(GameObject spawnObject) {}

		GameObject GetSpawnableObject() {
			GameObject g = null;
			int nullIndex = -1;
			for(int a = instances.Count - 1; a >= 0; a--) {
				g = instances[a];
				if(g == null) {
					nullIndex = a;
				}else if(!g.activeSelf) {
					g.SetActive(true);
					return g;
				}
			}
			g = MakeGameObject();
			if(nullIndex >= 0) {
				instances[nullIndex] = g;
			} else {
				instances.Add(g);
			}
			return g;
		}

		GameObject MakeGameObject() {
			GameObject g = Instantiate(prefabs.GetRandom());
			if(autoParentChild) {
				g.transform.SetParent(transform);
			}
			if(autoLayerChild) {
				g.MoveToLayer(gameObject.layer); // recursively move to proper layer
			}
			return g;
		}

		public bool HasActive() {
			foreach(GameObject a in instances) {
				if(a != null && a.activeSelf) {
					return true;
				}
			}
			return false;
		}

		public int ActiveCount() {
			int counter = 0;
			foreach(GameObject a in instances) {
				if(a != null && a.activeSelf) {
					counter++;
				}
			}
			return counter;
		}

		float GetDelayedSpawnTime() {
			return Time.time + Random.Range(minMaxSpawnDelay.x, minMaxSpawnDelay.y);
		}

		public void StartSpawning() {
			timer = GetDelayedSpawnTime();
		}

		public void StopSpawning() {
			timer = float.PositiveInfinity;
		}

		public void SetAllInactive() {
			foreach(GameObject a in instances) {
				if(a != null) {
					a.SetActive(false);
				}
			}
		}

		public void SpawnImmediate() { // public version of the spawn function
			Spawn();
		}

		public void DeleteAllInstances() {
			foreach(GameObject a in instances) {
				Destroy(a);
				instances.Remove(a);
			}
		}
	}
}
