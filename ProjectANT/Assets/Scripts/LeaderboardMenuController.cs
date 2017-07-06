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
	private GlobalstatsIO_Leaderboard globalLeaderboard; // not used for now
	private HSMuellerLeaderboard muellerLeaderboard;
	private HSController hsMuellerController;
	private List<LeaderboardEntry> currentValues;
	private string currGraphName;

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

		//globalLeaderboard = Leaderboard.Instance.GetScore ();
		yield return StartCoroutine(OnSetTsp(dropdownGraph));

		buttonPreviousPage.onClick.AddListener(PreviousPage);
		buttonNextPage.onClick.AddListener(NextPage);
		buttonSendName.onClick.AddListener(SetUserName);

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
			StartCoroutine(OnSetTsp(dropdownGraph));
		});

	}

	private IEnumerator OnSetTsp(Dropdown dropdownGraph)
	{
		pageIndex = 0;
		currGraphName = (dropdownGraph.options[dropdownGraph.value]).text;
		//FillLeaderboard();
		hsMuellerController = GetComponent<HSController>();

		string result = null;
		yield return hsMuellerController.StartGetScoresCoroutine(currGraphName, 100, value => result = value);
		print("The result was:" + result);
		muellerLeaderboard = new HSMuellerLeaderboard(result);
		currentValues = new List<LeaderboardEntry>(muellerLeaderboard.Data);
		currentValues.Sort((x,y) => y.Score - x.Score);
		FillMuellerLeaderboard();
		
		buttonPreviousPage.gameObject.SetActive(false);
		buttonNextPage.gameObject.SetActive(currentValues.Count > leaderboardCells.Count);
	}

	private void SetUserName()
	{
		AntPrefs.Instance.StoreUsername(inputFieldName.text);
	}

	private void FillMuellerLeaderboard()
	{
		for (int i = 0; i < leaderboardCells.Count; i++)
		{
			if (pageIndex * leaderboardCells.Count + i < currentValues.Count)
			{
				LeaderboardEntry entry = currentValues[pageIndex * leaderboardCells.Count + i];
				entry.Rank = pageIndex * leaderboardCells.Count + i + 1;
				SetLeaderboardEntryToUi(leaderboardCells[i], entry);
			}
			else
			{
				leaderboardCells[i].SetActive(false);
			}
		}
	}

	private void FillLeaderboard() {
		if (globalLeaderboard.data == null) {
			return;
		}

		currentValues = new List<LeaderboardEntry>();

		foreach (GlobalstatsIO_LeaderboardValue v in globalLeaderboard.data)
		{
			LeaderboardEntry entry = new LeaderboardEntry(v);
			//if (entry.Graph.Equals(currGraphName))
			//{
				currentValues.Add(entry);
			//}
		}

		for (int i = 0; i < leaderboardCells.Count; i++)
		{
			if (pageIndex * leaderboardCells.Count + i < currentValues.Count)
			{
				LeaderboardEntry entry = currentValues[pageIndex * leaderboardCells.Count + i];
				entry.Rank = pageIndex * leaderboardCells.Count + i + 1;
				SetLeaderboardEntryToUi(leaderboardCells[i], entry);
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
		cellController.textScore.text = entry.Score.ToString ();

		cell.SetActive(true);
	}


	private void GoBackToMenu() {
		SceneManager.LoadScene("MainMenuScene");
	}

	private void NextPage() {
		pageIndex++;
		FillMuellerLeaderboard ();

		if ((pageIndex + 1) * leaderboardCells.Count >= currentValues.Count) {
			buttonNextPage.gameObject.SetActive (false);
		}
		buttonPreviousPage.gameObject.SetActive (true);
	}

	private void PreviousPage() {

		pageIndex--;
		FillMuellerLeaderboard ();

		if (pageIndex == 0) {
			buttonPreviousPage.gameObject.SetActive (false);
		}
		buttonNextPage.gameObject.SetActive (true);
	}

}
