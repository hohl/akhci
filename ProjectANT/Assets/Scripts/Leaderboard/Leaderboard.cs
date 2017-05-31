using UnityEngine;
using System.Collections.Generic;
using System;

// this is a singleton representing the leaderboard
//TODO
public sealed class Leaderboard
{
	private static readonly Leaderboard instance = new Leaderboard();

	private GlobalstatsIO gs = new GlobalstatsIO();

	private Leaderboard() {
		GlobalstatsIO.api_id = "SVJqT6lOZb3qMRX2iwTQ0V6593wWzqirl1Se8e37";
		GlobalstatsIO.api_secret = "7xcYveh18813e7diE9R4sjqucFCrIJRQh3OMi6Oq";
	}

	public static Leaderboard Instance
	{
		get
		{
			return instance;
		}
	}
	// see 
	public void SubmitResult(float distance, int graph, int algorithm)
	{
		
		string user_name = "Anonymous";

		Dictionary<string, string> values = new Dictionary<string, string>();
		values.Add("distance", distance.ToString());
		values.Add("graph", graph.ToString());
		values.Add("algo", algorithm.ToString());

		if (gs.share("", user_name, values))
		{
			// Success
		}
		else
		{
			// An Error occured
		}
	}

	// No idea if that works
	public GlobalstatsIO_Leaderboard GetScore() {

		return gs.getLeaderboard ("distance", 100, new string[] { "graph","algo" });

	}
}