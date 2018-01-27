using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerInGameUI : MonoBehaviour
{
	public PlatformerCharacter2D character;
	public bool playerHasInput = true;
	public bool isIdle = true;
	public CanvasGroup controlsCanvas;
	public float showControlsDelayIncrement = 3f;
	public float showControlsDelayMax = 12f;

	private SimpleTimer playerTimer;
	private float currentShowControlsDelay;

	// Use this for initialization
	void Start()
	{
		Debug.Assert(character != null, "character not set");

		character.PlayerIdleEvent += Character_PlayerIdleEvent;

		ToggleCanvasGroup(controlsCanvas, true);

		NewTimer();
	}

	// Update is called once per frame
	void Update()
	{
		playerTimer.CheckUpdate();
	}

	private void NewTimer()
	{
		currentShowControlsDelay += showControlsDelayIncrement;
		currentShowControlsDelay = Mathf.Min(showControlsDelayMax, currentShowControlsDelay);

		playerTimer = new SimpleTimer(currentShowControlsDelay);
		playerTimer.TimerEndedEvent += PlayerTimeEvent;
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
		if (isIdling) {
			OnPlayerIdle();
		} else {
			OnPlayerMove();
		}
	}

	void PlayerTimeEvent()
	{
		NewTimer();
		ToggleCanvasGroup(controlsCanvas, true);
	}

	public static void ToggleCanvasGroup(CanvasGroup canvas, bool toggle)
	{
		canvas.alpha = toggle ? 1 : 0;
		canvas.interactable = toggle;
		canvas.blocksRaycasts = toggle;
	}
}
