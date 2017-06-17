using AntAlgorithms;
using System;

public class BestDistancesPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public BestDistancesPlatformGenerator (AntAlgorithm algo) : base(algo, 300, "bDist")
	{
	}

	protected override Platform FindNextCities (City startCity)
	{
		cities.Sort (delegate(City a, City b) {
			double deltaA = DistanceBetween (startCity, a);
			double deltaB = DistanceBetween (startCity, b);
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

	private double DistanceBetween(City a, City b)
	{
		return Math.Sqrt(Math.Pow(a.getXPosition() - b.getXPosition(), 2) + Math.Pow(a.getYPosition() - b.getYPosition(), 2) );
	}
}
