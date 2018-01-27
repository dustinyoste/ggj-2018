using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerInGameUI : MonoBehaviour
{
	void L_ShowLockedEvent(PlatformerCharacter2D player, bool enter)
	{
		if (player != character)
			return;
		ToggleCanvasGroup(playerControls, enter);
	}

	public PlatformerCharacter2D character;
	public bool playerHasInput = true;
	public bool isIdle = true;
	public bool isInteractionSquare;
	public CanvasGroup playerControls;
	public float showControlsDelay = 5f;

	private SimpleTimer playerTimer;

	// Use this for initialization
	void Start()
	{
		Debug.Assert(character != null, "character not set");

		playerTimer = new SimpleTimer(showControlsDelay);

		playerTimer.TimerEndedEvent += PlayerTimeEvent;
		character.PlayerIdleEvent += Character_PlayerIdleEvent;

		ToggleCanvasGroup(playerControls, true);

		var lockedColliders = FindObjectsOfType<OnLockedCollider>();
		foreach (var l in lockedColliders) {
			l.ShowLockedEvent += L_ShowLockedEvent;
		}
	}

	// Update is called once per frame
	void Update()
	{
		playerTimer.CheckUpdate();
	}

	void OnPlayerIdle()
	{
		if (isIdle)
			return;
		Debug.LogFormat("OnPlayerIdle");
		playerTimer.Enable();
		playerHasInput = false;
		isIdle = true;
	}

	void OnPlayerMove()
	{
		if (!isIdle)
			return;
		Debug.LogFormat("OnPlayerMove");
		playerTimer.Disable();
		playerHasInput = true;
		ToggleCanvasGroup(playerControls, false);
		isIdle = false;
	}

	void Character_PlayerIdleEvent(bool isIdling)
	{
		Debug.Log("Character_PlayerIdleEvent");
		if (isInteractionSquare)
			return;
		if (isIdling) {
			OnPlayerIdle();
		} else {
			OnPlayerMove();
		}
	}

	void CharacterInInteractionSquare(bool show)
	{
		isInteractionSquare = show;
		ToggleCanvasGroup(playerControls, show);
	}

	void PlayerTimeEvent()
	{
		Debug.LogFormat("PlayerTimeEvent");
		ToggleCanvasGroup(playerControls, true);
	}

	static void ToggleCanvasGroup(CanvasGroup canvas, bool toggle)
	{
		canvas.alpha = toggle ? 1 : 0;
		canvas.interactable = toggle;
		canvas.blocksRaycasts = toggle;
	}
}
