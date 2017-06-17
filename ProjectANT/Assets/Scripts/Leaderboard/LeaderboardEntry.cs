using System;
using UnityEngine;

public class LeaderboardEntry
{
	private int rank;
	private string name;
	private float distance;
	private string graph;
	private string algo;

	public LeaderboardEntry(GlobalstatsIO_LeaderboardValue leaderboardValue)
	{
		this.name = leaderboardValue.name;
		this.Rank = Int32.Parse(leaderboardValue.rank);
		this.distance = Single.Parse(leaderboardValue.value);
		

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
		}
	}

	public LeaderboardEntry(int rank, string graph, string serverString)
	{
		string[] delimiters = { "ID:", " - Name: ", " - Score:", " - Comment:" };
		string[] words = serverString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

		if (words.Length < 4)
		{
			Debug.Log("ERROR while parsing entry");
		}
		else
		{
			this.Rank = rank;
			this.name = words[1];
			this.distance = Int32.Parse(words[2]);
			this.algo = "?";
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
}