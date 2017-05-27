using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
	private bool paused = false;
	public GameObject menuPause;
	public Button pauseButton; // for now: whole screen

	private Button buttonContinue;
	private Button buttonRestart;
	private Button buttonMenu;
	private bool gameOver = false;

	void Start()
	{
		Button[] menuButtons = menuPause.gameObject.GetComponentsInChildren<Button>(true);
		foreach(Button button in menuButtons) {
			String buttonName = button.gameObject.name;
			if (buttonName.Equals("ContinueButton"))
			{
				buttonContinue = button;
				buttonContinue.onClick.AddListener(PauseGame);
			}
			else if(buttonName.Equals("Restart"))
			{
				buttonRestart = button;
				buttonRestart.onClick.AddListener(RestartCurrentScene);
			}
			else if(buttonName.Equals("PauseMenuButton"))
			{
				buttonMenu = button;
				buttonMenu.onClick.AddListener(GoBackToMenu);
			}
		}



		pauseButton.onClick.AddListener(PauseGame);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
		{
			PauseGame();
		}
	}

	public void PauseGame()
	{
		if (!gameOver)
		{
			if (paused)
			{
				Time.timeScale = 1f;
				paused = false;
			}
			else
			{
				Time.timeScale = 0f;
				paused = true;
			}

			menuPause.SetActive(paused);
			pauseButton.gameObject.SetActive(!paused);
		}
	}

	public void RestartCurrentScene()
	{
		PauseGame(); // unpause
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}

	public bool IsPaused()
	{
		return paused;
	}

	public void SetGameOver(bool gameOver)
	{
		this.gameOver = gameOver;
		pauseButton.gameObject.SetActive(false);
	}

	private void GoBackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}
}