using UnityEngine;
using System.Collections;

public class DoubleJump : GenericObject {

	void Start() { }
	
	public override IEnumerator effect() {
		Players player = GameController.getInstance ().getPlayer ();
		
		player.jumpLimit = 2;
		
		yield return new WaitForSeconds (10f);
		
		player.jumpLimit = 1;
	}

}
