using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class LowerDrawbridge : MonoBehaviour, IOnUnlock
{
	public float rotationSpeed;
	public float startRotation;
	public float endRotation;
	
	private bool _isLowered;
	private bool _startLowering;
	
	// Use this for initialization
	void Start ()
	{
		_isLowered = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_startLowering)
		{	
			if (gameObject.transform.localEulerAngles.z >= endRotation)
			{
				_isLowered = true;
				_startLowering = false;
				gameObject.transform.localEulerAngles = new Vector3(0, 0, endRotation);
			}
			else
			{
				gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));	
			}
		}
	}

	public void OnUnlock()
	{
		if (!_isLowered)
		{
			_startLowering = true;		
		}
	}
}
