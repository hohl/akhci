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

	// helper for easy access to names (sorry)
	private static Dictionary<int, string> genNames;
	private static NameTuple infoBestPhero;
	private static NameTuple infoRandomPhero;
	private static NameTuple infoBestDist;
	private static NameTuple infoTwoSteps;

	// helper
	public class NameTuple
	{
		int id;
		string name;

		public NameTuple(int id, string name)
		{
			this.Id = id;
			this.Name = name;
		}

		public int Id
		{
			get
			{
				return id;
			}

			set
			{
				id = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}

			set
			{
				name = value;
			}
		}
	}

	// i know that this is not pretty, but it's pretty much the most performant way
	static PlatformGeneratorLoader()
	{
		infoBestPhero = new NameTuple(200, "bPhero");
		infoRandomPhero = new NameTuple(100, "rPhero");
		infoBestDist = new NameTuple(300, "bDist");
		infoTwoSteps = new NameTuple(400, "2step");

		genNames = new Dictionary<int, string>();
		genNames.Add(infoBestPhero.Id, infoBestPhero.Name);
		genNames.Add(infoRandomPhero.Id, infoRandomPhero.Name);
		genNames.Add(infoBestDist.Id, infoBestDist.Name);
		genNames.Add(infoTwoSteps.Id, infoTwoSteps.Name);
	}

	public PlatformGeneratorLoader()
	{
		// if doing a "Hot-Swap" this wouldn't work
		// for now, it is sufficient to only create it once
		AntAlgorithm algo = CreateAntAlgorithm();
		PlatformGenerator gen = new BestPheromonesPlatformGenerator(algo, infoBestPhero.Id, infoBestPhero.Name);
		generators.Add(gen.ID, gen);
		gen = new RandomPheromonesPlatformGenerator(algo, infoRandomPhero.Id, infoRandomPhero.Name);
		generators.Add(gen.ID, gen);
		gen = new BestDistancesPlatformGenerator(algo, infoRandomPhero.Id, infoRandomPhero.Name);
		generators.Add(gen.ID, gen);
		// not enabled or now gen = new TwoStepPlatformGenerator(algo, infoTwoSteps.id, infoTwoSteps.name);
		// generators.Add(gen.ID, gen);
	}

	public static string GetGenNameById(int id)
	{
		string name = null;
		bool found = genNames.TryGetValue(id, out name);

		return found ? name : "?(" + id + ")";
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
	