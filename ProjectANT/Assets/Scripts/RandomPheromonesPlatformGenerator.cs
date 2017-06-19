using AntAlgorithms;
using System;

public class RandomPheromonesPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public RandomPheromonesPlatformGenerator(AntAlgorithm algo, int id, string name) : base(algo, id, name)
	{
	}

	protected override Platform FindNextCities (City startCity)
	{
		int startCityIndex = cities.IndexOf (startCity);
		int firstCityIndex = rand.Next () % cities.Count;
		int secondCityIndex;
		do {
			secondCityIndex = rand.Next() % cities.Count;
		} while (firstCityIndex == secondCityIndex);
			
		City firstCity = cities[firstCityIndex];
		float firstPheromons = (float) algo.getPheromones().getPheromone(startCityIndex, firstCityIndex);
		City secondCity = cities[secondCityIndex];
		float secondPheromons = (float) algo.getPheromones().getPheromone(startCityIndex, secondCityIndex);

		// normalize values! [0..1]
		firstPheromons = PseudoSafeFloat(firstPheromons);
		secondPheromons = PseudoSafeFloat (secondPheromons);
		firstPheromons = -0.3f * firstPheromons / (firstPheromons + secondPheromons) + 0.4f;
		secondPheromons = 0.3f * secondPheromons / (firstPheromons + secondPheromons) + 0.6f;

		return new Platform (new Gap (firstPheromons, firstCity), new Gap (secondPheromons, secondCity));
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


