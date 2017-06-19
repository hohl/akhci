using AntAlgorithms;
using System;

public class RandomPheromonesPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public RandomPheromonesPlatformGenerator(AntAlgorithm algo, int id, string name) : base(algo, id, name)
	{
	}

	protected override void SortCities (City unused)
	{
		int[] randomValues = new int[cities.Count];
		for (int index = 0; index < cities.Count; ++index) {
			randomValues [index] = rand.Next ();
		}
		cities.Sort (delegate(City a, City b) {
			return randomValues[a.getId()].CompareTo(randomValues[b.getId()]);
		});
	}
}


