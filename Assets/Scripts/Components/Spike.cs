using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	public float damage;
	
	private float playerSpeed = 2.0f;

	private bool SpikeActive = true;

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (SpikeActive)
				StartCoroutine( effect(other.gameObject.GetComponent<Players>()) );
		}
	}

	IEnumerator effect(Players player) {
		SpikeActive = false;

		Blink blink = player.gameObject.AddComponent<Blink> ();
		blink.StartEffect (2f);

		player.speed = playerSpeed / 2;
		player.takeDamage (damage);

		yield return new WaitForSeconds (2f);
		
		player.speed = playerSpeed;
		SpikeActive = true;
	}

}
