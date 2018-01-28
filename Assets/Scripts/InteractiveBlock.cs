using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBlock : MonoBehaviour
{
	public GameObject Buddy;
	public ActionHandler actionHandler;
	public LayerRenderer speechBubble;

	private bool isBeingMovedByPlayer;
	private bool isBeingMovedByBuddy;

	protected void Start()
	{
		actionHandler.ActionUIEvent += Item_ActionUIEvent;

		StartCoroutine(NextFrame());
	}

	IEnumerator NextFrame()
	{
		yield return 0;
		speechBubble.ToggleLayer(false);
	}

	public void BeingMovedByBuddy(bool isBeingMoved)
	{
		isBeingMovedByBuddy = isBeingMoved;
	}

	public void BeingMovedByPlayer(bool isBeingMoved)
	{
		isBeingMovedByPlayer = isBeingMoved;
	}

	private void FixedUpdate()
	{
		if (isBeingMovedByBuddy || isBeingMovedByPlayer) {
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		} else {
			GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
		}

		if (Buddy != null) {
			var velocity = GetComponent<Rigidbody2D>().velocity;
			if (velocity.magnitude > 0.00 && !isBeingMovedByBuddy) {
				Buddy.GetComponent<Rigidbody2D>().velocity = velocity;
				Buddy.GetComponent<InteractiveBlock>().BeingMovedByBuddy(true);
			} else {
				Buddy.GetComponent<InteractiveBlock>().BeingMovedByBuddy(false);
			}
		}
	}

	void Item_ActionUIEvent(ActionHandler.ActionType type)
	{
		var shouldShow = type == ActionHandler.ActionType.Start;
		speechBubble.ToggleLayer(shouldShow);
	}
}