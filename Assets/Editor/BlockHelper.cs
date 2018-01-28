
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BlockHelper : MonoBehaviour
{
	[MenuItem("MorseBuilder/Block", false, 22)]
	static void AddBlockGameobject(MenuCommand cmd)
	{
		GameObject activeGameObject = Selection.activeGameObject;
		GameObject newObject = CreatePrefabEditor("Assets/Prefabs/ForEditor/EditorBlock.prefab");
		PrefabUtility.DisconnectPrefabInstance(newObject);

		Selection.activeGameObject = newObject;

		if (activeGameObject != null && IsPartOfLevel(activeGameObject)) {
			newObject.transform.SetParent(activeGameObject.transform.parent);
			newObject.transform.localPosition = activeGameObject.transform.localPosition;
			newObject.transform.localRotation = activeGameObject.transform.localRotation;
			newObject.transform.localScale = activeGameObject.transform.localScale;
			GameObject.DestroyImmediate(activeGameObject);
		} else {
			GameObject platformsChildOfLevel = GameObject.Find(kPlatformsChildOfLevelName);
			if (platformsChildOfLevel != null) {
				newObject.transform.SetParent(platformsChildOfLevel.transform);
			}
		}
	}

	[MenuItem("MorseBuilder/ConnectedBlock", false, 22)]
	static void AddConnectedBlockGameobject(MenuCommand cmd)
	{
		GameObject activeGameObject = Selection.activeGameObject;
		GameObject newObject = new GameObject("ConnectedBlock");
		GameObject newObjectOne = CreatePrefabEditor("Assets/Prefabs/ForEditor/EditorBlock.prefab");
		PrefabUtility.DisconnectPrefabInstance(newObjectOne);
		newObjectOne = Instantiate(newObjectOne);
		var newObjectTwo = Instantiate(newObjectOne);

		newObjectOne.transform.SetParent(newObject.transform);
		newObjectTwo.transform.SetParent(newObject.transform);

		newObjectOne.GetComponent<InteractiveBlock>().Buddy = newObjectTwo;
		newObjectTwo.GetComponent<InteractiveBlock>().Buddy = newObjectOne;

		Selection.activeGameObject = newObject;

		if (activeGameObject != null && IsPartOfLevel(activeGameObject)) {
			newObject.transform.SetParent(activeGameObject.transform.parent);
			newObject.transform.localPosition = activeGameObject.transform.localPosition;
			newObject.transform.localRotation = activeGameObject.transform.localRotation;
			newObject.transform.localScale = activeGameObject.transform.localScale;
			GameObject.DestroyImmediate(activeGameObject);
		} else {
			GameObject platformsChildOfLevel = GameObject.Find(kPlatformsChildOfLevelName);
			if (platformsChildOfLevel != null) {
				newObject.transform.SetParent(platformsChildOfLevel.transform);
			}
		}
	}

	public static readonly string kPlatformsChildOfLevelName = "Level";

	public static bool IsPartOfLevel(GameObject gameObject)
	{
		GameObject platformsChildOfLevel = GameObject.Find(kPlatformsChildOfLevelName);
		return IsChildOf(platformsChildOfLevel, gameObject);
	}

	public static GameObject GetChildGameObject(GameObject fromGameObject, string withName)
	{
		var ts = fromGameObject.transform.GetComponentsInChildren<Transform>();
		return (from t in ts where t.gameObject.name == withName select t.gameObject).FirstOrDefault();
	}

	public static bool IsChildOf(GameObject parent, GameObject child)
	{
		var search = GetChildGameObject(parent, child.name);
		return search != null;
	}

	public static GameObject CreatePrefabEditor(string prefabName)
	{
		var prefab = AssetDatabase.LoadAssetAtPath(prefabName, typeof(GameObject));

		if (prefab != null) {
			var clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

			if (clone == null)
				return null;

			if (Selection.activeTransform != null) {
				var parent = Selection.activeTransform.gameObject;
				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				if (parent != null) {
					clone.transform.SetParent(parent.transform);
				}
			}

			clone.transform.localPosition = Vector3.zero;
			clone.transform.localRotation = Quaternion.identity;
			clone.transform.localScale = Vector3.one;

			return clone;
		}
		Debug.Log(string.Format("Couldn't find prefab at {0}", prefabName));

		return null;
	}

	public static GameObject ReplaceWithPrefabEditor(string location)
	{
		var activeGameObject = Selection.activeGameObject;
		var newObject = CreatePrefabEditor(location);

		Selection.activeGameObject = newObject;

		if (activeGameObject == null)
			return newObject;

		newObject.transform.SetParent(activeGameObject.transform.parent);
		newObject.transform.localPosition = activeGameObject.transform.localPosition;
		newObject.transform.localRotation = activeGameObject.transform.localRotation;
		newObject.transform.localScale = activeGameObject.transform.localScale;
		Object.DestroyImmediate(activeGameObject);

		return newObject;
	}
}
