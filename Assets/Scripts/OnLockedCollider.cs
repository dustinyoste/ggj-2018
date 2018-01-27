using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
public class OnLockedCollider : MonoBehaviour
{
	public delegate void ShowLockedHandler(PlatformerCharacter2D player, bool enter);
	public event ShowLockedHandler ShowLockedEvent;

	public LockedComponent lockedObject;
	public ActionHandler actionHandler;

	protected void OnTriggerEnter2D(Collider2D coll)
	{
		var player = coll.GetComponent<PlatformerCharacter2D>();
		if (player == null)
			return;

		if (!lockedObject.IsLocked)
			return;

		// Show the UI
		var invokeEvent = ShowLockedEvent;
		if (invokeEvent != null) {
			invokeEvent(player, true);
		}

		// Allow interactablility
		actionHandler.StartInteract();
		
	}

	protected void OnTriggerExit2D(Collider2D coll)
	{
		var player = coll.GetComponent<PlatformerCharacter2D>();
		if (player == null)
			return;

		if (!lockedObject.IsLocked)
			return;

		// Show the UI
		var invokeEvent = ShowLockedEvent;
		if (invokeEvent != null) {
			invokeEvent(player, false);
		}
		
		// Allow interactablility
		actionHandler.StopInteract();
	}
}
