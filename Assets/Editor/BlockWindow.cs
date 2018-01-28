
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BlockWindow : EditorWindow
{
	string myString = "-*-";

	// Add menu item named "My Window" to the Window menu
	[MenuItem("MorseBuilder/Block Window")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(BlockWindow));
	}

	void OnGUI()
	{
		GUILayout.Label("Enter your code below (* or -)", EditorStyles.boldLabel);
		myString = EditorGUILayout.TextField("Morse Code", myString);

		if (GUILayout.Button(EditorGUIUtility.IconContent("Toolbar Plus", "Add to list"), GUIStyle.none))
			BuildBlock(myString);
	}

	private static void BuildBlock(string mystring)
	{
		GameObject activeGameObject = Selection.activeGameObject;
		GameObject newObject = BlockHelper.CreatePrefabEditor("Assets/Prefabs/ForEditor/EditorBlock.prefab");
		PrefabUtility.DisconnectPrefabInstance(newObject);

		// Cheap way of finding the speech bubble
		var speechBubble = newObject.GetComponentInChildren<LayerRenderer>();
		var actionHandler = newObject.GetComponentInChildren<ActionHandler>();
		List<Action> actions = new List<Action>();
		foreach (var item in mystring) {
			if (item == '-') {
				GameObject line = BlockHelper.CreatePrefabEditor("Assets/Prefabs/ActionElementLine.prefab");
				PrefabUtility.DisconnectPrefabInstance(line);
				line.transform.SetParent(speechBubble.transform);
				actions.Add(line.GetComponent<Action>());
			} else if (item == '*') {
				GameObject dot = BlockHelper.CreatePrefabEditor("Assets/Prefabs/ActionElementDot.prefab");
				PrefabUtility.DisconnectPrefabInstance(dot);
				dot.transform.SetParent(speechBubble.transform);
				actions.Add(dot.GetComponent<Action>());
			}
		}
		actionHandler.ActionList = actions.ToArray();

		Selection.activeGameObject = newObject;

		if (activeGameObject != null && BlockHelper.IsPartOfLevel(activeGameObject)) {
			newObject.transform.SetParent(activeGameObject.transform.parent);
			newObject.transform.localPosition = activeGameObject.transform.localPosition;
			newObject.transform.localRotation = activeGameObject.transform.localRotation;
			newObject.transform.localScale = activeGameObject.transform.localScale;
			GameObject.DestroyImmediate(activeGameObject);
		} else {
			GameObject platformsChildOfLevel = GameObject.Find(BlockHelper.kPlatformsChildOfLevelName);
			if (platformsChildOfLevel != null) {
				newObject.transform.SetParent(platformsChildOfLevel.transform);
			}
		}
	}
}