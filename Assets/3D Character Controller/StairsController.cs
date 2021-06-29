using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsController : MonoBehaviour {
	PlayerCharacter3DMovement player;

	void Update() {
		if(player) {

		}
	}

	void OnCollisionEnter(Collision collision) {
		PlayerCharacter3DMovement p = collision.gameObject.GetComponent<PlayerCharacter3DMovement>();
		if(p) {
			player = p;
		}
	}
	void OnCollisionExit(Collision collision) {
		PlayerCharacter3DMovement p = collision.gameObject.GetComponent<PlayerCharacter3DMovement>();
		if(p) {
			player = null;
		}
	}
}
