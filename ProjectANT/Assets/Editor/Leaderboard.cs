using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// this is a singleton representing the leaderboard
//TODO
public sealed class Leaderboard
{
	private static readonly Leaderboard instance = new Leaderboard();

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
		GlobalstatsIO gs = new GlobalstatsIO();
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
}