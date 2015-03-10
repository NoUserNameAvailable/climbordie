using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Player
	public GameObject playerPrefab;
	public float meltingDistance;

	private Players player;
	private Vector3 spawner;

	// Lava
	public GameObject lavaPrefab;
	private Lava lava;
	private DynamicMover lavaMover;

	// GUI
	public GameObject guiPrefab;
	private GameUIManager uiManager;

	// Camera
	private CameraController cameraControl;

	// Game states
	private bool gameRunning = false;
	private bool gameEnd = false;

	private int nextLevel;
	private int meter;

	public int currentLevel;
	public int meterPerLevel;
	public float scrollSpeed;

	// Instance
	private static GameController instance;

	// Use for initialization
	void Awake () {
		// Set instance (only one instance can exist)
		if (instance != null)
			return;
		else
			instance = this;

		// Add GUI
		GameObject ui = Instantiate (guiPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		uiManager = ui.GetComponent<GameUIManager> ();

		// Add lava
		RectTransform lavaRect = lavaPrefab.GetComponent<RectTransform> ();
		GameObject lavaObj = Instantiate (lavaPrefab, lavaRect.position, lavaRect.rotation) as GameObject;
		lava = lavaObj.GetComponent<Lava> ();
		lavaMover = lavaObj.GetComponent<DynamicMover> ();

		// Find camera
		cameraControl = Camera.main.GetComponent<CameraController> ();

		// Set spawn coord
		GameObject spawn = GameObject.Find ("PlayerSpawn");
		spawner = spawn.transform.position;
		spawn.SetActive (false);

		// Initializa game state
		nextLevel = meterPerLevel;

		// Active start text
		uiManager.toggleStartText (true);
	}

	// Update is called once per frame
	void Update() {
		// Start game
		if (! gameRunning && Input.GetKey (KeyCode.Return))
			StartCoroutine (start());

		// Restart game
		if (gameEnd && Input.GetKeyDown (KeyCode.R))
			Application.LoadLevel (Application.loadedLevel);

		if (gameRunning && player != null) {
			// Update meter
			if (player.gameObject.transform.position.y > meter) {
				meter = (int) player.gameObject.transform.position.y;
				uiManager.updateMeter(meter);
			}

			// Update current level
			if (meter >= nextLevel) {
				currentLevel++;
				nextLevel += meterPerLevel;

				GetComponent<LevelManager>().currentLevel = currentLevel;
				uiManager.updateLevel(currentLevel);

				// Increase scrolling speed

			}

			// Melting player
			if (player.gameObject.transform.position.y - lava.transform.position.y < meltingDistance)
				player.melting();
		}

	}

	IEnumerator start() {
		// Toggle ui
		uiManager.toggleStartText (false);
		uiManager.togglePlayUI (true);

		// Spawn player
		GameObject playerObj = Instantiate (playerPrefab, spawner, Quaternion.identity) as GameObject;

		player = playerObj.GetComponent<Players> ();
		player.setObs (uiManager.getHealthObs ());

		// Start timer
		uiManager.startTimer ();

		// Set game running status
		gameRunning = true;

		// Wait few second before start scrolling
		yield return new WaitForSeconds (3f);
		setScrollingSpeed (scrollSpeed);
	}

	void end() {
		// Disable scrolling
		setScrollingSpeed (0.0f);

		// Destroy player
		Destroy (player.gameObject);

		// Stop timer and show gameover
		uiManager.stopTimer ();
		uiManager.toggleEndText (true);

		// Set game ending status
		gameEnd = true;
	}

	public void playerDie() {
		end ();
	}

	public void setScrollingSpeed(float speed) {
		lavaMover.verticalSpeed = speed;
		cameraControl.speed = speed;
	}

	public static GameController getInstance() {
		return instance;
	}

	public bool isGameRunning() {
		return gameRunning;
	}

	public bool isGameEnd() {
		return gameEnd;
	}

	public Players getPlayer() {
		return player;
	}

	public int getMeter() {
		return meter;
	}

}
