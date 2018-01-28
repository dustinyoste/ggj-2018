using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityStandardAssets.CrossPlatformInput;

public class ActionHandler : MonoBehaviour
{
	public GameObject[] ActionList;
	public GameObject[] ActionOnUnlock;
	public float BufferTime;
	public float resetTime;
	public LockedComponent LockedComponent;
	public AudioClip SuccessSound;
	public AudioClip FailSound;
	
	private int _actionListIndex = 0;
	private bool _canInteract = false;
	private bool _shouldReset = false;
	private float _resetTimer = 0;
	private IAction _currentAction;
	private Color _defaultColor;
	private SpriteRenderer m_Renderer;
	private AudioSource _audioSource;
    private int _player;	

	// Use this for initialization
	void Start ()
	{
		m_Renderer = GetComponentInChildren<SpriteRenderer>();
		_audioSource = GetComponent<AudioSource>();
		_defaultColor = m_Renderer.color;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_shouldReset)
		{
			_resetTimer += Time.deltaTime;

			if (_resetTimer > resetTime)
			{
				ResetActions();
			}
		}
		else if (_canInteract)
		{

            if(CrossPlatformInputManager.GetButtonDown("Interact"+_player))
			{
				StartAction();
			}
			else if (CrossPlatformInputManager.GetButtonUp("Interact"+_player) && _currentAction != null)
			{
				_currentAction.EndAction();
			}
		}
		else if(!_canInteract && _currentAction != null)
		{
			_currentAction.EndAction();	
		}
		
		if (_currentAction != null && _currentAction.HasRun)
		{
			if (_currentAction.IsSuccess) GoToNextAction();
			else FailAction();
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
		if (++_actionListIndex == ActionList.Length)
		{
			PassAction();
		}
	}

	void ResetActions()
	{
		_shouldReset = false;
		_resetTimer = 0;
		for (int i = 0; i < ActionList.Length; i++)
		{
			ActionList[i].GetComponent<IAction>().Reset();
		}
		
		m_Renderer.color = _defaultColor;
	}

	void PassAction()
	{
		m_Renderer.color = Color.green;
		_audioSource.clip = SuccessSound;
		_audioSource.Play();
	
		for (int i = 0; i < ActionOnUnlock.Length; i++)
		{
			ActionOnUnlock[i].GetComponent<IOnUnlock>().OnUnlock();
		}
	}

	void FailAction()
	{
		_actionListIndex = 0;
		_resetTimer = 0;
		_currentAction = null;
		_shouldReset = true;
		m_Renderer.color = Color.red;
		_audioSource.clip = FailSound;
		_audioSource.Play();
	}

	public void StartInteract(int player)
	{
		_canInteract = true;
        _player = player;
		m_Renderer.color = Color.yellow;
	}

	public void StopInteract()
	{
		_canInteract = false;
        _player = -1;
		m_Renderer.color = _defaultColor;
	}
}
