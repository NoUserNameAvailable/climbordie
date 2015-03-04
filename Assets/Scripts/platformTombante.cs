using UnityEngine;
using System.Collections;

public class platformTombante : MonoBehaviour {

	private GameObject lava ;
	public float time ;

	private bool collide ;
	private float timeOld ;
	private float timeNow ;

	void start(){
		collide = false ;
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag == "Player" && !collide) {
		//	print("ok on est en colision");
			timeOld = Time.realtimeSinceStartup ;
			collide = true ;	
		}
	}

	void Update(){
		if (collide) {
		//	print("ok on est en colision update");
			timeNow = Time.realtimeSinceStartup;
		//	print((timeNow - timeOld));
			if( (timeNow - timeOld) > time ) {
				this.rigidbody2D.isKinematic = false ;
				this.collider2D.enabled = false ;
				collide = false ;
			}
		}

	}
}
