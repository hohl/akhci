using System;
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

	private PlatformGenerator generator;

	// Use this for initialization
	void Start ()
	{
		//generator = new SimplePlatformGenerator (GetComponent<AntAlgorithms.AntAlgorithmSimple>());
		generator = new RandomPlatformGenerator ();
		CalculateCorridorValues();
		platformHeight = startPlatform.GetComponent<BoxCollider2D>().size.y;
	}

	private void CalculateCorridorValues() 
	{
		GameObject leftWall = GameObject.Find("LeftWall");
		GameObject rightWall = GameObject.Find("RightWall");
		corridorStart = leftWall.transform.position.x + leftWall.transform.localScale.x / 2.0f;
		corridorWidth = (rightWall.transform.position.x - rightWall.transform.localScale.x / 2.0f) - corridorStart;
	}

	void Update()
	{
		if (transform.position.y > generationPoint.position.y)
		{
			var next = generator.Next ();

			transform.position = new Vector3 (
				transform.position.x,
				transform.position.y - (distanceBetweenPlatforms + platformHeight),
				transform.position.z);

			float x1 = 0.0f;
			float width1 = next.first - next.firstWidth / 2;
			CreatePlatform (x1, width1);

			float x2 = next.first + next.firstWidth / 2;
			float width2 = (next.second - next.secondWidth / 2) - x2;
			CreatePlatform (x2, width2);

			float x3 = next.second + next.secondWidth / 2;
			float width3 = 1.0f - x3;
			CreatePlatform (x3, width3);
		}
	}

	private void CreatePlatform (float x, float width)
	{
		Vector3 middlePlatformPosition = new Vector3 (corridorStart + x * corridorWidth + width * corridorWidth / 2, transform.position.y, startPlatform.transform.position.z);
		GameObject middlePlatform = Instantiate (startPlatform, middlePlatformPosition, transform.rotation);
		middlePlatform.name = String.Format ("Platform(x: {0}, width: {1})", x, width);
		middlePlatform.transform.localScale = new Vector3 (width * corridorWidth, startPlatform.transform.localScale.y, startPlatform.transform.localScale.z);
	}
}
