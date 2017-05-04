using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;

	private PlayerController player;
	private Vector3 lastPlayerPosition;

//	private GameObject leftWall;
//	private GameObject rightWall;
	public GameObject endCollider;

	// should we create a game/levelcontroller for that?
	private bool gameOver = false;

	void Start () {
		player = FindObjectOfType<PlayerController> ();
//		leftWall = GameObject.Find("LeftWall");
//		rightWall = GameObject.Find("RightWall");

		endCollider = GameObject.Find("EndPlatform");
	}
	

//	// Auto Moving Camera
//	void Update() {
//		if (!gameOver)
//		{
//			MoveGameObjectDown(this.gameObject);
//			MoveGameObjectDown(leftWall);
//			MoveGameObjectDown(rightWall);
//		}
//	}
//
	private void MoveGameObjectDown(GameObject go) {

		go.transform.position = new Vector3 (
			go.transform.position.x,
			go.transform.position.y - cameraSpeed * Time.deltaTime,
			go.transform.position.z);
		
	}

	public void SetGameOver(bool over)
	{
		gameOver = over;
	}

	public bool IsGameOver()
	{
		return gameOver;
	}

//	 Camera that follows the character
		void Update () {
	
		MoveGameObjectDown (endCollider);

		if (!gameOver) {
			float distanceToMove = player.transform.position.y - lastPlayerPosition.y;
	

			transform.position = new Vector3 (transform.position.x, transform.position.y + distanceToMove * Time.deltaTime, transform.position.z);

			lastPlayerPosition = transform.position;



		}
	}
}
