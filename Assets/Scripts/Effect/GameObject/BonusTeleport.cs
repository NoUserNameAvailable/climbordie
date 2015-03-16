using UnityEngine;
using System.Collections;

public class BonusTeleport : MonoBehaviour {

	public GameObject recepteur;
	public float time ;
	
	private Players player ;
	private bool collide ;
	private float timeOld ;
	private float timeNow ;
	private int i ;
	private Vector3 playerP ;
	

	void Start(){
		collide = false ;
		i = 0;
	}
	
	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Player" && collide == false) {
			player = col.gameObject.GetComponent<Players>();
			timeOld = Time.realtimeSinceStartup ;
			if(recepteur.transform.rotation.y == 0 ){
				player.transform.position = new Vector3(recepteur.transform.position.x-0.35f,recepteur.transform.position.y,0) ;  
			}
			else{
				player.transform.position = new Vector3(recepteur.transform.position.x+0.35f,recepteur.transform.position.y,0) ;  
			}
			playerP = player.transform.position ;
			collide = true;	
		}
	}

	void Update(){
		
		if (collide) {
			timeNow = Time.realtimeSinceStartup;
			if( (timeNow - timeOld) > time ) {
				collide = false ;
			}
			else{
				if(recepteur.transform.rotation.y == 0 ){
					player.transform.position = new Vector3(playerP.x-0.01f,playerP.y,0) ; 		
				}
				else{
					player.transform.position = new Vector3(playerP.x+0.01f,playerP.y,0) ; 	
				}
				playerP = player.transform.position ;

				if(player.transform.localScale.x == 1 && recepteur.transform.rotation.y == -1 ){
					runRotate();
				}
				else if(player.transform.localScale.x == -1 && recepteur.transform.rotation.y == 0 ){
					runRotate();
				}	
			}
		}	
	}

	void runRotate() {
		Vector3 scale = player.transform.localScale;
		scale.x *= -1;
		player.transform.localScale = scale;
	}

}
