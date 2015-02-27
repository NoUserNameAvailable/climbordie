using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float verticalSpeed;
	public float horizontalSpeed;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = new Vector2 (1.0f * horizontalSpeed, 1.0f * verticalSpeed);
	}

}
