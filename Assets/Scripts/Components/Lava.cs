using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameController.getInstance().playerDie();
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Platform") {
			Destroy (other.gameObject);
		}
	}

}
