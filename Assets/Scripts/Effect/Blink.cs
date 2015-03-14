using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	public void StartEffect(float waitTime) {
		StartCoroutine(effect (waitTime));
	}

	IEnumerator effect(float waitTime) {
		float endTime = Time.time + waitTime;

		while (Time.time < endTime) {
			renderer.enabled = false;
			yield return new WaitForSeconds (0.1f);

			renderer.enabled = true;
			yield return new WaitForSeconds (0.1f);
		}

		renderer.enabled = true;
		Destroy (this);
	}

}
