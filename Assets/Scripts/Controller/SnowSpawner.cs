using UnityEngine;
using System.Collections;

public class SnowSpawner : MonoBehaviour {

	public GameObject snowball;
	public float spawnRange;
	
	public int maxLevel;

	// Use this for initialization
	void Start () {
		StartCoroutine(spawnSnowball());
	}
	
	IEnumerator spawnSnowball() {
		GameController controller = GameController.getInstance();
		System.Random rnd = new System.Random();
		
		while (controller.currentLevel < maxLevel && ! controller.isGameEnd()) {
			if (rnd.Next(1, maxLevel) % controller.currentLevel == 0) {
				Instantiate(snowball, new Vector3(
					Random.Range(-spawnRange, spawnRange), Camera.main.transform.position.y + 13.5f, 0.0f),
					Quaternion.identity
				);
			}
			
			yield return new WaitForSeconds(10f);
		}
	}
	
}
