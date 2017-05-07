using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed;
	public float jumpForce;
	public GameObject playerCamera;
	public GameObject menuEnd;
	public GameObject platformControllerObject;

	private CameraController cameraCont;
	private PauseController pauseCont;
	private Rigidbody2D myRigidbody;
	private Button restartButton;
	private PlatformController platformCont;
	private int platformScore = 0;
	private Text endTextPlatforms;
	private Text endTextDistance;
	private Text endTextTsp;
	private GameObject endPlatform;

	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		// should we create a gamecontroller for that
		cameraCont = playerCamera.GetComponent<CameraController>();
		pauseCont = GetComponent<PauseController>();
		platformCont = platformControllerObject.GetComponent<PlatformController>();
		endPlatform = FindObjectOfType<EndPlatformController>().gameObject;

		InitiateMenu();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!pauseCont.IsPaused())
		{
			if (!cameraCont.IsGameOver())
			{
				MoveCharacter();
			}
			else
			{
				myRigidbody.gameObject.SetActive(false);
				endPlatform.SetActive(false);
				ShowEndMenu();
			}
		}
	}

	private void MoveCharacter()
	{
		int direction = 0;

		if (Input.acceleration.x > 0.1f || Input.GetKey("d"))
		{
			if (myRigidbody.velocity.x < 0) 
			{
				direction = 2;
			} 
			else 
			{
				direction = 1;
			}
		}
		else if (Input.acceleration.x < -0.1f || Input.GetKey("a"))
		{
			if (myRigidbody.velocity.x > 0) {
				direction = -2;
			} else {
				direction = -1;
			}
		}

		Vector2 movement = new Vector2(direction, 0);
		myRigidbody.AddForce(movement * movementSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("End"))
		{
			Debug.Log("DEAD!!!!");
			Debug.Log ("Result: " + platformCont.Result);
			cameraCont.SetGameOver(true);
			pauseCont.SetGameOver(true);
		}

		if (other.CompareTag ("Gap"))
		{
			GapController gapController = other.GetComponent<GapController> ();
			if (gapController != null) 
			{
				platformCont.Select (gapController.City);
				platformScore++;
			}
		}
	}

	public int GetPlatformScore()
	{
		return platformScore;
	}

	private void InitiateMenu()
	{
		restartButton = menuEnd.GetComponentInChildren<Button>(true);
		restartButton.onClick.AddListener(RestartGame);
		Text[] textViews = menuEnd.GetComponentsInChildren<Text>(true);
		foreach (Text view in textViews)
		{
			String name = view.name;
			if (name != null)
			{
				if (name.Equals("PlatformsPlayer"))
				{
					endTextPlatforms = view;
				}
				else if (name.Equals("DistancePlayer"))
				{
					endTextDistance = view;
				}
				else if (name.Equals("TspPlayer"))
				{
					endTextTsp = view;
				}
			}
		}
	}

	private void ShowEndMenu()
	{
		endTextPlatforms.text = GetPlatformScore() + "";
		endTextDistance.text = platformCont.Result + "";
		endTextTsp.text = platformCont.GetCurrentTspName();
		menuEnd.SetActive(true);
	}

	private void RestartGame()
	{
		//myRigidbody.gameObject.SetActive(false);
		menuEnd.SetActive(false);
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}
}
