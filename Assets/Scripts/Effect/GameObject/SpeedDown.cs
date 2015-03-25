using UnityEngine;
using System.Collections;

public class SpeedDown : GenericObject {

	private float playerSpeed;

	void Start() {
		if (GameController.getInstance ().getPlayer () != null)
			playerSpeed = GameController.getInstance ().getPlayer ().speed;
		else
			playerSpeed = 2.0f;
	}

	public override IEnumerator effect() {
		Players player = GameController.getInstance ().getPlayer ();

		Blink blink = player.gameObject.AddComponent<Blink> ();
		blink.StartEffect (2f);
		
		player.speed = (float) (playerSpeed / 1.5);
		
		yield return new WaitForSeconds (2f);

		player.speed = playerSpeed;
	}

}
