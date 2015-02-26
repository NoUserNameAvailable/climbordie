using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Players : MonoBehaviour {

	// Player stats
	public int speed;
	public int jumpSpeed;
	public int jumpLimit;

	// For one-way collision
	public bool oneWayCollision;

	// Animator for animation control
	private Animator animator;

	// Jump
	private int jump = 0;
	private int jumpState = 0;
	private bool canJump = true;
	
	private bool runOn = false;

	// Rotate sprite when run left
	private bool runRight = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update () {
		// Basic player control

		// Jumping
		if (Input.GetKeyDown ("up") && jump < jumpLimit) {
			canJump = true;
		}

		// Animation
		animeSprite ();
	}

	// Update physics
	void FixedUpdate() {
		// Jumping
		if (Input.GetKey("up") && (jump == 0 || canJump)) {
			Vector3 movement = new Vector3(rigidbody2D.velocity.x, 1.0f * jumpSpeed, 0.0f);
			rigidbody2D.velocity = movement;
			
			jumpState = 0;
			canJump = false;
			jump++;
		}

		if (jump > 0) doJump();

		// Run left
		if (Input.GetKey ("left")) {
			if (runRight)
				runRotate();
			
			Vector3 movement = new Vector3(-1.0f * speed, rigidbody2D.velocity.y, 0.0f);
			rigidbody2D.velocity = movement;
		}
		
		// Run right
		if (Input.GetKey ("right")) {
			if ( ! runRight)
				runRotate();
			
			Vector3 movement = new Vector3(1.0f * speed, rigidbody2D.velocity.y, 0.0f);
			rigidbody2D.velocity = movement;
		}
		
		// Stop running
		if (Input.GetKeyUp ("left") || Input.GetKeyUp ("right")) {
			Vector3 movement = new Vector3(0.0f, rigidbody2D.velocity.y, 0.0f);
			rigidbody2D.velocity = movement;
		}
	}

	// Rotate sprite when run in a side
	void runRotate() {
		runRight = !runRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	// Make it jump !
	void doJump() {
		if (jumpState == 0 && rigidbody2D.velocity.y < 0) {
			jumpState = 1;
		}

		// Back to beginning
		if (jump > 0 && jumpState == 1 && rigidbody2D.velocity.y == 0) {
			animator.ResetTrigger("fall");
			animator.SetTrigger ("landing");
			jump = 0;
		}
	}

	// Trigger animation
	void animeSprite() {
		float velocityX = rigidbody2D.velocity.x;
		float velocityY = rigidbody2D.velocity.y;

		if (velocityX != 0) {
			animator.SetTrigger ("run");
		} else {
			animator.SetTrigger("stay");
		}

		if (velocityY > 0) {
			animator.SetTrigger("jump");
		}
		if (velocityY < 0) {
			animator.ResetTrigger("jump");
			animator.SetTrigger("fall");

			// When player fall is jump (animation and block player jumping)
			if (jump == 0) {
				jumpState = 1;
				jump = 1;
			}
		}
	}

}
