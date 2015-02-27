using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// GameObject
	public GameObject playerPrefab;
	public GameObject gameUIPrefab;
	public GameObject lavaPrefab;

	private GameObject lava;
	private GameObject player;

	// GUI
	private GameObject timer;
	private GameObject startText;
	private GameObject endText;

	// Game state
	private bool gameRunning = false;
	private bool gameEnd = false;

	// GameController instance
	private static GameController controller;

	// Use this for initialization
	void Awake () {
		// Instance game controller
		if (controller != null)
			return;
		else
			controller = this;

		// Create game GUI
		Instantiate(gameUIPrefab, Vector3.zero, Quaternion.identity);

		// Get timer
		timer = GameObject.Find("Timer");
		
		// Active start text
		startText = GameObject.Find ("StartText");
		if (startText != null)
			startText.SetActive(true);

		// Disable end text
		endText = GameObject.Find ("EndText");
		if (endText != null)
			endText.SetActive (false);

		// Create lava
		RectTransform lavaRect = lavaPrefab.GetComponent<RectTransform> ();
		lava = Instantiate (lavaPrefab, lavaRect.position, lavaRect.rotation) as GameObject;

		// Find player spawn
		GameObject spawn = GameObject.FindGameObjectWithTag ("PlayerSpawn");

		// Spawn player
		player = Instantiate (playerPrefab, spawn.transform.position, Quaternion.identity) as GameObject;
		player.SetActive (false);
		spawn.SetActive (false);
	}

	void Update() {
		if (! gameRunning && Input.GetKey (KeyCode.Return))
			StartGame ();

		if (gameEnd && Input.GetKeyDown (KeyCode.R))
			Application.LoadLevel (Application.loadedLevel);
	}

	void StartGame() {
		// Hide start text
		if (startText != null)
			startText.SetActive (false);

		// Active player
		player.SetActive (true);

		// Active and start timer
		if (timer != null) {
			Timer timerObj = timer.GetComponent<Timer>();
			timerObj.stop = false;
		}

		// Active lava
		DynamicMover lavaMover = lava.GetComponent<DynamicMover> ();
		lavaMover.verticalSpeed = 0.2f;
	}

	public void EndGame() {
		// Disable player
		player.SetActive(false);

		// Stop timer
		if (timer != null) {
			Timer timerObj = timer.GetComponent<Timer>();
			timerObj.stop = true;
		}

		// Freeze lava
		DynamicMover lavaMover = lava.GetComponent<DynamicMover> ();
		lavaMover.verticalSpeed = 0.0f;

		// Show gameover
		if (endText != null)
			endText.SetActive (true);

		// Set game end
		gameEnd = true;
	}

	// Get controller instance
	public static GameController GetInstance() {
		return controller;
	}

}
