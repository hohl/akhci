
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

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
 
 // Touchable_Editor component, to prevent treating the component as a Text object.
[CustomEditor(typeof(InvisibleTouchable))]
public class InvisibleTouchable_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		// Do nothing
	}
}