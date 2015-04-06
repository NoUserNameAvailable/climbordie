using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	// For unity editor (because editor doesn't support dictionary)
	public List<GameObject> prefabs;
	public List<int> levels;
	
	// Current level
	public int currentLevel = 1;

	// Max diffilculty
	public int difficultyMax;

	// Height of one prefab
	public float prefabHeight;

	// First level prefab
	public GameObject firstLevel;
	
	private Dictionary<int, List<GameObject>> levelPrefabs;
	private	GameObject lastLevel;

	// Use this for initialization
	void Start () {
		if (levels.Count != prefabs.Count)
			throw new UnityException("[GameController] Size of levels list must be same than prefabs list size.");

		levelPrefabs = new Dictionary<int, List<GameObject>>();

		// Level indexation
		for (int i = 0; i < prefabs.Count; i++) {
			int lvl = levels[i];

			if (levelPrefabs.ContainsKey(lvl)) {
				levelPrefabs[lvl].Add(prefabs[i]);
			}
			else {
				List<GameObject> prefabList = new List<GameObject>();
				prefabList.Add(prefabs[i]);
				levelPrefabs.Add(lvl, prefabList);
			}
		}

		// Set last level has first
		lastLevel = firstLevel;
	}

	void FixedUpdate() {
		Players player = GameController.getInstance ().getPlayer ();

		if (player != null) {
			while (player.gameObject.transform.position.y + (2 * prefabHeight) > lastLevel.transform.position.y) {
				System.Random rnd = new System.Random ();
				addNextLevel (currentLevel, rnd.NextDouble ());
			}
		}
	}
	
	public void addNextLevel(int level, double seed) {
		GameObject prefab;

		int difficulty = (currentLevel > difficultyMax) ? difficultyMax : currentLevel;

		if (seed > 0.15 && seed <= 0.35 && difficulty > 1) {
			prefab = getRandomPrefab (difficulty - 1);
		} else {
			prefab = getRandomPrefab (difficulty);
		}

		GameObject nextLevel = Instantiate (prefab) as GameObject;
		nextLevel.transform.position = new Vector3 (0f, lastLevel.transform.position.y + prefabHeight, 0f);
		lastLevel = nextLevel;
	}

	GameObject getRandomPrefab(int level) {
		GameObject prefab;
		
		System.Random rnd = new System.Random ();
		
		if (levelPrefabs.ContainsKey(level)) {
			int prefabIndex = rnd.Next(0, levelPrefabs[level].Count);
			
			if (levelPrefabs[level][prefabIndex].name != lastLevel.name)
				prefab = levelPrefabs[level][prefabIndex];
			else
				prefab = getRandomPrefab(level);
		} else {
			prefab = getRandomPrefab(level - 1);
		}
		
		return prefab;
	}

	public GameObject getLastLevel() {
		return lastLevel;
	}
	
}
