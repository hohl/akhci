using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
	private bool paused = false;
	public GameObject menuPause;
	private Button buttonContinue;
	private Button buttonRestart;


	void Load()
	{
		/*Button[] menuButtons = menuPause.gameObject.GetComponents<Button>();
		foreach(Button button in menuButtons) {
			String buttonName = button.gameObject.name.ToLower();
			if (buttonName.Equals("continue"))
			{
				
			}
		}*/
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			paused = PauseGame();
		}
	}

	public bool PauseGame()
	{
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			menuPause.SetActive(false);
			return false;
		}
		else
		{
			Time.timeScale = 0f;
			menuPause.SetActive(true);
			return true;
		}
	}

	public bool IsPaused()
	{
		return paused;
	}
}