using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBoxOnUnlock : MonoBehaviour, IOnUnlock {

	void Start()
	{
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
	}

	public void OnUnlock()
	{
		GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
	}
}
