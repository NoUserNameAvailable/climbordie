using UnityEngine;
using System.Collections;

public class platformTombante : MonoBehaviour {

	private GameObject lava ;
	public float distance ;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.tag == "Player") {
			this.rigidbody2D.isKinematic = false ;
			this.collider2D.enabled = false ;	
	}
}
}
