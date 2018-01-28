using UnityEngine;

[DisallowMultipleComponent]
public class ActionControlsUI : MonoBehaviour
{
	public Camera m_MainCamera;
	public CanvasGroup controlsCanvas;
	public Vector3 labelOffset = new Vector3(0f, 40f, 0f);
	public PlatformerCharacter2D playerWithUI;

	private bool m_IsShowing;
	public bool IsShowing
	{
		get { return m_IsShowing; }
		private set
		{
			m_IsShowing = value;
		}
	}

	// Use this for initialization
	void Start()
	{
		var lockedColliders = FindObjectsOfType<OnLockedCollider>();
		foreach (var l in lockedColliders) {
			l.ShowLockedEvent += L_ShowLockedEvent;
		}

		MovementControlsUI.ToggleCanvasGroup(controlsCanvas, false);
	}

	void L_ShowLockedEvent(OnLockedCollider collider, PlatformerCharacter2D player, bool enter)
	{
		if (playerWithUI != player)
			return;

		MovementControlsUI.ToggleCanvasGroup(controlsCanvas, enter);

		m_IsShowing = enter;

		transform.position = m_MainCamera.WorldToScreenPoint(collider.block.transform.position) + labelOffset;

		collider.actionHandler.ActionUIEvent += ActionHandler_ActionUIEvent;
	}


	void ActionHandler_ActionUIEvent(ActionHandler.ActionType type)
	{
		var shouldHide = type == ActionHandler.ActionType.Start;
		MovementControlsUI.ToggleCanvasGroup(controlsCanvas, !shouldHide);
	}
}
