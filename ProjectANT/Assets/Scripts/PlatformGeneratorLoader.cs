using AntAlgorithms;
using System;
using System.Collections.Generic;

/**
 * Load any `PlatformGenerator` implementation randomly.
 */
public class PlatformGeneratorLoader
{
	private Random rand = new Random();
	private Dictionary<int, PlatformGenerator> generators = new Dictionary<int, PlatformGenerator>();

	public PlatformGeneratorLoader()
	{
		PlatformGenerator gen = new BestPheromonesPlatformGenerator(CreateAntAlgorithm());
		generators.Add(gen.ID, gen);
		gen = new RandomPheromonesPlatformGenerator(CreateAntAlgorithm());
		generators.Add(gen.ID, gen);
		gen = new BestDistancesPlatformGenerator(CreateAntAlgorithm());
		generators.Add(gen.ID, gen);
	}

	public string GetGenNameById(int id)
	{
		PlatformGenerator gen = null;
		bool found = generators.TryGetValue(id, out gen);

		return found ? gen.Name : "?(" + id + ")";
	}

	/**
	 * @return any random generator.
	 */
	public PlatformGenerator LoadAny()
	{
		List<int> keys = new List<int>(generators.Keys);
		return generators [keys[rand.Next () % keys.Count]];
	}

	private static AntAlgorithm CreateAntAlgorithm()
	{
		return new ASAlgorithm (1, 2, 100, 6);
	}
}
	