using UnityEngine;
using UnityEditor;

// Touchable_Editor component, to prevent treating the component as a Text object.
[CustomEditor(typeof(InvisibleTouchable))]
public class InvisibleTouchable_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		// Do nothing
	}
}