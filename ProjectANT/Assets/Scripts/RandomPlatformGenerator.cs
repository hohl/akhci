using System;

public class RandomPlatformGenerator : PlatformGenerator
{
	protected System.Random rand = new System.Random ();

	public override Platform Next ()
	{
		const float buf = 0.225f; // <- minimum distance between two gaps

		float first = (float)((0.5 - buf) * rand.NextDouble () + buf);
		float second = (float)((1.0 - 2 * buf - first) * rand.NextDouble () + buf + first);
		return new Platform(new Gap(first, null), new Gap(second, null));
	}
}
