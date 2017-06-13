using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LeaderboardMenuController : MonoBehaviour {

	public Button buttonBack;
	public Button buttonNextPage;
	public Button buttonPreviousPage;

	private List<GameObject> leaderboardCells = new List<GameObject>();
	private GameObject leaderboardHeader;

	private int pageIndex = 0;
	private GlobalstatsIO_Leaderboard globalLeaderboard;

	void Start () {


		buttonBack.onClick.AddListener(GoBackToMenu);

		leaderboardHeader = GameObject.FindGameObjectWithTag ("LeaderboardHeader");

		// Getting the Leaderboard cells
		GameObject[] cells = GameObject.FindGameObjectsWithTag ("LeaderboardCell");

		foreach (GameObject cell in cells) {

			leaderboardCells.Add (cell);
		}

		leaderboardCells.Sort ((x, y) => (y.transform.localPosition.y.CompareTo(x.transform.localPosition.y)));


		globalLeaderboard = Leaderboard.Instance.GetScore ();
		FillLeaderboard ();

		buttonPreviousPage.onClick.AddListener(PreviousPage);
		buttonNextPage.onClick.AddListener(NextPage);

		buttonPreviousPage.gameObject.SetActive (false);

		if (globalLeaderboard.data.Length <= leaderboardCells.Count) {
			buttonNextPage.gameObject.SetActive(false);
		}
	}

	private void FillLeaderboard() {

		if (globalLeaderboard.data == null) {
			return;
		}

		for (int i = 0; i < leaderboardCells.Count; i++) {
			if (pageIndex * leaderboardCells.Count + i < globalLeaderboard.data.Length) {

				GameObject cell = leaderboardCells [i];
				LeaderboardItemController cellController = cell.GetComponent<LeaderboardItemController> ();
				GlobalstatsIO_LeaderboardValue leaderboardValue = globalLeaderboard.data [pageIndex * leaderboardCells.Count + i];

				cellController.textName.text = leaderboardValue.name;
				cellController.textRank.text = leaderboardValue.rank;
				cellController.textDistance.text = leaderboardValue.value;

				foreach(GlobalstatsIO_Additional additional in leaderboardValue.additionals) {

					if (additional.key == "graph") {
						cellController.textGraph.text = additional.value;
					} else if (additional.key == "algo") {
						cellController.textAlgo.text = additional.value;
					}
				}

				cell.SetActive (true);
			} else {
				leaderboardCells [i].SetActive (false);
			}
		} 
	}

	private void FillLeaderboardMueller()
	{

		if (globalLeaderboard.data == null)
		{
			return;
		}

		for (int i = 0; i < leaderboardCells.Count; i++)
		{
			if (pageIndex * leaderboardCells.Count + i < globalLeaderboard.data.Length)
			{

				GameObject cell = leaderboardCells[i];
				LeaderboardItemController cellController = cell.GetComponent<LeaderboardItemController>();
				GlobalstatsIO_LeaderboardValue leaderboardValue = globalLeaderboard.data[pageIndex * leaderboardCells.Count + i];

				cellController.textName.text = leaderboardValue.name;
				cellController.textRank.text = leaderboardValue.rank;
				cellController.textDistance.text = leaderboardValue.value;

				foreach (GlobalstatsIO_Additional additional in leaderboardValue.additionals)
				{

					if (additional.key == "graph")
					{
						cellController.textGraph.text = additional.value;
					}
					else if (additional.key == "algo")
					{
						cellController.textAlgo.text = additional.value;
					}
				}

				cell.SetActive(true);
			}
			else
			{
				leaderboardCells[i].SetActive(false);
			}
		}
	}


	private void GoBackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}

	private void NextPage() {

		pageIndex++;
		FillLeaderboard ();
		buttonPreviousPage.gameObject.SetActive (true);
	}

	private void PreviousPage() {

		pageIndex--;
		FillLeaderboard ();

		if (pageIndex == 0) {
			buttonPreviousPage.gameObject.SetActive (false);
		}
	}

}
