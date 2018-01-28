using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedComponent : MonoBehaviour, ILockable
{
	#region  ILockable
	[SerializeField]
	private bool _locked = true;

	public bool Unlock()
	{
		return _locked = false;
	}

	public bool IsLocked
	{
		get
		{
			return _locked;
		}
	}

	#endregion
}
