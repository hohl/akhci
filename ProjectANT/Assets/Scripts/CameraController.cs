using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public PlayerController player;

	private Vector3 lastPlayerPosition;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		float distanceToMove = player.transform.position.y - lastPlayerPosition.y;

		transform.position = new Vector3 (transform.position.x, transform.position.y + distanceToMove, transform.position.z);

		lastPlayerPosition = player.transform.position;
	}
}
