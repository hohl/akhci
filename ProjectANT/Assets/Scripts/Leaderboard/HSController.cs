using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.Security.Cryptography;

public class HSController : MonoBehaviour
{
	private string secretKey = "rLdZyTAJeynUh6JDR8Sut8Yj1sLXIPWO";
	public string addScoreURL = "https://www.andrejmueller.com/highscoresIML/addscore.php?";
	public string highscoreURL = "https://www.andrejmueller.com/highscoresIML/getscores.php?";

	public class CoroutineWithData
	{
		public Coroutine coroutine { get; private set; }
		public object result;
		private IEnumerator target;
		public CoroutineWithData(MonoBehaviour owner, IEnumerator target)
		{
			this.target = target;
			this.coroutine = owner.StartCoroutine(Run());
		}

		private IEnumerator Run()
		{
			while (target.MoveNext())
			{
				result = target.Current;
				yield return result;
			}
		}
	}
	void Start()
	{
		//StartCoroutine(GetScores("test1", 1));
		//StartCoroutine(PostScores("Test", 1023, "test2"));
	}

	IEnumerator PostScores(string name, int score, string tsp)
	{
		string hash = Hash(secretKey);

		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&tsp=" + tsp + "&hash=" + hash;

		WWW hs_post = new WWW(post_url);
		yield return hs_post;
		Debug.Log("HSPOST" + hs_post.url);

		if (!string.IsNullOrEmpty(hs_post.error))
		{
			yield return "There was an error posting the high score: " + hs_post.error;
		}
	}

	IEnumerator GetScores(string tspName, int numberOfEntries)
	{
		WWW hs_get = new WWW(highscoreURL + "tsp=" + WWW.EscapeURL(tspName) + "&num=" + numberOfEntries);
		yield return hs_get;

		if (!string.IsNullOrEmpty(hs_get.error))
		{
			yield return "There was an error getting the high score: " + hs_get.error;
		}
		else
		{
			yield return hs_get.text;
		}
	}

	public string Hash(string password)
	{
		return BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(password))).Replace("-", String.Empty).ToUpper();
	}

	public IEnumerator PostScoresCoroutineLog(string name, int score, string tsp)
	{
		CoroutineWithData cd = new CoroutineWithData(this, PostScores(name, score, tsp));
		yield return cd.coroutine;
		Debug.Log("Result:\n" + cd.result);
	}

	public void StartPostScoresCoroutineLog(string name, int score, string tsp)
	{
		StartCoroutine(PostScoresCoroutineLog(name, score, tsp));
	}

	public void StartGetScoresCoroutine(string tspName, int numberOfEntries)
	{
		StartCoroutine(GetScores(tspName, numberOfEntries));
	}

}