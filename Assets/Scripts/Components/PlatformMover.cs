using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {

	public Vector2 moveTo;
	public float speed;
	
	private Vector2 initPos;
	private Vector2 moveToAbs;
	private Vector2 velocity;
	
	private Transform playerTransform;
	private Vector3 lastPlatformPosition;
	
	private RectTransform rect;

	void Start() {
		rect = GetComponent<RectTransform> ();
		
		initPos = (Vector2) rect.position;
		lastPlatformPosition = rect.position;

		moveToAbs = vectorAbs (moveTo);
		velocity = calcVelocity ();
	}
	
	void Update() {
		if (playerTransform != null) {
			Vector3 moveTo = rect.position - lastPlatformPosition;

			if (moveTo != Vector3.zero)
				playerTransform.position += moveTo;
		}
		
		lastPlatformPosition = rect.position;
	}

	void FixedUpdate() {
		Vector2 diff = (Vector2) rect.position - initPos;
		Vector2 diffAbs = vectorAbs (diff);

		if (diffAbs.x < moveToAbs.x || diffAbs.y < moveToAbs.y) {
			rigidbody2D.MovePosition(transform.position + (Vector3) velocity * Time.deltaTime);
		} else {
			rigidbody2D.velocity = Vector2.zero;

			initPos += moveTo;
			moveTo = moveTo * -1;

			moveToAbs = vectorAbs(moveTo);
			velocity = calcVelocity();
		}
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			playerTransform = other.gameObject.transform;
		}
	}
	
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			playerTransform = null;
		}
	}

	Vector2 vectorAbs(Vector2 src) {
		return new Vector2 (Mathf.Abs (src.x), Mathf.Abs (src.y));
	}

	Vector2 calcVelocity() {
		float vx, vy;

		float sx = Mathf.Sign (moveTo.x);
		float sy = Mathf.Sign (moveTo.y);

		if (Mathf.Abs (moveTo.x) > Mathf.Abs (moveTo.y)) {
			vx = 1.0f * speed * sx;
			vy = moveTo.y * vx / moveTo.x;
		} else if (moveTo.y != 0) {
			vy = 1.0f * speed * sy;
			vx = moveTo.x * vy / moveTo.y;
		} else {
			vy = 0f;
			vx = vy;
		}

		return new Vector2 (vx, vy);
	}

}

