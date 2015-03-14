using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour, IPlayerObserver {

	private RectTransform health;

	private GameObject warningText;

	private bool playerMelting;
	
	void Start () {
		health = GameObject.Find ("Health").GetComponent<RectTransform> ();
		warningText = GameObject.Find ("HealthBar/Warning");
	}

	public void PlayerUpdate(Players player) {
		float playerSizeAbs = Mathf.Abs (player.transform.localScale.x) - player.minSize.x;
		float maxSize = player.maxSize.x - player.minSize.x;

		float life = playerSizeAbs / maxSize;
		health.sizeDelta = new Vector2(life * 186, health.sizeDelta.y);
	}

	public void PlayerDie() {
		health.sizeDelta = new Vector2(0f, health.sizeDelta.y);
		warningText.SetActive (true);
		warningText.GetComponent<Text> ().text = "Die";
	}

	public void toggleMelting(bool value) {
		if (warningText != null && value != warningText.activeSelf)
			warningText.SetActive (value);
	}

}
