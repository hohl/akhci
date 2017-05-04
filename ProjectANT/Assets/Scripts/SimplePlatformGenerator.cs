using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public class SimplePlatformGenerator : PlatformGenerator
{
	private AntAlgorithmSimple algo;
	private List<City> cities = new List<City> ();
	private GameObject cityGameObject;
	private bool nextIsBuffer = true;

	public SimplePlatformGenerator (AntAlgorithmSimple algo)
	{
		this.algo = algo;
		cityGameObject = new GameObject ();
		Load ();
	}

	public override Result Next ()
	{
		if (nextIsBuffer) 
		{
			nextIsBuffer = false;
			return new Result (-0.2f, null, 0.5f, null);
		}
		else 
		{
			nextIsBuffer = true;
			return FindBestForCity(SelectedCity);
		}
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

		SelectedCity = cities [0];

		algo.setCities(cities);
		algo.init();
		for (int i = 0; i < 50; i++)
			algo.iteration();
	}

	public Result FindBestForCity (City startCity)
	{
		int startCityIndex = cities.IndexOf (startCity);

		City firstCity = null;
		float firstPheromons = 0.0f;
		City secondCity = null;
		float secondPheromons = 0.0f;
		for (int targetCityIndex = 0; targetCityIndex < cities.Count; ++targetCityIndex) {
			if (startCityIndex == targetCityIndex)
				continue;

			float pheromons = (float) algo.getPheromones ().getPheromone (startCityIndex, targetCityIndex);
			if (firstPheromons < pheromons) {
				secondCity = firstCity;
				secondPheromons = firstPheromons;
				firstCity = cities[targetCityIndex];
				firstPheromons = pheromons;
			} else if (secondPheromons < pheromons) {
				secondCity = cities[targetCityIndex];
				secondPheromons = pheromons;
			}
		}

		// normalize values! [0..1]
		firstPheromons = 0.3f * firstPheromons / (firstPheromons + secondPheromons) + 0.1f;
		secondPheromons = 0.3f * secondPheromons / (firstPheromons + secondPheromons) + 0.6f;

		return new Result (firstPheromons, firstCity, secondPheromons, secondCity);
	}
}
