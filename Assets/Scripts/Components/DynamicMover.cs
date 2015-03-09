using UnityEngine;
using System.Collections;

public class DynamicMover : Mover {

	void FixedUpdate() {
		rigidbody2D.velocity = new Vector2 (1.0f * horizontalSpeed, 1.0f * verticalSpeed);
	}
	
}
