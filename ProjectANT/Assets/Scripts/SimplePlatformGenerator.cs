using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public class SimplePlatformGenerator : RandomPlatformGenerator
{
	private const int PlayerFactor = Int32.MaxValue;
	private const int RecalcInterval = 3;

	private AntAlgorithmSimple algo;
	private List<City> cities = new List<City> ();
	private GameObject cityGameObject;
	private bool needsBuffer = true;
	private int needsRecalcCounter = RecalcInterval;

	public override double Result {
		get {
			return algo.getTourLength ();
		}
	}

	public override City SelectedCity {
		get {
			return base.SelectedCity;
		}
		set {
			City oldValue = base.SelectedCity;
			base.SelectedCity = value;
			if (oldValue != null) {
				algo.getPheromones ().increasePheromone (oldValue.getId (), value.getId (), PlayerFactor);
				RecalcIfNeeded ();
			}
		}
	}

	public City PreviousCity {
		get;
		private set;
	}

	public SimplePlatformGenerator (AntAlgorithmSimple algo)
	{
		this.algo = algo;
		cityGameObject = new GameObject ();
		Load ();
	}

	public override Platform Next ()
	{
		if (SelectedCity == PreviousCity) 
		{
			needsBuffer = true;
			return base.Next(); // just randomly choose one!
		}
		else if (needsBuffer) 
		{
			needsBuffer = false;
			return new Platform (new Gap (-0.2f, null), new Gap (0.5f, null));
		}
		else 
		{
			PreviousCity = SelectedCity;
			needsBuffer = true;
			return FindBestForCity (SelectedCity);
		}
	}

	public override void Finish ()
	{
		Recalc (cities.Count * 3);
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
		Recalc (cities.Count);
	}

	private void RecalcIfNeeded()
	{
		if (needsRecalcCounter == 0) {
			Recalc (cities.Count / 3);
			needsRecalcCounter = RecalcInterval;
		} else {
			needsRecalcCounter--;
		}
	}

	private void Recalc(int iterations)
	{

		Debug.Log (String.Format ("AntAlgo: Run {0} iterations...", iterations));
		for (int i = 0; i < iterations; i++)
			algo.iteration ();
	}

	public Platform FindBestForCity (City startCity)
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
		firstPheromons = -0.3f * firstPheromons / (firstPheromons + secondPheromons) + 0.4f;
		secondPheromons = 0.3f * secondPheromons / (firstPheromons + secondPheromons) + 0.6f;

		return new Platform (new Gap (firstPheromons, firstCity), new Gap (secondPheromons, secondCity));
	}
}
