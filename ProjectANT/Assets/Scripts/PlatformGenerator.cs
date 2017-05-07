using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PlatformGenerator
{
	public virtual double Result { 
		get {
			return 0;
		}
	}

	public virtual City SelectedCity { get; set; }

	public abstract Platform Next ();

	public virtual void Finish () 
	{
	}
}
