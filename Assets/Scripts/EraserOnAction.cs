using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Eraser))]
public class EraserOnAction : MonoBehaviour, IOnUnlock
{
	Eraser eraser;
	private void Start()
	{
		eraser = GetComponent<Eraser>();
		eraser.enabled = false;
	}
	public void OnUnlock()
	{
		eraser.enabled = true;
	}
}
