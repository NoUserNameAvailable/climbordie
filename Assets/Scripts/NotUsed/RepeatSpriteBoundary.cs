using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class RepeatSpriteBoundary : MonoBehaviour {

	float gridX = 0.0f;
	float gridY = 0.0f;

	SpriteRenderer sprite;

	void Awake() {
		repeatSprite ();
	}

	void repeatSprite() {
		// Get current sprite
		sprite = GetComponent<SpriteRenderer> ();
		
		Vector2 spriteSize = new Vector2 (
			sprite.bounds.size.x / transform.localScale.x,
			sprite.bounds.size.y / transform.localScale.y
			);
		
		Vector3 scale = new Vector3(1.0f, 1.0f, 1.0f);
		
		if (0.0f != gridX) {
			float width_wu = sprite.bounds.size.x / gridX;
			scale.x = width_wu / spriteSize.x;
			spriteSize.x = width_wu;
		}
		if (0.0f != gridY) {
			float height_wu = sprite.bounds.size.y / gridY;
			scale.y = height_wu / spriteSize.y;
			spriteSize.y = height_wu;
		}
		
		// Create child prefab
		GameObject childPrefab = new GameObject ();
		SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer> ();
		childPrefab.transform.position = transform.position;
		childSprite.sprite = sprite.sprite;
		
		// Repeat background
		GameObject child;
		float i = 0.0f;
		float j = 0.0f;
		
		while (i * spriteSize.y < sprite.bounds.size.y) {
			j = 0.0f;
			while (j * spriteSize.x < sprite.bounds.size.x) {
				child = Instantiate(childPrefab) as GameObject;
				child.transform.position = transform.position - (new Vector3(-spriteSize.x * j, spriteSize.y * i, 0));
				child.transform.localScale = scale;
				child.transform.parent = transform;
				j++;
			}
			i++;
		}
		
		Destroy(childPrefab);
		sprite.enabled = false;
	}

}
