using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatformController : MonoBehaviour {

	public GameObject endCollider;

	public float platformSpeed;

	private bool moveDown = true;
	private GameObject mainCamera;
	private GameObject movingPoint;


	// Use this for initialization
	void Start () {
		endCollider = GameObject.Find("EndPlatform");	
		mainCamera = GameObject.Find ("Main Camera");
		movingPoint = GameObject.Find ("MaxEndPlatformDistance");
		moveDown = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (movingPoint.transform.position.y > endCollider.transform.position.y) {
			endCollider.transform.position = new Vector3 (
				endCollider.transform.position.x,
				endCollider.transform.position.y - platformSpeed * Time.deltaTime,
				endCollider.transform.position.z);
		} else {

			endCollider.transform.position = new Vector2 (
				endCollider.transform.position.x, 
				movingPoint.transform.position.y - movingPoint.transform.localScale.y);
		}
	}

	private void MoveGameObjectDown(GameObject go) {



	}

//	void OnTriggerEnter2D(Collider2D other) {
//
//		if (other.gameObject.name == "MaxEndPlatformDistance") {
//			moveDown = false;
//		}
//		
//	}
//	void OnTriggerExit2D(Collider2D other) {
//		if (other.gameObject.name == "MaxEndPlatformDistance") {
//			moveDown = true;
//		}
//	}
}
