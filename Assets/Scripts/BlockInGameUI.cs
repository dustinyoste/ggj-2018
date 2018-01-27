using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class BlockInGameUI : MonoBehaviour
{
	public Camera m_MainCamera;
	public CanvasGroup playerControls;
	public GameObject block;
	public Vector3 labelOffset = new Vector3(0f, 40f, 0f);
	public PlatformerCharacter2D playerWithUI;

	// Use this for initialization
	void Start()
	{
		var lockedColliders = FindObjectsOfType<OnLockedCollider>();
		foreach (var l in lockedColliders) {
			l.ShowLockedEvent += L_ShowLockedEvent;
		}

		PlayerInGameUI.ToggleCanvasGroup(playerControls, false);
	}

	void L_ShowLockedEvent(PlatformerCharacter2D player, bool enter)
	{
		Debug.LogFormat("L_ShowLockedEvent {0} | {1}", player.name, enter);
		if (playerWithUI != player)
			return;
		
		PlayerInGameUI.ToggleCanvasGroup(playerControls, enter);

		transform.position = m_MainCamera.WorldToScreenPoint(block.transform.position) + labelOffset;
	}
}
