using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlatformController : MonoBehaviour {

	private GameObject endCollider;

	public float platformSpeed;
	private GameObject movingPoint;

	private PlayerController playerController;

	private int scoreAtLastSpeedup;

	// Use this for initialization
	void Start () {
		endCollider = GameObject.Find("EndPlatform");
		movingPoint = GameObject.Find ("MaxEndPlatformDistance");

		playerController = GameObject.Find ("Player").GetComponent<PlayerController> ();

		scoreAtLastSpeedup = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (movingPoint.transform.position.y > endCollider.transform.position.y) {
			endCollider.transform.position = new Vector3 (
				endCollider.transform.position.x,
				endCollider.transform.position.y - platformSpeed * Time.deltaTime,
				endCollider.transform.position.z);
		} else {
			endCollider.transform.position = new Vector3 (
				endCollider.transform.position.x, 
				movingPoint.transform.position.y - movingPoint.transform.localScale.y, 
				endCollider.transform.position.z);
		}

		int score = playerController.GetPlatformScore ();
		if (score > scoreAtLastSpeedup && score % 20 == 0) {
			platformSpeed *= 1.2f;
			scoreAtLastSpeedup = score;
			Debug.Log ("SPEEED " + platformSpeed);
		}
			
	}
}
