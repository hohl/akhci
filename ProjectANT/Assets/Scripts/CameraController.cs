using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;

	private PlayerController player;
	private Vector3 lastPlayerPosition;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Camera that follows the character
//	void Update () {
//
//		float distanceToMove = player.transform.position.y - lastPlayerPosition.y;
//
//		transform.position = new Vector3 (transform.position.x, transform.position.y + distanceToMove, transform.position.z);
//
//		lastPlayerPosition = player.transform.position;
//	}

	void Update() {

		transform.position = new Vector3 (transform.position.x, transform.position.y - cameraSpeed * Time.deltaTime, transform.position.z);
	}
}
