using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockable
{
	bool Unlock();
	bool IsLocked
	{
		get;
	}
}
