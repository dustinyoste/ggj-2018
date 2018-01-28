using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using NUnit.Framework.Constraints;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(AudioSource))]
public class ActionHandler : MonoBehaviour
{
	public enum ActionType
	{
		Start,
		End,
		Next,
		Reset,
		Fail,
		Pass
	}

	public delegate void ActionUIHandler(ActionType type);
	public event ActionUIHandler ActionUIEvent;

	public GameObject[] ActionList;
	public GameObject[] ActionOnUnlock;
	public float BufferTime;
	public float resetTime;
	public LockedComponent LockedComponent;
	public AudioClip SuccessSound;
	public AudioClip FailSound;

	[SerializeField]
	private SpriteRenderer m_Graphic;

	private int _actionListIndex = 0;
	private bool _canInteract = false;
	private bool _shouldReset = false;
	private float _resetTimer = 0;
	private IAction _currentAction;
	private Color _defaultColor;
	private AudioSource _audioSource;
	private int _player;

	// Use this for initialization
	void Start()
	{
		if (m_Graphic == null) {
			m_Graphic = GetComponentInChildren<SpriteRenderer>();
		}
		_audioSource = GetComponent<AudioSource>();
		_defaultColor = m_Graphic.color;
	}

	// Update is called once per frame
	void Update()
	{
		if (_shouldReset) {
			_resetTimer += Time.deltaTime;

			if (_resetTimer > resetTime) {
				ResetActions();
			}
		} else if (_canInteract && LockedComponent.IsLocked)
		{	
			if (CrossPlatformInputManager.GetButtonDown("Interact" + _player)) {
				StartAction();
			} else if (CrossPlatformInputManager.GetButtonUp("Interact" + _player) && _currentAction != null) {
				_currentAction.EndAction();
			}
		} else if (!_canInteract && _currentAction != null) {
			_currentAction.EndAction();
		}

		if (_currentAction != null && _currentAction.HasRun) {
			if (_currentAction.IsSuccess) GoToNextAction();
			else {
				FailAction();
			}
		}
	}

	void StartAction()
	{
		var actionObject = ActionList[_actionListIndex];
		_currentAction = actionObject.GetComponent<IAction>();
		_currentAction.StartAction();
	}

	void GoToNextAction()
	{
		_currentAction = null;
		if (++_actionListIndex == ActionList.Length) {
			PassAction();
		}
	}

	void ResetActions()
	{
		_shouldReset = false;
		_resetTimer = 0;
		for (int i = 0; i < ActionList.Length; i++) {
			ActionList[i].GetComponent<IAction>().Reset();
		}

		m_Graphic.color = _defaultColor;
	}

	void PassAction()
	{
		Debug.LogFormat("{0} passes its action", name);

		m_Graphic.color = Color.green;
		LockedComponent.Unlock();

		GameController gameController;
		if (GameController.TryGetInstance(out gameController)) {
			gameController.CompleteAction();
		}
		
		var invokeEvent = ActionUIEvent;
		if (invokeEvent != null) {
			invokeEvent(ActionType.Pass);
		}

		if (SuccessSound != null)
		{
			_audioSource.clip = SuccessSound;
			_audioSource.Play();	
		}

		for (int i = 0; i < ActionOnUnlock.Length; i++)
		{
			var unlockComponent = ActionOnUnlock[i].GetComponent<IOnUnlock>();
			if (unlockComponent != null)
			{
				ActionOnUnlock[i].GetComponent<IOnUnlock>().OnUnlock();
			}
			else
			{
				Debug.LogError(ActionOnUnlock[i].name + " has no OnUnlock script");
			}
		}
	}

	void FailAction()
	{
		_actionListIndex = 0;
		_resetTimer = 0;
		_currentAction = null;
		_shouldReset = true;
		m_Graphic.color = Color.red;

		if (FailSound != null)
		{
			_audioSource.clip = FailSound;
			_audioSource.Play();	
		}
	}

	public void StartInteract(int player)
	{
		_canInteract = true;
		_player = player;
		m_Graphic.color = Color.yellow;

		if (LockedComponent.IsLocked)
		{
			var invokeEvent = ActionUIEvent;
			if (invokeEvent != null) {
				invokeEvent(ActionType.Start);
			}
		}
	}

	public void StopInteract()
	{
		_canInteract = false;
		_player = -1;
		m_Graphic.color = _defaultColor;
		
	
		var invokeEvent = ActionUIEvent;
		if (invokeEvent != null) {
			invokeEvent(ActionType.End);
		}

	}
}
