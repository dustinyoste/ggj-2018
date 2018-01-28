using UnityEngine;
using UnityEditor;

public class HelperEditor
{
	[MenuItem("Cheat/UnlockAll", false, 21)]
	static void UnlockAllLockedComponents()
	{
		LockedComponent[] objs = Resources.FindObjectsOfTypeAll<LockedComponent>();
		foreach (LockedComponent obj in objs) {
			obj.Unlock();
		}
	}
}
