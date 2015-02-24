using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Players : MonoBehaviour {

	public int speed;
	public int jumpSpeed;

	private Animator animator;

	private bool jump = false;
	private int jumpState = 0;
	private bool runRight = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		animeSprite ();

		// Basic player control

		// Jumping
		if (Input.GetKey("up") && jump == false) {
			Vector3 movement = new Vector3(rigidbody2D.velocity.x, 1.0f * jumpSpeed, 0.0f);
			rigidbody2D.velocity = movement;

			jumpState = 0;
			jump = true;
		}
		
		if (jump) doJump ();

		// Run left
		if (Input.GetKey ("left")) {
			if (runRight)
				runRotate();

			Vector3 movement = new Vector3(-1.0f * speed, rigidbody2D.velocity.y, 0.0f);
			rigidbody2D.velocity = movement;
			animator.SetTrigger("+run");
		}

		// Run right
		if (Input.GetKey ("right")) {
			if ( ! runRight)
				runRotate();

			Vector3 movement = new Vector3(1.0f * speed, rigidbody2D.velocity.y, 0.0f);
			rigidbody2D.velocity = movement;
			animator.SetTrigger("+run");
		}

		// Stop running
		if (Input.GetKeyUp ("left") || Input.GetKeyUp ("right")) {
			Vector3 movement = new Vector3(0.0f, rigidbody2D.velocity.y, 0.0f);
			rigidbody2D.velocity = movement;
		}
	
	}

	void runRotate() {
		runRight = !runRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	// Make it jump !
	void doJump() {
		if (rigidbody2D.velocity.y < 0) {
			animator.ResetTrigger("-jump");
			animator.SetTrigger("+jump");
			jumpState = 1;
		}

		// Back to initiale
		if (jump && jumpState == 1 && rigidbody2D.velocity.y == 0) {
			animator.ResetTrigger("+jump");
			animator.SetTrigger ("-jump");
			jump = false;
		}
	}

	// Trigger animation
	void animeSprite() {
		if (Input.GetKeyDown ("left") || Input.GetKeyDown ("right")) {
			animator.ResetTrigger("-run");
		}
		if (Input.GetKeyUp ("left") || Input.GetKeyUp ("right")) {
			if (!jump)
				animator.SetTrigger("-run");

			animator.ResetTrigger("+run");
		}
	}

}
