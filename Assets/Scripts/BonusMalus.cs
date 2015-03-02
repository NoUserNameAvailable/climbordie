using UnityEngine;
using System.Collections;

public class BonusMalus : MonoBehaviour {

	public float newSpeed; 
	public float newJumpSpeed;
	public bool doubleJump ;
	public float time ;



	private float timeOld ;
	private float timeNow ;
	private Players player;
	private float speedOld ;
	private float speedJumpOld ;
	private bool collide ;

	void start(){
		timeNow = Time.realtimeSinceStartup;
		timeOld = 100000000000000000000.0F;
	}

	void OnTriggerEnter2D (Collider2D col){
		collide = true;
		if (col.gameObject.tag == "Player") {
			timeOld = Time.realtimeSinceStartup;
			player = col.gameObject.GetComponent<Players>();
			speedOld = player.speed ;
			speedJumpOld = player.jumpSpeed ;

			if(this.newSpeed != -1)
				player.speed = newSpeed;

			if(this.newJumpSpeed!=-1)
				player.jumpSpeed = newJumpSpeed;

			if(doubleJump) 
				player.jumpLimit = 2 ;
		}

		this.renderer.enabled = false;

		
	}

	void Update(){
		if (collide) {
			timeNow = Time.realtimeSinceStartup;
			if ((timeNow - timeOld) > time) {

				player.speed = speedOld;
				player.jumpSpeed = speedJumpOld; 
				player.jumpLimit = 1 ;
				collide = false ;
				Destroy (this.gameObject);
			}
		}
	}
	

}
