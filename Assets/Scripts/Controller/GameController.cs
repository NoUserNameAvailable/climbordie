using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// GameObject
	public GameObject playerPrefab;
	public GameObject lavaPrefab;

	private GameObject lava;
	private GameObject player;
	private Players playerObj;

	private CameraController cameraControl;

	// GUI
	public GameObject gameUIPrefab;
	private GameUIManager uimanager;

	// Game state
	public int level = 0;
	public int meterPerLevel;

	private int nextLevel;
	private int meter = 0;

	private bool gameRunning = false;
	private bool gameEnd = false;

	// GameController instance
	private static GameController controller;

	// Erase counter
	private int eraseLevelFrame;

	// Use this for initialization
	void Awake () {
		// Instance game controller
		if (controller != null)
			return;
		else
			controller = this;

		// Create game GUI
		GameObject ui = Instantiate(gameUIPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		uimanager = ui.GetComponent<GameUIManager> ();

		// Active start text
		uimanager.toggleStartText (true);

		// Create lava
		RectTransform lavaRect = lavaPrefab.GetComponent<RectTransform> ();
		lava = Instantiate (lavaPrefab, lavaRect.position, lavaRect.rotation) as GameObject;

		// Find player spawn
		GameObject spawn = GameObject.FindGameObjectWithTag ("PlayerSpawn");

		// Spawn player
		player = Instantiate (playerPrefab, spawn.transform.position, Quaternion.identity) as GameObject;
		playerObj = player.GetComponent<Players> ();

		player.SetActive (false);
		spawn.SetActive (false);

		// Find camera
		cameraControl = Camera.main.GetComponent<CameraController> ();
		cameraControl.setPlayer (player);

		nextLevel = meterPerLevel;
	}

	void Update() {
		if (! gameRunning && Input.GetKey (KeyCode.Return))
			StartCoroutine(this.StartGame());

		if (gameEnd && Input.GetKeyDown (KeyCode.R))
			Application.LoadLevel (Application.loadedLevel);

		if (gameRunning && player != null) {
			// Update meter count
			if (player.transform.position.y > meter) {
				meter = (int)player.transform.position.y;
				uimanager.updateMeter (meter);
			}

			// Update current level
			if (player.transform.position.y >= nextLevel) {
				level += 1;
				nextLevel += meterPerLevel;

				GetComponent<LevelManager>().currentLevel = level;
				uimanager.updateLevel(level);
			}

			// Update lava position (for more chalenge)
			if (lava.transform.position.y + 27 < player.transform.position.y) {
				lava.transform.position = new Vector3(lava.transform.position.x, player.transform.position.y - 27, lava.transform.position.z);
			}

			// Erase level (only 1 frame of 10)
			if (eraseLevelFrame % 10 == 0) {
				GameObject[] levelsInMap = GameObject.FindGameObjectsWithTag("Level");

				foreach (GameObject obj in levelsInMap) {
					if (obj.transform.position.y < lava.transform.position.y)
						Destroy(obj);
				}

				eraseLevelFrame = 0;
			}

			eraseLevelFrame++;
		}
	}

	IEnumerator StartGame() {
		playerObj.setObs(uimanager.getHealthObs());
		uimanager.toggleStartText (false);
		uimanager.togglePlayUI (true);

		// Active camera
		cameraControl.speed = 0.3f;

		// Active player
		player.SetActive (true);

		// Active and start timer
		uimanager.startGame();

		// Active lava
		yield return new WaitForSeconds(2f);
		DynamicMover lavaMover = lava.GetComponent<DynamicMover> ();
		lavaMover.verticalSpeed = 0.3f;

		// Game running
		gameRunning = true;
	}

	public IEnumerator EndGame() {
		// Disable camera scrolling
		cameraControl.setPlayer (null);
		cameraControl.speed = 0.0f;

		// Disable player
		Destroy (player);

		// End game ui
		uimanager.endGame ();

		// Freeze lava
		yield return new WaitForSeconds(1.5f);

		DynamicMover lavaMover = lava.GetComponent<DynamicMover> ();
		lavaMover.verticalSpeed = 0.0f;

		// Show gameover
		uimanager.toggleEndText (true);

		// Set game end
		gameEnd = true;
	}

	public void playerDie() {
		StartCoroutine(controller.EndGame());
	}

	// Get controller instance
	public static GameController GetInstance() {
		return controller;
	}

}
