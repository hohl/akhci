using AntAlgorithms;
using System;

public class BestPheromonesPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public BestPheromonesPlatformGenerator(AntAlgorithm algo, int id, string name) : base(algo, id, name)
	{
	}

	protected override void SortCities (City startCity)
	{
		cities.Sort (delegate(City a, City b) {
			double deltaA = algo.getPheromones ().getPheromone (startCity.getId(), a.getId());
			double deltaB = algo.getPheromones ().getPheromone (startCity.getId(), b.getId());
			return deltaA.CompareTo(deltaB);
		});
	}
}
	