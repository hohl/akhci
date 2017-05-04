using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public class PlatformGenerator
{
	private AntAlgorithmSimple algo;
	private List<City> cities = new List<City> ();
	private GameObject cityGameObject;

	public PlatformGenerator (AntAlgorithmSimple algo)
	{
		this.algo = algo;
		cityGameObject = new GameObject();
		Load ();
	}

	public Result Next ()
	{
		// TODO: return real values there!!!
		return new Result(0.25f, 0.75f);
	}

	private void Load ()
	{
		cities.Add(new City(2, 4, 0, "Vienna", cityGameObject));
		cities.Add(new City(1, 9, 1, "Graz", cityGameObject));
		cities.Add(new City(3, 8, 2, "Klagenfurt", cityGameObject));
		cities.Add(new City(9, 1, 3, "Innsbruck", cityGameObject));
		cities.Add(new City(10, 1, 4, "Innsbruck", cityGameObject));

		cities.Add(new City(5, 4, 5, "Vienna", cityGameObject));
		cities.Add(new City(1, 11, 6, "Graz", cityGameObject));
		cities.Add(new City(3, 4, 7, "Klagenfurt", cityGameObject));

		algo.setCities(cities);
		algo.init();
		for (int i = 0; i < 50; i++)
			algo.iteration();
	}

	public class Result
	{
		public float first { get; private set; }
		public float firstWidth = 0.1f;
		public float second { get; private set; }
		public float secondWidth = 0.1f;

		public Result(float first, float second)
		{
			this.first = first;
			this.second = second;
		}
	}
}
