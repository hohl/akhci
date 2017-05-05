using System;

public class RandomPlatformGenerator : PlatformGenerator
{
	private System.Random rand = new System.Random ();

	public override Result Next ()
	{
		const float buf = 0.25f; // <- minimum distance between two gaps

		float first = (float)((0.5 - buf) * rand.NextDouble () + buf);
		float second = (float)((1.0 - 2 * buf - first) * rand.NextDouble () + buf + first);
		return new Result(first, null, second, null);
	}
}
