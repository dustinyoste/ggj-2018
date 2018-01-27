using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class ActionHandler : MonoBehaviour
{
	public GameObject[] ActionList;
	public float BufferTime;
	public float resetTime;
	public LockedComponent LockedComponent;
	
	private int _actionListIndex = 0;
	private bool _canInteract = false;
	private bool _shouldReset = false;
	private float _resetTimer = 0;
	private IAction _currentAction;
	private Color _defaultColor;
	

	// Use this for initialization
	void Start ()
	{
		_defaultColor = GetComponent<SpriteRenderer>().color;
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
			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				StartAction();
			}
			else if (Input.GetKeyUp(KeyCode.LeftShift) && _currentAction != null)
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
		
		GetComponent<SpriteRenderer>().color = _defaultColor;
	}

	void PassAction()
	{
		GetComponent<SpriteRenderer>().color = Color.green;
	}

	void FailAction()
	{
		_actionListIndex = 0;
		_resetTimer = 0;
		_currentAction = null;
		_shouldReset = true;
		GetComponent<SpriteRenderer>().color = Color.red;
	}

	public void StartInteract()
	{
		_canInteract = true;
		GetComponent<SpriteRenderer>().color = Color.yellow;
	}

	public void StopInteract()
	{
		_canInteract = false;
		GetComponent<SpriteRenderer>().color = _defaultColor;
	}
}
