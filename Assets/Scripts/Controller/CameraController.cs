using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public bool followMode;
	public float speed = 0.0f; // Forced scrolling speed

	private Vector3 newPosition;
	private GameObject player;

	// Use this for initialization
	void Start () {
		newPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// Forced Scrolling
		if (! followMode) {
			newPosition.y += Time.deltaTime * speed;
			transform.position = newPosition;
		} else { 
			// Follow scrolling
			if (player != null) {
				newPosition.y = player.transform.position.y;
				transform.position = newPosition;
			}
		}
	}

	// Set player gameobject
	public void setPlayer(GameObject player) {
		this.player = player;
	}

}
