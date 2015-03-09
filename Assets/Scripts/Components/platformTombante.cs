using UnityEngine;
using System.Collections;

public class platformTombante : MonoBehaviour {

	private GameObject lava ;
	public float time ;

	private bool collide ;
	private float timeOld ;
	private float timeNow ;
	private bool turn ;
	private int i ;

	// Player SFX
	private AudioSource audioSource;
	
	public AudioClip contactAudio;
	public AudioClip chuteAudio;

	void Start(){
		collide = false ;
		turn = true ;
		i = 0;
			audioSource = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter2D(Collision2D collider) {
		if (collider.gameObject.tag == "Player" && !collide) {
		//	print("ok on est en colision");
			timeOld = Time.realtimeSinceStartup ;
			collide = true ;
			playSFX("contact");
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
				playSFX("chute");
			}
			else{
				if(turn && i== 5){
					transform.position = new Vector3(transform.position.x,transform.position.y+(0.06f*(timeNow - timeOld)),transform.position.z);
					turn = false ;
					i=0;
				}
				else if(!turn && i== 10){
					transform.position = new Vector3(transform.position.x,transform.position.y-(0.049f*(timeNow - timeOld)),transform.position.z);
					turn = true ;
					i=0;
				}
				else{
					i++;
				}
			}
		}

	}

	// Play audio clip
	void playSFX(string name) {
		if (name == "contact") {
			audioSource.clip = contactAudio;
			audioSource.Play ();
		} else if (name == "chute") {
			audioSource.clip = chuteAudio;
			audioSource.Play ();
		}
	}
}
