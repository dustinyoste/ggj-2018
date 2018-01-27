using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedComponent : MonoBehaviour, ILockable
{
	#region  ILockable

	public bool Unlock()
	{
		return locked = false;
	}

	public bool IsLocked
	{
		get
		{
			return locked;
		}
	}

	#endregion

	[SerializeField]
	private bool locked = true;
}
