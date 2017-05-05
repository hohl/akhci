using System;

public class Platform
{
	public Gap FirstGap { get; private set; }
	public Gap SecondGap { get; private set; }

	public Platform(Gap first, Gap second)
	{
		FirstGap = first;
		SecondGap = second;
	}
}