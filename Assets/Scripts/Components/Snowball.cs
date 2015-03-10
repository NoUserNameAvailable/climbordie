using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {

	public float health;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			Players player = collider.gameObject.GetComponent<Players>();
			player.increaseSize(health);
			Destroy(this.gameObject);
		}

	}
}
