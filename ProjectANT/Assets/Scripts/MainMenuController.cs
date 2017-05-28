using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController: MonoBehaviour {

	public Button buttonStart;
	public Button buttonLeaderboard;

	// Use this for initialization
	void Start () {
		buttonStart.onClick.AddListener(StartGame);
		buttonLeaderboard.onClick.AddListener(ShowLeaderboard);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartGame() {
		SceneManager.LoadScene("GameScene");
	}

	void ShowLeaderboard() {
		SceneManager.LoadScene("LeaderboardScene");
	}
}
