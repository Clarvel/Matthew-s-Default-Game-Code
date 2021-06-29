using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulFlare.Spawners {

	public class SphereSpawner : PointSpawner {
		public float radius = 1f;

		protected override void SetTransform(GameObject go) {
			go.transform.SetPositionAndRotation(point.position + Random.insideUnitSphere * radius, point.rotation);
		}

		public void SetRadius(float r) {
			radius = r;
		}
	}
}
