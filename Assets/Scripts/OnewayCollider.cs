using UnityEngine;
using System.Collections;

public class OnewayCollider : MonoBehaviour {

	public bool enable;

	private BoxCollider2D platformCollider;

	void Start() {
		platformCollider = GetComponent<BoxCollider2D> ();

		// Add trigger is correct size for one-way collision
		BoxCollider2D onewayTrigger = gameObject.AddComponent ("BoxCollider2D") as BoxCollider2D;

		onewayTrigger.size = new Vector2 (platformCollider.size.x + 0.25f, platformCollider.size.y + 0.05f);
		onewayTrigger.center = new Vector2 (0.0f, -0.05f);
		onewayTrigger.isTrigger = true;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (enable && other.gameObject.tag == "Player") {
			Players player = other.GetComponent<Players> ();

			Collider2D[] colliders = other.gameObject.GetComponents<Collider2D>();
			foreach (Collider2D c in colliders) {
				Physics2D.IgnoreCollision (platformCollider, c, player.oneWayCollision);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (enable && other.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision (GetComponent<Collider2D>(), other, false);
		}
	}
}
