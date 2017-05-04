using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PlatformGenerator
{
	public const int NO_CITY = -1;

	public abstract Result Next ();

	public class Result
	{
		public float first { get; private set; }
		public float firstWidth = 0.2f;
		public int firstCity;
		public float second { get; private set; }
		public float secondWidth = 0.2f;
		public int secondCity;

		public Result(float first, int firstCity, float second, int secondCity)
		{
			this.first = first;
			this.firstCity = firstCity;
			this.second = second;
			this.secondCity = secondCity;
		}
	}
}
