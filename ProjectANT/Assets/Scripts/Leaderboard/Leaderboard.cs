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
		GlobalstatsIO.api_id = "SB8HfCieIc1pSOm1iLCpHa1jnou3zo7sZgaeSttA";
		GlobalstatsIO.api_secret = "v0yknzC9c1Qm39KsDN22Vu1bEg37JENeEmz2Wbb8";
	}

	public static Leaderboard Instance
	{
		get
		{
			return instance;
		}
	}
	// see 
	public void SubmitResult(string name, double distance, int graphId, int algorithmId)
	{
		Dictionary<string, string> values = new Dictionary<string, string>();
		values.Add("distance", distance.ToString());
		values.Add("graph", graphId.ToString());
		values.Add("algo", algorithmId.ToString());

		if (gs.share("", name, values))
		{
			// Success
		}
		else
		{
			// An Error occured
		}
	}

	//TODO doesn't work
	public void SubmitResultAsync(double distance, int graphId, int algorithmId)
	{
		Thread m_Thread = new Thread(() => SubmitResult("Anonymous", distance, graphId, algorithmId));
		m_Thread.Start();
	}

	// No idea if that works
	public GlobalstatsIO_Leaderboard GetScore() {

		return gs.getLeaderboard ("distance", 100, new string[] { "graph","algo" });

	}
}