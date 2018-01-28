using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	#region Singleton

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
	}

	protected void OnDestroy()
	{
		sInstance = null;
	}

	#endregion
}
