using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			Players player = collider.gameObject.GetComponent<Players>();
			player.increaseSize(0.5f);
			Destroy(this.gameObject);
		}

	}
}
