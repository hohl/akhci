
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TspLoader
{
	private static TspLoader instance;
	private Dictionary<int, TspInfo> allTSPs = new Dictionary<int, TspInfo>();

	private TspLoader() {
		//  new TspTest8(), new TspTest50()//, new Berlin52(), new Eil51()
		// These will load but take a bit long:
		//		new Eil101(),
		// These all won't load???
		//		new Dantzig42(), new Dsj1000(), new Fnl4461(), new Si1032()new TspTest8(), new TspTest50()


		TspInfo tsp = new TspTest8();
		allTSPs.Add(tsp.GetId(), tsp);
		
		tsp = new TspTest50();
		allTSPs.Add(tsp.GetId(), tsp);
	}

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

	public Dictionary<int, TspInfo> GetAllTSPs()
	{
		return allTSPs;
	}

	public string GetTspNameById(int id)
	{
		TspInfo value = null;
		bool found = allTSPs.TryGetValue(id, out value);

		return found ? value.GetName() : "?(" + id + ")";
	}

	public TspInfo SelectRandomTsp()
	{
		System.Random rnd = new System.Random(System.DateTime.Now.Millisecond);
		int index = rnd.Next(0, allTSPs.Count);
		List<int> keys = new List<int>(allTSPs.Keys);
		return allTSPs[keys[index]];
	}

	// TSP generation here below
	class TspTest8 : TspInfo
	{
		public override int GetId()
		{
			return 8;
		}

		public override string GetName()
		{
			return "test8";
		}

		public override List<City> Load()
		{
			List<City> cities = new List<City>
			{
				new City(2, 4, 0),
				new City(1, 9, 1),
				new City(3, 8, 2),
				new City(9, 1, 3),
				new City(10, 1, 4),

				new City(5, 4, 5),
				new City(1, 11, 6),
				new City(3, 4, 7)
			};

			return cities;
		}
	}
	class TspTest50 : TspInfo
	{
		public override int GetId()
		{
			return 50;
		}

		public override string GetName()
		{
			return "test50";
		}

		public override List<City> Load()
		{

			List<City> cities = new List<City>
			{
				new City(2, 4, 0),
				new City(1, 9, 1),
				new City(3, 8, 2),
				new City(9, 1, 3),
				new City(9, 2, 4),

				new City(22, 4, 5),
				new City(1, 11, 6),
				new City(3, 4, 7),
				new City(55, 1, 8),
				new City(9, 38, 9),

				new City(12, 14, 10),
				new City(1, 33, 11),
				new City(3, 53, 12),
				new City(80, 1, 13),
				new City(53, 11, 14),

				new City(22, 42, 15),
				new City(11, 92, 16),
				new City(32, 82, 17),
				new City(91, 12, 18),
				new City(92, 12, 19),

				new City(23, 44, 20),
				new City(14, 94, 21),
				new City(33, 84, 22),
				new City(94, 14, 23),
				new City(93, 14, 24),

				new City(25, 46, 25),
				new City(16, 96, 26),
				new City(35, 86, 27),
				new City(96, 16, 28),
				new City(95, 16, 29),

				new City(26, 47, 30),
				new City(17, 97, 31),
				new City(36, 87, 32),
				new City(97, 17, 33),
				new City(96, 17, 34),

				new City(27, 48, 35),
				new City(18, 98, 36),
				new City(37, 88, 37),
				new City(98, 18, 38),
				new City(97, 18, 39),

				new City(29, 40, 40),
				new City(10, 90, 41),
				new City(39, 80, 42),
				new City(90, 10, 43),
				new City(99, 10, 44),

				new City(82, 44, 45),
				new City(61, 49, 46),
				new City(73, 48, 47),
				new City(49, 41, 48),
				new City(39, 41, 49)
			};

			return cities;
		}
	}

	class Eil51 : TspInfo
	{
		public override int GetId()
		{
			return 51;
		}
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
		public override int GetId()
		{
			return 52;
		}
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
		public override int GetId()
		{
			return 42;
		}
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
		public override int GetId()
		{
			return 1000;
		}

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
		public override int GetId()
		{
			return 101;
		}

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
		public override int GetId()
		{
			return 14461;
		}

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
		public override int GetId()
		{
			return 1032;
		}

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
