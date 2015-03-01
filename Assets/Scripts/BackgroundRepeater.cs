using UnityEngine;
using System.Collections;

public class BackgroundRepeater : MonoBehaviour {

	private Transform cameraTransform;
	private float backgroundSize;

	// Use this for initialization
	void Start () {

		cameraTransform = Camera.main.transform;
		backgroundSize = GetComponent<RectTransform> ().sizeDelta.y;
	
	}
	
	void Update () {
		if((transform.position.y + backgroundSize) < cameraTransform.position.y) {
			Vector3 newPos = transform.position;
			newPos.y += 2.0f * backgroundSize; 
			transform.position = newPos;
		}
	}
}
