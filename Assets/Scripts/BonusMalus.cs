using UnityEngine;
using System.Collections;

public class BonusMalus : MonoBehaviour {

	public int newSpeed = 2; 
	public int newJumpSpeed = 5; 
	
	public void applyEffect(Players p){

		//print (p.speed);
		if(this.newSpeed!=-1) p.setSpeed (this.newSpeed);
		if(this.newJumpSpeed!=-1) p.setJumpSpeed (this.newJumpSpeed);

	}

}