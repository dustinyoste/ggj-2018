using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerInGameUI : MonoBehaviour
{
	public PlatformerCharacter2D character;
	public bool playerHasInput = true;
	public bool isIdle = true;
	public bool isInteractionSquare;
	public CanvasGroup controlsCanvas;
	public float showControlsDelay = 5f;

	private SimpleTimer playerTimer;

	// Use this for initialization
	void Start()
	{
		Debug.Assert(character != null, "character not set");

		playerTimer = new SimpleTimer(showControlsDelay);

		playerTimer.TimerEndedEvent += PlayerTimeEvent;
		character.PlayerIdleEvent += Character_PlayerIdleEvent;

		ToggleCanvasGroup(controlsCanvas, true);
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
		ToggleCanvasGroup(controlsCanvas, false);
		isIdle = false;
	}

	void Character_PlayerIdleEvent(bool isIdling)
	{
		if (isInteractionSquare)
			return;
		if (isIdling) {
			OnPlayerIdle();
		} else {
			OnPlayerMove();
		}
	}

	void PlayerTimeEvent()
	{
		ToggleCanvasGroup(controlsCanvas, true);
	}

	public static void ToggleCanvasGroup(CanvasGroup canvas, bool toggle)
	{
		canvas.alpha = toggle ? 1 : 0;
		canvas.interactable = toggle;
		canvas.blocksRaycasts = toggle;
	}
}
