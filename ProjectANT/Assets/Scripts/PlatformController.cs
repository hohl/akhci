using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour 
{
	public Transform generationPoint;
	public float distanceBetweenPlatforms;

	public GameObject startPlatform;
	public GameObject startTrigger;

	private float corridorStart;
	private float corridorWidth;
	private float platformHeight;

	private PlatformGenerator generator;

	public void Select(City city)
	{
		if (city != null) // dummy layers just have the city set to null, just ignore them!
		{
			generator.SelectedCity = city;
		}
	}

	void Start ()
	{
		generator = new SimplePlatformGenerator (GetComponent<AntAlgorithms.AntAlgorithmSimple>());
		//generator = new RandomPlatformGenerator ();
		CalculateCorridorValues();
		platformHeight = startPlatform.GetComponent<BoxCollider2D>().size.y;
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

			CreateTrigger (next.first, next.firstWidth, next.firstCity);

			float x2 = next.first + next.firstWidth / 2;
			float width2 = (next.second - next.secondWidth / 2) - x2;
			CreatePlatform (x2, width2);

			CreateTrigger (next.second, next.secondWidth, next.secondCity);

			float x3 = next.second + next.secondWidth / 2;
			float width3 = 1.0f - x3;
			CreatePlatform (x3, width3);
		}
	}

	private void CalculateCorridorValues() 
	{
		GameObject leftWall = GameObject.Find("LeftWall");
		GameObject rightWall = GameObject.Find("RightWall");
		corridorStart = leftWall.transform.position.x + leftWall.transform.localScale.x / 2.0f;
		corridorWidth = (rightWall.transform.position.x - rightWall.transform.localScale.x / 2.0f) - corridorStart;
	}

	private void CreatePlatform (float x, float width)
	{
		Vector3 position = new Vector3 (corridorStart + x * corridorWidth + width * corridorWidth / 2, transform.position.y, startPlatform.transform.position.z);
		GameObject platform = Instantiate (startPlatform, position, transform.rotation);
		platform.name = String.Format ("Platform(x: {0}, width: {1})", x, width);
		platform.transform.localScale = new Vector3 (width * corridorWidth, startPlatform.transform.localScale.y, startPlatform.transform.localScale.z);
	}

	private void CreateTrigger (float x, float width, City city)
	{
		Vector3 position = new Vector3 (corridorStart + (x - width / 2) * corridorWidth + width * corridorWidth / 2, transform.position.y, startPlatform.transform.position.z);
		GameObject trigger = Instantiate (startTrigger, position, transform.rotation);
		trigger.name = String.Format ("Triger(pos: {0}, city: {2})", x, width, city == null ? -1 : city.getId());
		trigger.transform.localScale = new Vector3 (width * corridorWidth, startPlatform.transform.localScale.y, startPlatform.transform.localScale.z);
		GapController gapCont = trigger.AddComponent<GapController> ();
		gapCont.City = city;
	}
}
