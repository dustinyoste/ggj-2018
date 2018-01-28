using UnityEngine;
using UnityEditor;

public class HelperEditor
{
	[MenuItem("Cheat/UnlockAll", false, 21)]
	static void UnlockAllLockedComponents()
	{
		ActionHandler[] objs = Resources.FindObjectsOfTypeAll<ActionHandler>();
		foreach (ActionHandler obj in objs) {
			obj.SendMessage("PassAction", SendMessageOptions.DontRequireReceiver);
		}
	}
}
