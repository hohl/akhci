using AntAlgorithms;
using System;

/**
 * Load any `PlatformGenerator` implementation randomly.
 */
public class PlatformGeneratorLoader
{
	private PlatformGenerator[] generators = new PlatformGenerator[] {
		new BestPheromonesPlatformGenerator (CreateAntAlgorithm ()),
		new RandomPheromonesPlatformGenerator (CreateAntAlgorithm ()),
		new BestDistancesPlatformGenerator (CreateAntAlgorithm ())
	};

	private Random rand = new Random();

	/**
	 * @return any random generator.
	 */
	public PlatformGenerator LoadAny()
	{
		return generators [rand.Next () % generators.Length];
	}

	private static AntAlgorithm CreateAntAlgorithm()
	{
		return new ASAlgorithm (1, 2, 100, 6);
	}
}
	