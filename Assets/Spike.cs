using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	public float damage;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Players> ().takeDamage (damage);
			other.gameObject.rigidbody2D.velocity = new Vector2 (0.0f, 3.0f);
		}
	}

}
