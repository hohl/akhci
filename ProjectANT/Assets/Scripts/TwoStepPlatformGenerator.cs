using AntAlgorithms;
using UnityEngine;
using System;

public class TwoStepPlatformGenerator : PheromonesBasedPlatformGenerator
{
	private City startCity = null;
	private City firstCity = null;
	private City secondCity = null;
	private City previousSelectedCity = null;

	public override City SelectedCity {
		get {
			return base.SelectedCity;
		}
		set {
			previousSelectedCity = base.SelectedCity;
			base.SelectedCity = value;
			if (previousSelectedCity == firstCity && value == secondCity) {
				Debug.Log (String.Format ("AntAlgo: Increase pheromones of path {0} to {1} to {2}.", startCity.getId (), firstCity.getId (), secondCity.getId ()));
				algo.getPheromones ().increasePheromoneAS (startCity.getId (), firstCity.getId (), PlayerFactor);
				algo.getPheromones ().increasePheromoneAS (firstCity.getId (), secondCity.getId (), PlayerFactor);
				RecalcIfNeeded ();
				startCity = firstCity = secondCity = null;
			} else if (firstCity != value) {
				startCity = firstCity = secondCity = null;
			}
		}
	}

	public TwoStepPlatformGenerator (AntAlgorithm algo, int id, string name) : base(algo, id, name)
	{
	}

	public override Platform Next ()
	{
		if (needsBuffer == true) {
			needsBuffer = false;
			return new Platform (new Gap (-0.2f, null), new Gap (0.5f, null));
		} else if (firstCity == null) {
			startCity = cities [rand.Next () % cities.Count];
			var next = FindNextCities (startCity);
			firstCity = next.FirstGap.City;
			needsBuffer = true;
			return next;
		} else if (secondCity == null) {
			var next = FindNextCities (firstCity);
			secondCity = next.FirstGap.City;
			needsBuffer = true;
			return next;
		} else {
			needsBuffer = true;
			return base.Next();
		}
	}

	protected override Platform FindNextCities (City startCity)
	{
		cities.Sort (delegate(City a, City b) {
			double deltaA = algo.getPheromones ().getPheromone (startCity.getId(), a.getId());
			double deltaB = algo.getPheromones ().getPheromone (startCity.getId(), b.getId());
			return deltaA.CompareTo(deltaB);
		});

		City firstCity = cities[0];
		float firstPheromons = PseudoSafeFloat((float) (algo.getPheromones ().getPheromone (startCity.getId (), firstCity.getId ())));
		City secondCity = cities[1];
		float secondPheromons = PseudoSafeFloat((float) (algo.getPheromones ().getPheromone (startCity.getId (), secondCity.getId ())));

		// normalize values! [0..1]
		firstPheromons = -0.3f * firstPheromons / (firstPheromons + secondPheromons) + 0.4f;
		secondPheromons = 0.3f * secondPheromons / (firstPheromons + secondPheromons) + 0.6f;

		return new Platform (new Gap (firstPheromons, firstCity), new Gap (secondPheromons, secondCity));
	}
}
