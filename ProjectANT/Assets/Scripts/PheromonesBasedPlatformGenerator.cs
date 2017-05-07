using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PheromonesBasedPlatformGenerator : RandomPlatformGenerator
{
	private const int PlayerFactor = Int32.MaxValue;
	private const int RecalcInterval = 3;

	protected AntAlgorithmSimple algo;
	protected List<City> cities = new List<City> ();
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
			base.SelectedCity = value;
			if (PreviousCity != null) {
				Debug.Log (String.Format ("AntAlgo: Increase pheromones of path {0} to {1}.", PreviousCity.getId (), value.getId ()));
				algo.getPheromones ().increasePheromone (PreviousCity.getId (), value.getId (), PlayerFactor);
				RecalcIfNeeded ();
			}
		}
	}

	public City PreviousCity {
		get;
		private set;
	}

	public PheromonesBasedPlatformGenerator (AntAlgorithmSimple algo)
	{
		this.algo = algo;
		Load ();
	}

	public override void Finish ()
	{
		Recalc (cities.Count * 3);
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
			return FindNextCities (SelectedCity);
		}
	}

	protected abstract Platform FindNextCities (City startCity);

	private void Load ()
	{
		//TspInfo[] allTSPs = TspLoader.Instance.GetAllTSPs();
		CurrentTsp = TspLoader.Instance.SelectRandomTsp(); //allTSPs[2];
		cities = CurrentTsp.Load();

		Debug.Log("Loaded TSP '" + CurrentTsp.GetName() + "' with " + cities.Count + " cities.");

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
}
