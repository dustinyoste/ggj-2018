using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThing : MonoBehaviour, IOnUnlock
{
	public float distance = -1.31f;
	public bool force;
	void Start()
	{
	}

	private void Update()
	{
		if(force){
			force = false;
			OnUnlock();
		}
	}

	public void OnUnlock()
	{
		transform.position = new Vector2(transform.position.x, transform.position.y + distance);
	}
}
