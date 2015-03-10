using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour {

	// Game
	private GameObject startText;
	private GameObject endText;

	// Stats
	private Timer timer;
	private GameObject levelCount;
	private GameObject meterCount;

	// Health
	private Healthbar health;

	// Use this for initialization
	void Awake () {
		startText = GameObject.Find ("StartText");
		endText = GameObject.Find ("EndText");

		timer = GameObject.Find ("Timer").GetComponent<Timer> ();
		levelCount = GameObject.Find ("LevelCount");
		meterCount = GameObject.Find ("MeterCount");

		health = GameObject.Find ("HealthBar").GetComponent<Healthbar> ();

		// Hide UI
		toggleStartText (false);
		togglePlayUI (false);
		toggleEndText (false);
	}
	
	public void updateLevel(int level) {
		levelCount.GetComponent<Text> ().text = "Level " + level;
	}

	public void updateMeter(int meter) {
		meterCount.GetComponent<Text> ().text = meter + "m";
	}

	public void toggleStartText(bool enabled) {
		startText.SetActive (enabled);
	}

	public void togglePlayUI(bool enabled) {
		timer.gameObject.SetActive (enabled);
		levelCount.SetActive (enabled);
		meterCount.SetActive (enabled);
		health.gameObject.SetActive (enabled);
	}

	public void toggleEndText(bool enabled) {
		endText.SetActive (enabled);
	}

	public void startTimer() {
		timer.stop = false;
	}

	public void stopTimer() {
		timer.stop = true;
	}

	public void playerDie() {
		health.PlayerDie ();
	}

	public IPlayerObserver getHealthObs() {
		return (IPlayerObserver)health;
	}

}
