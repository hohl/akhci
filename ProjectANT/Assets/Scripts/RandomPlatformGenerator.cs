using System;

public class RandomPlatformGenerator : PlatformGenerator
{
	protected Random rand = new Random ();

	public RandomPlatformGenerator (int id) : base (id)
	{
	}

	public override Platform Next ()
	{
		const double buf = 0.225; // <- minimum distance between two gaps

		if (rand.NextDouble () > 0.4f) 
		{
			float first = (float)((1.0 - 2 * buf) * rand.NextDouble () + buf);
			return new Platform(new Gap(-0.2f, null), new Gap(first, null));
		} 
		else 
		{
			float first = (float)((0.5 - buf) * rand.NextDouble () + buf);
			float second = (float)((1.0 - 2 * buf - first) * rand.NextDouble () + buf + first);
			return new Platform(new Gap(first, null), new Gap(second, null));
		}
	}
}
