using UnityEngine;
using UnityEditor;

public class AntPrefs
{
	private static AntPrefs instance = null;
	private static readonly string PREF_USERNAME = "ant_user";

	public static AntPrefs Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new AntPrefs();
			}
			return instance;
		}
	}

	public void StoreUsername(string name)
	{
		PlayerPrefs.SetString(PREF_USERNAME, name);
		PlayerPrefs.Save();
	}

	public string GetUsername()
	{
		return PlayerPrefs.GetString(PREF_USERNAME, "Anonymous");
	}
}