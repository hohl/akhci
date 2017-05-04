using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PlatformGenerator
{
	public abstract Result Next ();

	public class Result
	{
		public float first { get; private set; }
		public float firstWidth = 0.2f;
		public float second { get; private set; }
		public float secondWidth = 0.2f;

		public Result(float first, float second)
		{
			this.first = first;
			this.second = second;
		}
	}
}
