using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Data/Level", order = 1)]
public class Level : ScriptableObject
{
	public string sceneToLoad;
	public Section[] sections;
}

[Serializable]
public struct Section
{
	public int numActionsToHit;
	public Sprite spriteToShow;
}
