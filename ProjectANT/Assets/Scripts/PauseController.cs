using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
	private bool paused = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			paused = PauseGame();
		}
	}

	void OnGUI()
	{
		if (paused)
		{
			GUILayout.Label("Game is paused!");
			if (GUILayout.Button("Click me to unpause"))
			{
				paused = PauseGame();
			}
		}
	}

	bool PauseGame()
	{
		if (Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return false;
		}
		else
		{
			Time.timeScale = 0f;
			return true;
		}
	}

	public bool IsPaused()
	{
		return paused;
	}
}