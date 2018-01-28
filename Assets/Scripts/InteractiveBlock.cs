using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBlock : MonoBehaviour
{
	public GameObject Buddy;
	public ActionHandler actionHandler;
	public LayerRenderer speechBubble;

	public bool canUpdate = true;

	private bool isBeingMovedByPlayer;
	private bool isBeingMovedByBuddy;
	private GameObject topCollider;

	protected void Start()
	{
		actionHandler.ActionUIEvent += Item_ActionUIEvent;

		StartCoroutine(NextFrame());

		topCollider = transform.Find("TopCollider").gameObject;
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
		if (!canUpdate)
			return;

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
		switch (type) {
			case ActionHandler.ActionType.InitInteraction:
				speechBubble.ToggleLayer(true);
				break;
			case ActionHandler.ActionType.End:
			case ActionHandler.ActionType.Pass:
				speechBubble.ToggleLayer(false);
				break;
		}
	}

	protected void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Hazard>() != null) {
			topCollider.gameObject.transform.localScale = topCollider.gameObject.transform.localScale * new Vector3(1.2, 1, 1);
		}
	}
}
