using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, ILockableEntity {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool Unlock()
	{
		return false;
	}

	public bool IsLocked { get; private set; }
}
