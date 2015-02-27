using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Players : MonoBehaviour {

	// Player stats
	public int speed;
	public int jumpSpeed;
	public int jumpLimit;
	public int meltingSpeed;

	public Vector2 minSize;
	public Vector2 maxSize;

	// For one-way collision
	public bool oneWayCollision;

	// Animator for animation control
	private Animator animator;

	// Jump
	private int jump = 0;
	private int jumpState = 0;
	private bool canJump = true;

	// Rotate sprite when run left
	private bool runRight = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	void Update () {
		// Stop running
		if (Input.GetKeyUp ("left") || Input.GetKeyUp ("right")) {
			Vector2 movement = new Vector2 (0.0f, rigidbody2D.velocity.y);
			rigidbody2D.velocity = movement;
		}

		// Animation
		animeSprite ();
	}

	void FixedUpdate() {
		Vector3 movement;

		// Jumping
		if (Input.GetKeyDown ("up") && jump < jumpLimit) {
			canJump = true;
		}

		if (Input.GetKey ("up") && (jump == 0 || canJump)) {
			movement = new Vector2 (rigidbody2D.velocity.x, 1.0f * jumpSpeed);
			rigidbody2D.velocity = movement;
			
			jumpState = 0;
			canJump = false;
			jump++;
		}
		
		if (jump > 0)
			doJump ();
		
		// Running
		if (Input.GetKey ("left") && runRight)
			runRotate();

		if (Input.GetKey ("right") && ! runRight)
			runRotate();

		if (Input.GetKey ("left") || Input.GetKey ("right")) {
			float vx = (runRight) ? 1.0f : -1.0f;
			
			movement = new Vector2(vx * speed, rigidbody2D.velocity.y);
			rigidbody2D.velocity = movement;

			playerMelting ();
		}
	}
	
	// Rotate sprite when run in a side
	void runRotate() {
		runRight = !runRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	// Reduce player size when he run
	void playerMelting() {
		if (Mathf.Abs(transform.localScale.x) > minSize.x && transform.localScale.y > minSize.y) {

			// Reduce player size
			float sizeFactor = Mathf.Floor(transform.localScale.y + 1);
			float sx;

			if (transform.localScale.x > 0)
				sx = transform.localScale.x - (0.0003f * meltingSpeed * sizeFactor);
			else 
				sx = transform.localScale.x + (0.0003f * meltingSpeed * sizeFactor);

			Vector3 scale = new Vector3 (
				sx,
				transform.localScale.y - (0.0003f * meltingSpeed * sizeFactor),
				transform.localScale.z
			);

			transform.localScale = scale;
		}
	}

	// Increase player size
	public void increaseSize(float amount) {
		float sx = (transform.localScale.x > 0) ? transform.localScale.x + (amount) : transform.localScale.x - (amount);
		float sy = transform.localScale.y + amount;

		if (Mathf.Abs (sx) > maxSize.x)
			sx = (sx < 0) ? -maxSize.x : maxSize.x;
		if (sy > maxSize.y)
			sy = maxSize.y;

		Vector3 scale = new Vector3(sx, sy, transform.localScale.z);
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
