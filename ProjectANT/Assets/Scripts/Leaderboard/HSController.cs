using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.Security.Cryptography;

public class HSController : MonoBehaviour
{
	public static readonly string ALGO_START = "|algo:";
	private static readonly String GAME_NAME = "ProjectANT";
	private string secretKey = "rLdZyTAJeynUh6JDR8Sut8Yj1sLXIPWO";
	public static string addScoreURL = "http://www.andrejmueller.com/highscoresIML/addscore.php?";
	public static string highscoreURL = "http://www.andrejmueller.com/highscoresIML/getscores.php?";

	void Start()
	{
		//StartCoroutine(GetScores("test2", "TravellingSnakesman", 10));
		//StartCoroutine(PostScores("Test", 1023, "test2", 1, "TravellingSnakesman", "Time3:02"));
	}

	IEnumerator PostScores(string name, int distance, int score, string tsp, int algorithm, string game)
	{
		string post_url = GetPostUrl(name, distance, score, tsp, algorithm, game);

		WWW hs_post = new WWW(post_url);
		print("HSPOST|" + hs_post.url);
		yield return hs_post;

		if (!string.IsNullOrEmpty(hs_post.error))
		{
			print("There was an error posting the high score: " + hs_post.error);
		}

		print("finished call:" + hs_post.url);
	}

	IEnumerator PostScores(string name, int distance, int score, string tsp, int algorithm, string game, Action<string> action)
	{
		string post_url = GetPostUrl(name, distance, score, tsp, algorithm, game);

		WWW hs_post = new WWW(post_url);
		print("HSPOST|" + hs_post.url);
		yield return hs_post;
		string resultText;

		if (!string.IsNullOrEmpty(hs_post.error))
		{
			resultText = "There was an error posting the high score: " + hs_post.error.ToString();
		}
		else
		{
			resultText = "Posting high score succesful";
		}

		print(resultText);
		action(resultText);
	}

	public IEnumerator GetScores(string tspName, string gameName, int numberOfEntries)
	{
		WWW hs_get = new WWW(GetScoresUrl(tspName, gameName, numberOfEntries));
		print("HSGET|" + hs_get.url);
		yield return hs_get;
		string resultText;

		if (!string.IsNullOrEmpty(hs_get.error))
		{
			resultText = "There was an error getting the high score: " + hs_get.error.ToString();
		}
		else
		{
			print("Getting high score succesful");
			resultText = hs_get.text.ToString();
		}

		print(resultText);
	}


	private IEnumerator GetScores(string tspName, string gameName, int numberOfEntries, Action<string> action)
	{
		WWW hs_get = new WWW(GetScoresUrl(tspName, gameName, numberOfEntries));
		print("HSGET|" + hs_get.url);
		yield return hs_get;
		string resultText;

		if (!string.IsNullOrEmpty(hs_get.error))
		{
			resultText = "There was an error getting the high score: " + hs_get.error.ToString();
		}
		else
		{
			print("Getting high score succesful");
			resultText = hs_get.text.ToString();
		}

		//print(resultText);
		action(resultText);
	}

	internal string GetScoresUrl(string tspName, string gameName, int numberOfEntries)
	{
		return new WWW(highscoreURL + "tsp=" + WWW.EscapeURL(tspName) + "&num=" + numberOfEntries + "&game=" + gameName).url;
	}

	internal string GetPostUrl(string name, int distance, int score, string tsp, int algorithm, string game)
	{
		string hash = Hash(secretKey);

		return addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + distance + "&tsp=" + tsp + "&hash=" + hash + "&algorithm=" + algorithm + "&game=" + game + "&comment=" + score + ALGO_START + algorithm;
	}

	public string Hash(string password)
	{
		return BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(password))).Replace("-", String.Empty).ToUpper();
	}

	internal IEnumerator StartPostScoresCoroutine(string name, int distance, int score, string tsp, int algorithm, Action<string> action)
	{
		String dateStringForComment = DateTime.Now.ToString(@"dd\/MM\/yyyy\/HH\:mm");
		yield return StartCoroutine(PostScores(name, distance, score, tsp, algorithm, GAME_NAME, action));
	
	}

	public void StartPostScoresCoroutine(string name, int distance, int score, string tsp, int algorithm, string game)
	{
		StartCoroutine(PostScores(name, distance, score, tsp, algorithm, game));
	}

	public void StartGetScoresCoroutine(string tspName, int numberOfEntries)
	{
		StartCoroutine(GetScores(tspName, GAME_NAME, numberOfEntries));
	}

	public IEnumerator StartGetScoresCoroutine(string tspName, int numberOfEntries, Action<string> action)
	{
		yield return StartCoroutine(GetScores(tspName, GAME_NAME, numberOfEntries, action));
	}

}

// same as in GlobalstatsIO_Leaderboard
public class HSMuellerLeaderboard
{
	private LeaderboardEntry[] data;

	public HSMuellerLeaderboard(string serverResponse)
	{
		string[] lineDelimiters = { "<br>" };
		string[] lines = serverResponse.Split(lineDelimiters, StringSplitOptions.RemoveEmptyEntries);
		Data = new LeaderboardEntry[lines.Length];

		for(int i = 0; i < lines.Length; i++)
		{
			Data[i] = new LeaderboardEntry(i+1, "test8", lines[i]);
		}
	}

	public LeaderboardEntry[] Data
	{
		get
		{
			return data;
		}

		set
		{
			data = value;
		}
	}
}