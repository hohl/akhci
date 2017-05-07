
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TspLoader
{
	private static TspLoader instance;
	private TspInfo[] allTSPs = new TspInfo[] { new TspTest8(), new TspTest50(), new Berlin52(), new Eil51()
		// These will load but take a bit long:
		//		new Eil101(),
		// These all won't load???
		//		new Dantzig42(), new Dsj1000(), new Fnl4461(), new Si1032()
	};

	private TspLoader() { }

	public static TspLoader Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new TspLoader();
			}
			return instance;
		}
	}

	public TspInfo[] GetAllTSPs()
	{
		return allTSPs;
	}

	public TspInfo SelectRandomTsp()
	{
		System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
		int index = rnd.Next(0, allTSPs.Length);
		// Debug.Log("Overall: " + allTSPs.Length+"index:" + index);
		return allTSPs[index];
	}

	// TSP generation here below
	class TspTest8 : TspInfo
	{
		public override string GetName()
		{
			return "test8";
		}

		public override List<City> Load()
		{
			GameObject cityGameObject = new GameObject();
			List<City> cities = new List<City>
			{
			new City(2, 4, 0, "Vienna", cityGameObject),
			new City(1, 9, 1, "Graz", cityGameObject),
			new City(3, 8, 2, "Klagenfurt", cityGameObject),
			new City(9, 1, 3, "Innsbruck", cityGameObject),
			new City(10, 1, 4, "Innsbruck", cityGameObject),

			new City(5, 4, 5, "Vienna", cityGameObject),
			new City(1, 11, 6, "Graz", cityGameObject),
			new City(3, 4, 7, "Klagenfurt", cityGameObject)
			};

			return cities;
		}
	}
	class TspTest50 : TspInfo
	{
		public override string GetName()
		{
			return "test50";
		}

		public override List<City> Load()
		{

			GameObject cityGameObject = new GameObject();
			List<City> cities = new List<City>
			{
				new City(2, 4, 0, "Vienna", cityGameObject),
				new City(1, 9, 1, "Graz", cityGameObject),
				new City(3, 8, 2, "Klagenfurt", cityGameObject),
				new City(9, 1, 3, "Innsbruck", cityGameObject),
				new City(9, 2, 4, "Innsbruck", cityGameObject),

				new City(22, 4, 5, "Vienna", cityGameObject),
				new City(1, 11, 6, "Graz", cityGameObject),
				new City(3, 4, 7, "Klagenfurt", cityGameObject),
				new City(55, 1, 8, "Innsbruck", cityGameObject),
				new City(9, 38, 9, "Innsbruck", cityGameObject),

				new City(12, 14, 10, "Vienna", cityGameObject),
				new City(1, 33, 11, "Graz", cityGameObject),
				new City(3, 53, 12, "Klagenfurt", cityGameObject),
				new City(80, 1, 13, "Innsbruck", cityGameObject),
				new City(53, 11, 14, "Innsbruck", cityGameObject),

				new City(22, 42, 15, "Vienna", cityGameObject),
				new City(11, 92, 16, "Graz", cityGameObject),
				new City(32, 82, 17, "Klagenfurt", cityGameObject),
				new City(91, 12, 18, "Innsbruck", cityGameObject),
				new City(92, 12, 19, "Innsbruck", cityGameObject),

				new City(23, 44, 20, "Vienna", cityGameObject),
				new City(14, 94, 21, "Graz", cityGameObject),
				new City(33, 84, 22, "Klagenfurt", cityGameObject),
				new City(94, 14, 23, "Innsbruck", cityGameObject),
				new City(93, 14, 24, "Innsbruck", cityGameObject),

				new City(25, 46, 25, "Vienna", cityGameObject),
				new City(16, 96, 26, "Graz", cityGameObject),
				new City(35, 86, 27, "Klagenfurt", cityGameObject),
				new City(96, 16, 28, "Innsbruck", cityGameObject),
				new City(95, 16, 29, "Innsbruck", cityGameObject),

				new City(26, 47, 30, "Vienna", cityGameObject),
				new City(17, 97, 31, "Graz", cityGameObject),
				new City(36, 87, 32, "Klagenfurt", cityGameObject),
				new City(97, 17, 33, "Innsbruck", cityGameObject),
				new City(96, 17, 34, "Innsbruck", cityGameObject),

				new City(27, 48, 35, "Vienna", cityGameObject),
				new City(18, 98, 36, "Graz", cityGameObject),
				new City(37, 88, 37, "Klagenfurt", cityGameObject),
				new City(98, 18, 38, "Innsbruck", cityGameObject),
				new City(97, 18, 39, "Innsbruck", cityGameObject),

				new City(29, 40, 40, "Vienna", cityGameObject),
				new City(10, 90, 41, "Graz", cityGameObject),
				new City(39, 80, 42, "Klagenfurt", cityGameObject),
				new City(90, 10, 43, "Innsbruck", cityGameObject),
				new City(99, 10, 44, "Innsbruck", cityGameObject),

				new City(82, 44, 45, "Vienna", cityGameObject),
				new City(61, 49, 46, "Graz", cityGameObject),
				new City(73, 48, 47, "Klagenfurt", cityGameObject),
				new City(49, 41, 48, "Innsbruck", cityGameObject),
				new City(39, 41, 49, "Innsbruck", cityGameObject)
			};

			return cities;
		}
	}

	class Eil51 : TspInfo
	{
		public override string GetName()
		{
			return "Eil51";
		}

		public override List<City> Load()
		{
			return TSPImporter.importTsp("eil51.tsp");
		}
	}

	class Berlin52 : TspInfo
	{
		public override string GetName()
		{
			return "Berlin52";
		}

		public override List<City> Load()
		{
			return TSPImporter.importTsp("berlin52.tsp");
		}
	}

	class Dantzig42 : TspInfo
	{
		public override string GetName()
		{
			return "Dantzig42";
		}

		public override List<City> Load()
		{
			return TSPImporter.importTsp("dantzig42.tsp");
		}
	}

	class Dsj1000 : TspInfo
	{
		public override string GetName()
		{
			return "DSJ1000";
		}

		public override List<City> Load()
		{
			return TSPImporter.importTsp("dsj1000.tsp");
		}
	}

	class Eil101 : TspInfo
	{
		public override string GetName()
		{
			return "Eil101";
		}

		public override List<City> Load()
		{
			return TSPImporter.importTsp("eil101.tsp");
		}
	}

	class Fnl4461 : TspInfo
	{
		public override string GetName()
		{
			return "Fnl4461";
		}

		public override List<City> Load()
		{
			return TSPImporter.importTsp("fnl4461.tsp");
		}
	}

	class Si1032 : TspInfo
	{
		public override string GetName()
		{
			return "Si1032.tsp";
		}

		public override List<City> Load()
		{
			return TSPImporter.importTsp("si1032.tsp");
		}
	}

}
