using AntAlgorithms;
using System;

public class RandomPheromonesPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public RandomPheromonesPlatformGenerator(AntAlgorithm algo) : base(algo, 100)
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
}


