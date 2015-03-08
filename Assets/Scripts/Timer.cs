using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
	
	public int timeLimit;
	public bool stop = true;

	private Text textComponent;
	private bool descending = false;
	private float counter;

	// Use this for initialization
	void Start () {
		textComponent = GetComponent<Text> ();
		descending = (timeLimit > 0);

		counter = (descending) ? timeLimit : 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (! stop) {
			if (descending) {
				counter -= Time.deltaTime;

				if (counter <= 0) {
					counter = 0;
					stop = true;
				}
			}
			else {
				counter += Time.deltaTime;
			}

			// Update text
			textComponent.text = string.Format ("{0:00}:{1:00}:{2:00}", Mathf.Floor (counter / 60), Mathf.Floor (counter % 60), Mathf.Floor ((counter * 100) % 100));
		}
	}
}
