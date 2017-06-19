using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PheromonesBasedPlatformGenerator : RandomPlatformGenerator
{
	protected const int PlayerFactor = Int32.MaxValue;
	private const int RecalcInterval = 3;

	protected AntAlgorithm algo;
	protected List<City> cities = new List<City> ();
	protected List<City> visitedCities = new List<City> ();
	protected bool needsBuffer = true;
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
				algo.getPheromones ().increasePheromoneAS (PreviousCity.getId (), value.getId (), PlayerFactor);
				Visit (SelectedCity);
				RecalcIfNeeded ();
			}
		}
	}

	public City PreviousCity {
		get;
		protected set;
	}

	public PheromonesBasedPlatformGenerator (AntAlgorithm algo, int id, string name) : base(id, name)
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

	protected virtual Platform FindNextCities (City startCity)
	{
		SortCities (startCity);

		City firstCity = null, secondCity = null;
		for (int index = 0, count = 0; index < cities.Count && count < 2; ++index) {
			if (HasBeenVisited (cities [index])) {
				continue;
			}

			if (count == 0) {
				firstCity = cities [index];
			} else if (count == 1) {
				secondCity = cities [index];
			}
			++count;
		}

		if (firstCity == null || secondCity == null) {
			// no more cities left... just return random
			return NextRandom ();
		}

		float firstPheromons = PseudoSafeFloat((float) (algo.getPheromones ().getPheromone (startCity.getId (), firstCity.getId ())));
		float secondPheromons = PseudoSafeFloat((float) (algo.getPheromones ().getPheromone (startCity.getId (), secondCity.getId ())));

		// normalize values! [0..1]
		firstPheromons = -0.3f * firstPheromons / (firstPheromons + secondPheromons) + 0.4f;
		secondPheromons = 0.3f * secondPheromons / (firstPheromons + secondPheromons) + 0.6f;

		return new Platform (new Gap (firstPheromons, firstCity), new Gap (secondPheromons, secondCity));
	}

	/// <summary>
	/// Override this to use a different sorting for the generator.
	/// 
	/// For ex. sorting by pheromons or by distances.
	/// </summary>
	protected virtual void SortCities (City startCity)
	{
	}

	/// <summary>
	/// Visits the specified city.
	/// </summary>
	/// <param name="city">The city to visit.</param>
	protected void Visit(City city)
	{
		visitedCities.Add (city);
	}

	/// <summary>
	/// Determines whether this city has been visited already.
	/// </summary>
	/// <returns><c>true</c> if this city has been visited already; otherwise, <c>false</c>.</returns>
	/// <param name="city">The city to test.</param>
	protected bool HasBeenVisited(City city)
	{
		return visitedCities.Contains (city);
	}

	protected float PseudoSafeFloat(float value)
	{
		// no, that method doesn't have any mathematical background,
		// just an ugly hack which works for very unlikly cases

		if (float.IsPositiveInfinity (value))
			return float.MaxValue / 2.0f;

		if (float.IsNegativeInfinity (value))
			return float.MinValue / 2.0f;

		return value;
	}

	protected void RecalcIfNeeded()
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
}
