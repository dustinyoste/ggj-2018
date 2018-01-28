using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	#region Singleton

	public Level LevelData;
	public Canvas BackgroundCanvas;
	
	private int _actionsComplete;

	public static GameController Instance
	{
		get
		{
			return sInstance;
		}
	}

	public static bool TryGetInstance(out GameController returnVal)
	{
		returnVal = sInstance;
		if (returnVal == null) {
			Debug.LogWarning(string.Format("Couldn't access {0}", typeof(GameController).Name));
		}
		return (returnVal != null);
	}

	private static GameController sInstance;

	#endregion

	#region Monobehaviours

	protected void Awake()
	{
		if (sInstance == null) {
			sInstance = this;
		}
		
		UpdateBackground();
	}

	protected void OnDestroy()
	{
		sInstance = null;
	}

	public void CompleteAction()
	{
		_actionsComplete++;
		UpdateBackground();
	}
	
	void UpdateBackground()
	{
		Section newSection = LevelData.sections[0];
		for (int i = 0; i < LevelData.sections.Length; i++)
		{
			if (LevelData.sections[i].numActionsToHit <= _actionsComplete && 
			    LevelData.sections[i].numActionsToHit > newSection.numActionsToHit)
			{	
				newSection = LevelData.sections[i];
			}
		}

		BackgroundCanvas.GetComponentInChildren<Image>().sprite = newSection.spriteToShow;
	}

	#endregion
}
