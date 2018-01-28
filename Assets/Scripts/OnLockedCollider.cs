
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BoxCollider2D))]
public class OnLockedCollider : MonoBehaviour
{
	public delegate void ShowLockedHandler(OnLockedCollider collider, PlatformerCharacter2D player, bool enter);
	public event ShowLockedHandler ShowLockedEvent;

	public LockedComponent lockedObject;
	public ActionHandler actionHandler;
	public GameObject block;

	protected void Start()
	{
		Debug.Assert(block != null, "block not set");
		Debug.Assert(lockedObject != null, "lockedObject not set");
	}

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
			invokeEvent(this, player, true);
		}

		// Allow interactablility
		if (actionHandler) {
			var playerControl = coll.GetComponent<Platformer2DUserControl>();
			actionHandler.StartInteract(playerControl.player);
		}

        player.TouchingObject(this.block);
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
			invokeEvent(this, player, false);
		}

		// Allow interactablility
		if (actionHandler) {
			actionHandler.StopInteract();
		}

        player.TouchingObject(null);
	}
}
