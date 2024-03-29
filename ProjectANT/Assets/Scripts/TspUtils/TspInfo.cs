﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TspInfo
{
	public abstract int GetId();

	public abstract string GetName();

	public abstract List<City> Load();
}
