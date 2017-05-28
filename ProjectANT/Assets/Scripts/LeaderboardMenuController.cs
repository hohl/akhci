using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardMenuController : MonoBehaviour {

	public Button buttonBack;

	private List<GameObject> leaderboardCells = new List<GameObject>();
	private GameObject leaderboardHeader;

	private int pageIndex = 0;

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
//		int index = 0;
//		foreach (GameObject cell in leaderboardCells) {
//			index++;
//			cell.GetComponent<LeaderboardItemController> ().textRank.text = index.ToString ();
//		}


		GlobalstatsIO_Leaderboard globalLeaderboard = Leaderboard.Instance.GetScore ();

		for (int i = 0; i < leaderboardCells.Count; i++) {
			if (pageIndex * leaderboardCells.Count + i < globalLeaderboard.data.Length) {

				GameObject cell = leaderboardCells [i];
				LeaderboardItemController cellController = cell.GetComponent<LeaderboardItemController> ();
				GlobalstatsIO_LeaderboardValue leaderboardValue = globalLeaderboard.data [pageIndex * leaderboardCells.Count + i];

				cellController.textName.text = leaderboardValue.name;
				cellController.textRank.text = leaderboardValue.rank;
				cellController.textDistance.text = leaderboardValue.value;
				// TODO: Change this to the real values!!!!
				cellController.textGraph.text = "0";
				cellController.textAlgo.text = "0";

				cell.SetActive (true);
			} else {
				leaderboardCells [i].SetActive (false);
			}
		} 


	}
	
	private void GoBackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}
		
}
