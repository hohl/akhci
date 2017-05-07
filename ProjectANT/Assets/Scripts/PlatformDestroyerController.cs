using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Collider2D[] colliders = new Collider2D[20];
		int colliderCount = GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
		for (int i = 0; i < colliderCount; i++) {
			Destroy(colliders[i].gameObject);
		}
	}
}
