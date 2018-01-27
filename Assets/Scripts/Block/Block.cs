using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Block : MonoBehaviour, ILockableEntity
{
	public bool IsLocked;

	// Use this for initialization
	void Start ()
	{
		IsLocked = true;
		var renderer = GetComponent<Renderer>();
		renderer.material.SetColor("_Color", Color.red);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Unlock()
	{
		IsLocked = false;
		
		var renderer = GetComponent<Renderer>();
		renderer.material.SetColor("_Color", Color.green);

	}
}
