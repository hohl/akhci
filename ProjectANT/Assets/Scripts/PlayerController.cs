﻿using System;
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
	public Text textScore;

	private CameraController cameraCont;
	private PauseController pauseCont;
	private Rigidbody2D myRigidbody;
	private Button restartButton;
	private Button menuButton;
	private PlatformController platformCont;
	private int platformScore = 0;
	private Text endTextPlatforms;
	private Text endTextDistance;
	private Text endTextTsp;
	private GameObject endPlatform;
	private HSController hsMuellerController;

	// Use this for initialization
	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		// should we create a gamecontroller for that
		cameraCont = playerCamera.GetComponent<CameraController>();
		pauseCont = GetComponent<PauseController>();
		platformCont = platformControllerObject.GetComponent<PlatformController>();
		endPlatform = FindObjectOfType<EndPlatformController>().gameObject;
		hsMuellerController = GetComponent<HSController>();

		InitiateMenu();
		GlobalstatsIO stats = new GlobalstatsIO();
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

		if (Input.acceleration.x > 0.1f || Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
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
		else if (Input.acceleration.x < -0.1f || Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
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

	IEnumerator OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("End"))
		{
			Debug.Log("DEAD!!!!");
		
			Debug.Log ("Result: " + platformCont.Result);
			cameraCont.SetGameOver(true);
			pauseCont.SetGameOver(true);
			
			yield return SendResults();
		}

		if (other.CompareTag ("Gap"))
		{
			GapController gapController = other.GetComponent<GapController> ();
			if (gapController != null) 
			{
				platformCont.Select (gapController.City);
				platformScore++;
				textScore.text = platformScore.ToString ();
			}
		}
	}

	private IEnumerator SendResults()
	{
		string username = AntPrefs.Instance.GetUsername();
		Debug.Log("Attempt to send result: " + platformCont.Result);
		//Leaderboard.Instance.SubmitResult(username, (float)platformCont.Result, platformCont.GetCurrentTspId(), platformCont.GetGeneratorAlgoId(), platformScore);

		string result = null;
		yield return hsMuellerController.StartPostScoresCoroutine(username, (int)platformCont.Result, platformScore, platformCont.GetCurrentTspName(), platformCont.GetGeneratorAlgoId(), value => result = value);
	}

	public int GetPlatformScore()
	{
		return platformScore;
	}

	private void InitiateMenu()
	{
		Button[] buttons = menuEnd.GetComponentsInChildren<Button> (true);

		foreach(Button button in buttons) {
			String name = button.name;
			if (name != null) {
				if (name.Equals ("RestartButton")) {
					restartButton = button;
					restartButton.onClick.AddListener(RestartGame);
				} else if (name.Equals ("EndMenuButton")) {
					menuButton = button;
					menuButton.onClick.AddListener (GoBackToMenu);
				}
			}
		}
			


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

		// TODO: Submit scores again.
		//       To do that, create a number to indicate Graph and Algo
		//Leaderboard.Instance.SubmitResult ((float)platformCont.Result, 0, 0);

	}

	private void RestartGame()
	{
		//myRigidbody.gameObject.SetActive(false);
		menuEnd.SetActive(false);
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	private void GoBackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}

}
