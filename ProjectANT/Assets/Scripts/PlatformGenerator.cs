using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PlatformGenerator
{
	public virtual City SelectedCity { get; set; }

	public abstract Result Next ();

	public class Result
	{
		public float first { get; private set; }
		public float firstWidth = 0.2f;
		public City firstCity;
		public float second { get; private set; }
		public float secondWidth = 0.2f;
		public City secondCity;

		public Result(float first, City firstCity, float second, City secondCity)
		{
			this.first = first;
			this.firstCity = firstCity;
			this.second = second;
			this.secondCity = secondCity;
		}
	}
}
