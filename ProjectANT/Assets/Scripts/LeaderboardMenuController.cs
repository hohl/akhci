using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardMenuController : MonoBehaviour {

	public Button buttonBack;

	private List<GameObject> leaderboardCells = new List<GameObject>();
	private GameObject leaderboardHeader;

	void Start () {


		buttonBack.onClick.AddListener(GoBackToMenu);

		leaderboardHeader = GameObject.FindGameObjectWithTag ("LeaderboardHeader");

		// Getting the Leaderboard cells
		GameObject[] cells = GameObject.FindGameObjectsWithTag ("LeaderboardCell");

		foreach (GameObject cell in cells) {

			leaderboardCells.Add (cell);
		}

		leaderboardCells.Sort ((x, y) => (y.transform.localPosition.y.CompareTo(x.transform.localPosition.y)));


		// Dummy to demonstrate that this actually works :D
		int index = 0;
		foreach (GameObject cell in leaderboardCells) {
			index++;
			cell.GetComponent<LeaderboardItemController> ().textRank.text = index.ToString ();
		}
	}
	
	private void GoBackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}
		
}
