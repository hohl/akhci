using System;

public class Gap
{
	public float Position { get; private set; }
	public float Width { get; private set; }
	public City City { get; private set; }

	public Gap(float position, City city)
	{
		Position = position;
		Width = 0.175f;
		City = city;
	}
}
