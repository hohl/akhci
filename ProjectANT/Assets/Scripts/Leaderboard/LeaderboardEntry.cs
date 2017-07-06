using System;
using UnityEngine;

public class LeaderboardEntry
{
	private int rank;
	private string name;
	private float distance;
	private string graph;
	private string algo;
	private int score;

	public LeaderboardEntry(GlobalstatsIO_LeaderboardValue leaderboardValue)
	{
		this.name = leaderboardValue.name;
		this.Rank = Int32.Parse(leaderboardValue.rank);
		this.score = Int32.Parse(leaderboardValue.value);
		

		foreach (GlobalstatsIO_Additional additional in leaderboardValue.additionals)
		{
			if (additional.key == "graph")
			{
				this.graph = TspLoader.Instance.GetTspNameById(Int32.Parse(additional.value));
			}
			else if (additional.key == "algo")
			{
				this.algo = PlatformGeneratorLoader.GetGenNameById(Int32.Parse(additional.value));
			}
			else if (additional.key == "distance")
			{
				this.distance = Single.Parse(additional.value);
			}
		}
	}

	public LeaderboardEntry(int rank, string graph, string serverString)
	{
		string[] delimiters = { "ID:", " - Name: ", " - Score:", " - Comment:", HSController.ALGO_START};
		string[] words = serverString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

		if (words.Length < 4)
		{
			Debug.Log("ERROR while parsing entry");
		}
		else
		{
			this.Rank = rank;
			this.name = words[1];
			Single.TryParse(words[2], out this.distance);
			Int32.TryParse(words[3], out this.score);

			if (words.Length > 4)
			{
				int id = -1;
				Int32.TryParse(words[4], out id);
				this.algo = PlatformGeneratorLoader.GetGenNameById(id);
			}
			else
			{
				this.algo = "?";
			}

			this.graph = graph;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}

		set
		{
			name = value;
		}
	}

	public float Distance
	{
		get
		{
			return distance;
		}

		set
		{
			distance = value;
		}
	}

	public string Algo
	{
		get
		{
			return algo;
		}

		set
		{
			algo = value;
		}
	}

	public string Graph
	{
		get
		{
			return graph;
		}

		set
		{
			graph = value;
		}
	}

	public int Rank
	{
		get
		{
			return rank;
		}

		set
		{
			rank = value;
		}
	}
		
	public int Score
	{
		get
		{
			return score;
		}

		set
		{
			score = value;
		}
	}
}