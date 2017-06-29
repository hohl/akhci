 using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;

// this is a singleton representing the leaderboard
//TODO
public sealed class Leaderboard
{
	private static readonly Leaderboard instance = new Leaderboard();

	private GlobalstatsIO gs = new GlobalstatsIO();

	private Leaderboard() {
		GlobalstatsIO.api_id = "24xZIXHP8bd4ddCKD3zcH2nyYZu87PY9Tw8CtPsC";
		GlobalstatsIO.api_secret = "GHeTjZwnzJ5B6Ez68HdYGnScvvGlXimD0BwjB3Hb";
	}

	public static Leaderboard Instance
	{
		get
		{
			return instance;
		}
	}
	// see 
	public void SubmitResult(string name, double distance, int graphId, int algorithmId, int score)
	{
		Dictionary<string, string> values = new Dictionary<string, string>();
		values.Add("distance", distance.ToString());
		values.Add("graph", graphId.ToString());
		values.Add("algo", algorithmId.ToString());
		values.Add("score", score.ToString());

		if (gs.share("", name, values))
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

		return gs.getLeaderboard ("score", 100, new string[] { "graph","algo","distance"});

	}
}