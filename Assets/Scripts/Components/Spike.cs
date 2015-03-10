using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	public float damage;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Debug.Log(other.gameObject.rigidbody2D.velocity.y * -1.0f);
			other.gameObject.rigidbody2D.velocity = new Vector2 (-1f * (other.gameObject.rigidbody2D.velocity.x + 1.0f), Mathf.Sign(other.gameObject.rigidbody2D.velocity.y) * -3f);
		}
	}

}
