using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	/*void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameController controller = GameController.GetInstance();
			playerDie.Play();
			StartCoroutine(controller.EndGame());
		}
	}*/

	public float damage;

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Players>().takeDamage(damage);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Platform") {
			Destroy (other.gameObject);
		}
	}

}
