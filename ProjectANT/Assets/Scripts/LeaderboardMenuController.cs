using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardMenuController : MonoBehaviour {

	private List<GameObject> leaderboardCells = new List<GameObject>();

	// Use this for initialization
	void Start () {
		GameObject[] cells = GameObject.FindGameObjectsWithTag ("LeaderboardCell");

		foreach (GameObject cell in cells) {

			leaderboardCells.Add (cell);
		}

		leaderboardCells.Sort ((x, y) => (y.transform.localPosition.y.CompareTo(x.transform.localPosition.y)));

		int index = 0;
		foreach (GameObject cell in leaderboardCells) {
			index++;
			cell.GetComponent<LeaderboardItemController> ().textName.text = index.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
}
