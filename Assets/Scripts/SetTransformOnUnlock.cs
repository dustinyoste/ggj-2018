using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTransformOnUnlock : MonoBehaviour, IOnUnlock
{
	public Vector3 position;

	public void OnUnlock()
	{
		transform.localPosition =  position;
	}
}
