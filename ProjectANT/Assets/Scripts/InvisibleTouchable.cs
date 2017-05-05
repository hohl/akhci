using UnityEngine.UI;

// enables invisible button
// thanks obama! ahm, i mean: 
// http://answers.unity3d.com/answers/851816/view.html

public class InvisibleTouchable : Text
{
	protected override void Awake()
	{
		base.Awake();
	}
}
