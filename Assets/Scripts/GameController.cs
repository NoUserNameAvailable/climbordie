using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject playerPrefab;

	private GameObject player;
	private GameObject timer;
	private bool gameRunning = false;

	// Use this for initialization
	void Awake () {
		GameObject spawn = GameObject.FindGameObjectWithTag ("PlayerSpawn");

		// Spawn player
		player = Instantiate (playerPrefab, spawn.transform.position, Quaternion.identity) as GameObject;
		player.SetActive (false);
		spawn.SetActive (false);

		// Start timer
		timer = GameObject.Find("Timer");
	}

	void Update() {
		if (! gameRunning && Input.GetKey (KeyCode.Return))
			StartGame ();
	}

	void StartGame() {
		// Hide start text
		GameObject startText = GameObject.Find ("StartText");
		if (startText != null)
			startText.SetActive (false);

		// Active player
		player.SetActive (true);

		// Active and start timer
		if (timer != null) {
			Timer timerObj = timer.GetComponent<Timer>();
			timerObj.stop = false;
		}
	}

}
