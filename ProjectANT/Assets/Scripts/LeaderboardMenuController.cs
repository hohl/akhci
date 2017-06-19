using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LeaderboardMenuController : MonoBehaviour {

	public Button buttonBack;
	public Button buttonNextPage;
	public Button buttonPreviousPage;
	public InputField inputFieldName;
	public Button buttonSendName;
	public Dropdown dropdownGraph;

	private List<GameObject> leaderboardCells = new List<GameObject>();
	private GameObject leaderboardHeader;

	private int pageIndex = 0;
	private GlobalstatsIO_Leaderboard globalLeaderboard;
	private HSMuellerLeaderboard muellerLeaderboard;
	private HSController hsMuellerController;

	// keep IEnumerator for now as signature
	IEnumerator Start () {
		if (AntPrefs.Instance.IsUsernameStored())
		{
			inputFieldName.text = AntPrefs.Instance.GetUsername();
		}

		buttonBack.onClick.AddListener(GoBackToMenu);
		SetUpDropdown();
		leaderboardHeader = GameObject.FindGameObjectWithTag ("LeaderboardHeader");

		// Getting the Leaderboard cells
		GameObject[] cells = GameObject.FindGameObjectsWithTag ("LeaderboardCell");

		foreach (GameObject cell in cells) {

			leaderboardCells.Add (cell);
		}

		leaderboardCells.Sort ((x, y) => (y.transform.localPosition.y.CompareTo(x.transform.localPosition.y)));


		globalLeaderboard = Leaderboard.Instance.GetScore ();
		FillLeaderboard ((dropdownGraph.options[dropdownGraph.value]).text);

		buttonPreviousPage.onClick.AddListener(PreviousPage);
		buttonNextPage.onClick.AddListener(NextPage);
		buttonSendName.onClick.AddListener(SetUserName);

		buttonPreviousPage.gameObject.SetActive (false);

		if (globalLeaderboard.data.Length <= leaderboardCells.Count) {
			buttonNextPage.gameObject.SetActive(false);
		}

		// For now, don't show mueller leaderboard
		// TODO: make user choose graph
		hsMuellerController = GetComponent<HSController>();
		/*string result = null;
		yield return hsMuellerController.StartGetScoresCoroutine("test8", 100, value => result = value);
		print("The result was:" + result);
		muellerLeaderboard = new HSMuellerLeaderboard(result);
		FillMuellerLeaderboard();*/
		yield return null;
	}

	private void SetUpDropdown()
	{
		dropdownGraph.ClearOptions();
		List<string> tspNames = new List<string>();
		Dictionary<int, TspInfo> tspInfos = TspLoader.Instance.GetAllTSPs();
		foreach (KeyValuePair<int, TspInfo> tspInfo in tspInfos)
		{
			tspNames.Add(tspInfo.Value.GetName());
		}
		tspNames.Sort();
		dropdownGraph.AddOptions(tspNames);
		dropdownGraph.RefreshShownValue();

		dropdownGraph.onValueChanged.AddListener(delegate {
			OnSetTsp(dropdownGraph);
		});

	}

	private void OnSetTsp(Dropdown dropdownGraph)
	{
		FillLeaderboard((dropdownGraph.options[dropdownGraph.value]).text);
	}

	private void SetUserName()
	{
		AntPrefs.Instance.StoreUsername(inputFieldName.text);
	}

	private void FillMuellerLeaderboard()
	{
		for (int i = 0; i < leaderboardCells.Count; i++)
		{
			if (pageIndex * leaderboardCells.Count + i < globalLeaderboard.data.Length)
			{
				LeaderboardEntry entry = muellerLeaderboard.Data[pageIndex * leaderboardCells.Count + i];
				SetLeaderboardEntryToUi(leaderboardCells[i], entry);
			}
			else
			{
				leaderboardCells[i].SetActive(false);
			}
		}
	}

	private void FillLeaderboard(string graph) {
		if (globalLeaderboard.data == null) {
			return;
		}

		for (int i = 0; i < leaderboardCells.Count; i++)
		{
			if (pageIndex * leaderboardCells.Count + i < globalLeaderboard.data.Length)
			{
				GlobalstatsIO_LeaderboardValue leaderboardValue = globalLeaderboard.data [pageIndex * leaderboardCells.Count + i];
				SetLeaderboardEntryToUi(leaderboardCells[i], new LeaderboardEntry(leaderboardValue));
			}
			else
			{
				leaderboardCells [i].SetActive (false);
			}
		} 
	}

	private void SetLeaderboardEntryToUi(GameObject cell, LeaderboardEntry entry)
	{
		LeaderboardItemController cellController = cell.GetComponent<LeaderboardItemController>();
		cellController.textName.text = entry.Name;
		cellController.textRank.text = entry.Rank.ToString();
		cellController.textDistance.text = entry.Distance.ToString();
		cellController.textGraph.text = entry.Graph;
		cellController.textAlgo.text = entry.Algo;

		cell.SetActive(true);
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
		FillLeaderboard ((dropdownGraph.options[dropdownGraph.value]).text);

		if ((pageIndex + 1) * leaderboardCells.Count >= globalLeaderboard.data.Length) {
			buttonNextPage.gameObject.SetActive (false);
		}
		buttonPreviousPage.gameObject.SetActive (true);
	}

	private void PreviousPage() {

		pageIndex--;
		FillLeaderboard ((dropdownGraph.options[dropdownGraph.value]).text);

		if (pageIndex == 0) {
			buttonPreviousPage.gameObject.SetActive (false);
		}
		buttonNextPage.gameObject.SetActive (true);
	}

}
