using UnityEngine;

namespace SoulFlare.Spawners {

	public class PointSpawner : Spawner {
		public Transform point;

		void Awake() {
			SetPoint(point);
		}

		protected override void SetTransform(GameObject go) {
			go.transform.SetPositionAndRotation(point.position, point.rotation);
		}

		public void SetPoint(Transform t) {
			point = t ?? transform;
		}
	}
}
