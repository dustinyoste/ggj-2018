using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class OnLockedCollider : MonoBehaviour
{
	public LockedComponent lockedObject;

	protected void OnTriggerStay2D(Collider2D coll)
	{
		var player = coll.GetComponent<PlatformerCharacter2D>();
		if (player == null)
			return;

		// Show the UI

		// Allow interactablility
	}
}
