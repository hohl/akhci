using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private PlayerController player;
	private Vector3 lastPlayerPosition;

	private GameObject movingPoint;

	// should we create a game/levelcontroller for that?
	private bool gameOver = false;

	void Start () {
		player = FindObjectOfType<PlayerController> ();
		movingPoint = GameObject.Find ("MaxEndPlatformDistance");
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

		if (!gameOver) {
			float distanceToMove = player.transform.position.y - lastPlayerPosition.y;

			transform.position = new Vector3 (transform.position.x, transform.position.y + distanceToMove * Time.deltaTime, transform.position.z);
			lastPlayerPosition = transform.position;

			movingPoint.transform.position = new Vector3 (
				movingPoint.transform.position.x, 
				movingPoint.transform.position.y + distanceToMove * Time.deltaTime, 
				movingPoint.transform.position.z);
		}
	}
}
