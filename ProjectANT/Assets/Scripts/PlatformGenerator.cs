using System;
using System.Collections.Generic;
using AntAlgorithms;
using UnityEngine;

public abstract class PlatformGenerator
{
	public virtual TspInfo CurrentTsp {	get; set; }

	public virtual int ID {
		get;
		private set;
	}

	public virtual double Result { 
		get {
			return 0;
		}
	}

	public virtual City SelectedCity { get; set; }
	public virtual string Name { get; private set; }

	public abstract Platform Next ();

	public virtual void Finish () 
	{
	}

	public PlatformGenerator(int id, string name)
	{
		this.ID = id;
		this.Name = name;
	}
}
