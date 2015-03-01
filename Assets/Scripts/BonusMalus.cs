using UnityEngine;
using System.Collections;

public class BonusMalus : MonoBehaviour {

	public float newSpeed; 
	public float newJumpSpeed;

	void OnTriggerEnter2D (Collider2D col){
		
		if (col.gameObject.tag == "Player") {
			Players player = col.gameObject.GetComponent<Players>();

			if(this.newSpeed != -1)
				player.speed = newSpeed;

			if(this.newJumpSpeed!=-1)
				player.jumpSpeed = newJumpSpeed;
		
			Destroy(this.gameObject);
		}
		
	}

}
