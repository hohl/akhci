using AntAlgorithms;
using System;

public class BestDistancesPlatformGenerator : PheromonesBasedPlatformGenerator
{
	public BestDistancesPlatformGenerator (AntAlgorithm algo, int id, string name) : base(algo, id, name)
	{
	}

	protected override void SortCities (City startCity)
	{
		cities.Sort (delegate(City a, City b) {
			double deltaA = DistanceBetween (startCity, a);
			double deltaB = DistanceBetween (startCity, b);
			return deltaA.CompareTo(deltaB);
		});
	}

	private double DistanceBetween(City a, City b)
	{
		return Math.Sqrt(Math.Pow(a.getXPosition() - b.getXPosition(), 2) + Math.Pow(a.getYPosition() - b.getYPosition(), 2) );
	}
}
