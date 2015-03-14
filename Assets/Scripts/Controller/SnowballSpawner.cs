using UnityEngine;
using System.Collections;

public class SnowballSpawner : MonoBehaviour {

	public float intervalCheck;
	public GameObject snowPrefab;

	private Players player;
	private Vector3 lastTryPosition;

	// Use this for initialization
	void Start () {
		lastTryPosition = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {

		if (player != null && (player.transform.position.y - lastTryPosition.y) > intervalCheck) {
			float health = player.getHealth();

			if (health < 0.5) {
				System.Random rnd = new System.Random();
				double stats = health - (GameController.getInstance().currentLevel * 0.02);
				if (stats > 0 && rnd.NextDouble() < stats) {
					Instantiate(
						snowPrefab,
						new Vector3(player.transform.position.x, player.transform.position.y + 10, 0f),
					    Quaternion.identity
					);
				}

				lastTryPosition = player.transform.position;
			}

		}

	}

	public void setPlayer(Players value) {
		player = value;
	}

}
