using UnityEngine;
using System.Collections;

public class BonusTeleport : MonoBehaviour {

	public GameObject recepteur; 

	private Players player ;
	private bool collide ;
	private bool activate = false ;


	void start(){
	}
	
	void OnTriggerEnter2D (Collider2D col){
		collide = true;
		if (col.gameObject.tag == "Player") {
			if(!activate){
				activate = true ;
			}
			player = col.gameObject.GetComponent<Players>();
			print(transform.rotation.y);
			if(transform.rotation.y == 1 ){
				player.transform.position = new Vector3(recepteur.transform.position.x-0.35f,recepteur.transform.position.y,0) ;  
			}
			else if(transform.rotation.y == 0 ){
				player.transform.position = new Vector3(recepteur.transform.position.x+0.35f,recepteur.transform.position.y,0) ;  
			}
			
		}
	}
}
