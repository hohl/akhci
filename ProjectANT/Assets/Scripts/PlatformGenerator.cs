using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PlatformGenerator
{
	public virtual City SelectedCity { get; set; }

	public abstract Platform Next ();
}
