using UnityEngine;
using SoulFlare.RequiredVariables;

namespace SoulFlare.Spawners {
	public class MeshSpawner : Spawner {
		[Required(GameObjectType.SCENE)]
		[SerializeField] MeshFilter spawnArea;

		int[] t;
		Vector3[] v;

		void Awake() {
			SetMesh(spawnArea);
		}

		protected override void SetTransform(GameObject go) {
			float x = Random.value; // set random point on unit square
			float y = Random.value; // set random point on unit square
			float s = 1f - x; // unit triangle slope

			if(y > s) { // if point on unit square is outside unity triangle
				x -= y - s; // subtract y's extra component from x
				y = s;
			}

			int r = Random.Range(0, t.Length / 3) * 3; // find random triangle index start point in array

			Vector3 p = v[t[r]]; // set 0 point
			go.transform.position = spawnArea.transform.TransformPoint(p + (v[t[++r]] - p) * x + (v[t[++r]] - p) * y); // add contributions from other vectors
		}

		public void SetMesh(MeshFilter mf) {
			if(mf == null) { return; }
			t = mf.mesh.triangles;
			v = mf.mesh.vertices;
		}
	}
}
