using AntAlgorithms;
using System;

public class WeightedPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public WeightedPlatformGenerator (AntAlgorithm algo, int id, string name) : base(algo, id, name)
	{
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
		float firstDistance = (float) DistanceBetween (startCity, firstCity);
		float secondPheromons = PseudoSafeFloat((float) (algo.getPheromones ().getPheromone (startCity.getId (), secondCity.getId ())));
		float secondDistance = (float) DistanceBetween (startCity, secondCity);

		// normalize values! [0..1]
		firstPheromons = firstPheromons / (firstPheromons + secondPheromons);
		secondPheromons = secondPheromons / (firstPheromons + secondPheromons);
		firstDistance = firstDistance / (firstDistance + secondDistance);
		secondDistance = secondDistance / (firstDistance + secondDistance);

		float firstGap = -0.3f * (firstPheromons * 0.6f + firstDistance * 0.4f) + 0.4f;
		float secondGap = 0.3f * (secondPheromons * 0.6f + secondDistance * 0.4f) + 0.6f;
		return new Platform (new Gap (firstGap, firstCity), new Gap (secondGap, secondCity));
	}

	protected override void SortCities (City startCity)
	{
		cities.Sort (delegate(City a, City b) {
			double deltaA = DistanceBetween (startCity, a);
			double deltaB = DistanceBetween (startCity, b);
			return deltaA.CompareTo(deltaB);
		});
	}
		
	private double DistanceBetween(City a, City b)
	{
		return Math.Sqrt(Math.Pow(a.getXPosition() - b.getXPosition(), 2) + Math.Pow(a.getYPosition() - b.getYPosition(), 2) );
	}
}
