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
	private int startCity = 0;

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
			return new Result (-0.2f, NO_CITY, 0.5f, NO_CITY);
		}
		else 
		{
			nextIsBuffer = true;
			return FindBestForCity(startCity);
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

		algo.setCities(cities);
		algo.init();
		for (int i = 0; i < 50; i++)
			algo.iteration();
	}

	public Result FindBestForCity (int startCity)
	{
		int firstCity = NO_CITY;
		float firstPheromons = 0.0f;
		int secondCity = NO_CITY;
		float secondPheromons = 0.0f;
		for (int targetCity = 0; targetCity < cities.Count; ++targetCity) {
			float pheromons = (float) algo.getPheromones ().getPheromone (startCity, targetCity);
			if (firstPheromons < pheromons) {
				secondCity = firstCity;
				secondPheromons = firstPheromons;
				firstCity = targetCity;
				firstPheromons = pheromons;
			} else if (secondPheromons < pheromons) {
				secondCity = targetCity;
				secondPheromons = pheromons;
			}
		}

		// normalize!
		firstPheromons = 0.3f * firstPheromons / (firstPheromons + secondPheromons) + 0.1f;
		secondPheromons = 0.3f * secondPheromons / (firstPheromons + secondPheromons) + 0.6f;

		// TEMP: next time start from the left one
		startCity = firstCity;

		return new Result (firstPheromons, firstCity, secondPheromons, secondCity);
	}
}
