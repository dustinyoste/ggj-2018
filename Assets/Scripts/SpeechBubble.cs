using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
	public ActionHandler actionHandler;
	public LayerRenderer speechBubble;

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
}
