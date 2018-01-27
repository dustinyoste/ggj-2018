using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTimer
{
	public delegate void TimerEndedHandler();
	public event TimerEndedHandler TimerEndedEvent;

	public float targetTime;
	private bool isEnabled;
	private float currentTargetTime;

	public SimpleTimer(float targetTime = 60.0f)
	{
		this.targetTime = targetTime;
	}

	public void Enable()
	{
		currentTargetTime = targetTime;
		isEnabled = true;
	}

	public void Disable()
	{
		isEnabled = false;
	}

	public void CheckUpdate()
	{
		if (!isEnabled)
			return;

		currentTargetTime -= Time.deltaTime;

		if (currentTargetTime <= 0.0f) {
			var invokeEvent = TimerEndedEvent;
			if (invokeEvent != null) {
				invokeEvent();
			}
			isEnabled = false;
		}
	}
}