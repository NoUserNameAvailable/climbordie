﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Players : MonoBehaviour {

	// Player stats
	public float speed;
	public float jumpSpeed;
	public int jumpLimit;

	public float meltingSpeed;

	public Vector2 minSize;
	public Vector2 maxSize;

	// Player SFX
	private AudioSource audioSource;

	public AudioClip jumpAudio;
	public AudioClip runAudio;
	public AudioClip sizeupAudio;
	public AudioClip playerHit;
	public AudioClip playerImpact;

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

	// Grounded status
	private bool isGrounded;

	// Observer for healthbar
	private IPlayerObserver obs;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update () {
		// Stop running
		if (Input.GetKeyUp ("left") || Input.GetKeyUp ("right")) {
			Vector2 movement = new Vector2 (0.0f, rigidbody2D.velocity.y);
			rigidbody2D.velocity = movement;
		}

		// Animation
		animeSprite ();

		// Update health
		if (obs != null)
			obs.PlayerUpdate (this);
	}

	void FixedUpdate() {
		Vector3 movement;

		// Jumping
		if ((Input.GetKeyDown ("up") || Input.GetKeyDown ("space")) && jump < jumpLimit) {
			canJump = true;
		}

		if ((Input.GetKey ("up") || Input.GetKey ("space")) && (jump == 0 || canJump)) {
			movement = new Vector2 (rigidbody2D.velocity.x, 1.0f * jumpSpeed);
			rigidbody2D.velocity = movement;

			playSFX("jump");
			
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

		if ((Input.GetKey ("left")) || (Input.GetKey ("right"))) {
			float vx = (runRight) ? 1.0f : -1.0f;

			movement = new Vector2(vx * speed, rigidbody2D.velocity.y);
			rigidbody2D.velocity = movement;
		}
	}

	void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Pic") {
			//transform.parent = other.gameObject.transform;
			isGrounded = true;
		}
		else {
		//	transform.parent = null;
		}
	}
	
	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Pic") {
			isGrounded = false;
		}
	}
	
	// Rotate sprite when run in a side
	void runRotate() {
		runRight = !runRight;

		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	// Reduce player size
	public void melting() {

		if (Mathf.Abs (transform.localScale.x) > minSize.x && transform.localScale.y > minSize.y) {
			float sizeFactor = Mathf.Floor (transform.localScale.y + 1);
			float sx;

			sx = transform.localScale.x - (0.0003f * meltingSpeed * sizeFactor * Mathf.Sign (transform.localScale.x));

			Vector3 scale = new Vector3 (
				sx,
				transform.localScale.y - (0.0003f * meltingSpeed * sizeFactor),
				transform.localScale.z
			);

			transform.localScale = scale;
		} else {
			// If player is so small, make him die
			GameController.getInstance().playerDie();
		}

	}

	// Take damage
	public void takeDamage(float damage) {
		if (Mathf.Abs (transform.localScale.x) > minSize.x && transform.localScale.y > minSize.y) {
			float sx;
			
			sx = transform.localScale.x - (0.01f * damage * Mathf.Sign (transform.localScale.x));
			
			Vector3 scale = new Vector3 (
				sx,
				transform.localScale.y - (0.01f * damage),
				transform.localScale.z
			);
			
			transform.localScale = scale;
			playSFX("impact");
		} else {
			// If player is so small, make him die
			GameController.getInstance().playerDie();
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

		playSFX("sizeup");
	}

	// Make it jump !
	void doJump() {
		if (jumpState == 0 && rigidbody2D.velocity.y < 0) {
			jumpState = 1;
		}

		// Back to beginning
		if (jump > 0 && jumpState == 1 && isGrounded) {
			animator.ResetTrigger("fall");
			animator.SetTrigger ("landing");
			jump = 0;
		}
	}

	// Trigger animation
	void animeSprite() {
		float velocityX = rigidbody2D.velocity.x;
		float velocityY = rigidbody2D.velocity.y;

		if (velocityX != 0 && velocityY == 0) {
			animator.SetTrigger ("run");
			playSFX ("run");
		} else {
			animator.SetTrigger ("stay");
		}

		if (velocityY > 0) {
			animator.SetTrigger ("jump");
		}
		if (velocityY < -0f && ! isGrounded) {
			animator.ResetTrigger ("jump");
			animator.SetTrigger ("fall");

			// When player fall is jump (animation and block player jumping)
			if (jump == 0) {
				jumpState = 1;
				jump = 1;
			}
		}
	}

	// Play audio clip
	void playSFX(string name) {
		if (name == "jump") {
			audioSource.clip = jumpAudio;
			audioSource.Play ();
		} else if (name == "sizeup") {
			audioSource.clip = sizeupAudio;
			audioSource.Play ();
		} else if (name == "run") {
			if (! audioSource.isPlaying) {
				audioSource.clip = runAudio;
				audioSource.Play ();
			}
		} else if (name == "hit") {
			if (! audioSource.isPlaying) {
				audioSource.clip = playerHit;
				audioSource.Play ();
			}
		} else if (name == "impact") {
			audioSource.clip = playerImpact;
			audioSource.Play ();
		}
	}

	// Set player observer
	public void setObs(IPlayerObserver obs) {
		this.obs = obs;
	}

	// Get player health
	public float getHealth() {
		float playerSizeAbs = Mathf.Abs (transform.localScale.x) - minSize.x;
		float size = maxSize.x - minSize.x;
		
		return playerSizeAbs / size;
	}

}
