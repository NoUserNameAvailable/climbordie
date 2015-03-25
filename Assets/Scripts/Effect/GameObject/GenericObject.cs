using UnityEngine;
using System.Collections;

public abstract class GenericObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			StartCoroutine(effect ());

			// Disactive render and trigger
			SpriteRenderer sprite = GetComponent<SpriteRenderer>();
			BoxCollider2D trigger = GetComponent<BoxCollider2D>();

			if (sprite != null)
				sprite.enabled = false;
			if (trigger != null)
				trigger.enabled = false;
		}
	}

	abstract public IEnumerator effect();

}
