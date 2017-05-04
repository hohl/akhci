using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour 
{
	public Transform generationPoint;
	public float distanceBetweenPlatforms;

	public GameObject startPlatform;

	private float corridorStart;
	private float corridorWidth;
	private float platformHeight;

	private bool left = false;

	private PlatformGenerator generator;

	// Use this for initialization
	void Start ()
	{
		generator = new PlatformGenerator (GetComponent<AntAlgorithms.AntAlgorithmSimple>());
		CalculateCorridorValues();
		platformHeight = startPlatform.GetComponent<BoxCollider2D>().size.y;
	}

	private void CalculateCorridorValues() 
	{
		GameObject leftWall = GameObject.Find("LeftWall");
		GameObject rightWall = GameObject.Find("RightWall");

		corridorStart = leftWall.transform.position.x + leftWall.transform.localScale.x;
		corridorWidth = (rightWall.transform.position.x - rightWall.transform.localScale.x) - corridorStart;
	}

	void Update()
	{
		if (transform.position.y > generationPoint.position.y) {

			transform.position = new Vector3 (
				transform.position.x,
				transform.position.y - (distanceBetweenPlatforms + platformHeight),
				transform.position.z);

			float xOffset = startPlatform.transform.localScale.x / 2;

			if (left) {
				xOffset *= -1;
				left = false;
			} else {
				left = true;
			}

			Vector3 platformPosition = new Vector3 (
				transform.position.x + xOffset,
				transform.position.y,
				startPlatform.transform.position.z);

			// Cloning the startPlatform at new position
			GameObject newPlatform = Instantiate (
				startPlatform, 
				platformPosition, 
				transform.rotation);
		}
	}
}
