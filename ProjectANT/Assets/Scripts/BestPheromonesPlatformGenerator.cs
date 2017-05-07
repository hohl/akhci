using System;

public class BestPheromonesPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public BestPheromonesPlatformGenerator(AntAlgorithms.AntAlgorithmSimple algo) : base(algo)
	{
	}

	protected override Platform FindNextCities (City startCity)
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
	