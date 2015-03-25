using UnityEngine;
using System.Collections;

public class SpeedUp : GenericObject {

	private float playerSpeed;
	
	void Start() {
		playerSpeed = GameController.getInstance ().getPlayer ().speed;
	}
	
	public override IEnumerator effect() {
		Players player = GameController.getInstance ().getPlayer ();

		Blink blink = player.gameObject.AddComponent<Blink> ();
		blink.StartEffect (2f);
		
		player.speed = (float) (playerSpeed * 1.5);
		
		yield return new WaitForSeconds (2f);
		
		player.speed = playerSpeed;
		Destroy (this);
	}
}
