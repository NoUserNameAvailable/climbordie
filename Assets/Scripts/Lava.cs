using UnityEngine;
using System.Collections;

public class Lava : MonoBehaviour {

	public AudioSource playerDie;

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			GameController controller = GameController.GetInstance();
			playerDie.Play();
			StartCoroutine(controller.EndGame());
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Platform") {
			Destroy (other.gameObject);
		}
	}

}
