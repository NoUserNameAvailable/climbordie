using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {


	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			Destroy(other.gameObject);

		

		}
	}
}
