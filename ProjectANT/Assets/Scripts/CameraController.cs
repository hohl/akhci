using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;

//	private PlayerController player;
//	private Vector3 lastPlayerPosition;

	private GameObject leftWall;
	private GameObject rightWall;
	public GameObject endCollider;

	void Start () {
		//player = FindObjectOfType<PlayerController> ();
		leftWall = GameObject.Find("LeftWall");
		rightWall = GameObject.Find("RightWall");
	}
	

	// Auto Moving Camera
	void Update() {

		MoveGameObjectDown (this.gameObject);
		MoveGameObjectDown (leftWall);
		MoveGameObjectDown (rightWall);
	}

	private void MoveGameObjectDown(GameObject go) {

		go.transform.position = new Vector3 (
			go.transform.position.x,
			go.transform.position.y - cameraSpeed * Time.deltaTime,
			go.transform.position.z);
		
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
}
