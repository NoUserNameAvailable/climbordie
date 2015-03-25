using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour {

	public List<GenericObject> Bonus;
	public List<GenericObject> Malus;

	// Use this for initialization
	void Start () {
		GameController controller = GameController.getInstance ();
		double seed = 1 - (controller.currentLevel * 0.1);

		System.Random rnd = new System.Random ();

		if (rnd.NextDouble () < seed) {
			if (controller.currentLevel % 2 == 0)
				addBonus();
			else
				addMalus();

			Destroy(this.gameObject);
		}
	}

	void addBonus() {
		System.Random rnd = new System.Random ();
		int objIndex = rnd.Next (0, Bonus.Count);

		Instantiate (Bonus[objIndex], transform.position, Quaternion.identity);
	}

	void addMalus() {
		System.Random rnd = new System.Random ();
		int objIndex = rnd.Next (0, Malus.Count);

		Instantiate (Malus[objIndex], transform.position, Quaternion.identity);
	}

}
