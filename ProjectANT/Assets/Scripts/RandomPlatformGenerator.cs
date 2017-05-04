using System;

public class RandomPlatformGenerator : PlatformGenerator
{
	private System.Random rand = new System.Random ();

	public override Result Next ()
	{
		return new Result((float)rand.NextDouble() * 0.3f + 0.1f, (float)rand.NextDouble() * 0.3f + 0.6f);
	}
}
