using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LineAction : Action
{
	private bool _isValidTime = false;
	protected override void ShowProgress()
	{
		ProgressTransform.localScale = new Vector2((ActionTime / MaxKeyDownTime), ProgressTransform.localScale.y);

		if (!_isValidTime && ActionTime > MinKeyDownTime)
		{
			_isValidTime = true;
			ProgressTransform.Find("Progress").GetComponent<Renderer>().material.SetColor("_Color", Color.green);
		}
	}
}
