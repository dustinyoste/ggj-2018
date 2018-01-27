using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Block : MonoBehaviour
{
	public bool IsLocked { get; private set; }

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
}
