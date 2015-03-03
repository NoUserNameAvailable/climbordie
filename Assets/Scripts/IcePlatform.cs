using UnityEngine;
using System.Collections;

public class IcePlatform : MonoBehaviour {

	private GameObject lava ;
	public float distance ;

	
	// Update is called once per frame
	void Update () {
		if (lava == null) 
			lava = GameObject.FindGameObjectWithTag ("Lava");
		else {
			float sizeFactor = Mathf.Floor(transform.localScale.y + 1);
			//print(transform.position.y -lava.transform.position.y);
			if((transform.position.y - lava.transform.position.y) <  (distance + 3 ) ){
				//float sy  = transform.localScale.y * 0.99f  ;
				float sx  = transform.localScale.x -  (0.0003f  * sizeFactor)  ;
				Vector3 scale = new Vector3 (sx,/*sy*/transform.localScale.y,transform.localScale.z);
				transform.localScale = scale ;
			}
		}
			
		}

}
