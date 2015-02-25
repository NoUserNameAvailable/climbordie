using UnityEngine;
using System.Collections;

public class OnewayCollider : MonoBehaviour {

	public bool enable;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (enable && other.gameObject.tag == "Player") {
			Players player = other.GetComponent<Players>();

			Physics2D.IgnoreCollision (
				(Collider2D) GetComponent<Collider2D>(),
				other,
				player.oneWayCollision
			);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (enable && other.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision (
				(Collider2D) GetComponent<Collider2D>(),
				other,
				false
			);
		}
	}

}
