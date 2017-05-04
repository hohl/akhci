using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
	private bool paused = false;
	public GameObject menuPause;
	private Button buttonContinue;
	private Button buttonRestart;

	void Start()
	{
		Button[] menuButtons = menuPause.gameObject.GetComponentsInChildren<Button>(true);
		foreach(Button button in menuButtons) {
			String buttonName = button.gameObject.name.ToLower();
			if (buttonName.Equals("continue"))
			{
				buttonContinue = button;
			}
			else if(buttonName.Equals("restart"))
			{
				buttonRestart = button;
			}
		}

		buttonContinue.onClick.AddListener(PauseGame);
		buttonRestart.onClick.AddListener(RestartCurrentScene);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseGame();
		}
	}

	public void PauseGame()
	{
		if (paused)
		{
			Time.timeScale = 1f;
			menuPause.SetActive(false);
			paused = false;
		}
		else
		{
			Time.timeScale = 0f;
			menuPause.SetActive(true);
			paused = true;
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
}