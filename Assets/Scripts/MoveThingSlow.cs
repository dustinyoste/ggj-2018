using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThingSlow : MonoBehaviour, IOnUnlock
{
	public float distance = -1.31f;
	public bool force;
	private bool updating;
	private float finaldistance;

	void Start()
	{
		finaldistance = transform.position.y + distance;
	}

	private void Update()
	{
		if (force) {
			force = false;
			OnUnlock();
		}

		if (updating) {
			if(Mathf.Approximately(transform.position.y, finaldistance)){
				return;
			}
			var newDistance = Mathf.Lerp(transform.position.y, finaldistance, 0.1f * Time.deltaTime);
			transform.position = new Vector2(transform.position.x, newDistance);
		}
	}

	public void OnUnlock()
	{
		updating = true;
	}
}
