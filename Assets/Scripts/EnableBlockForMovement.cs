using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableBlockForMovement : MonoBehaviour, IOnUnlock
{

	InteractiveBlock m_Body;

	void Start()
	{
		m_Body = GetComponent<InteractiveBlock>();
		m_Body.canUpdate = false;
	}

	public void OnUnlock()
	{
		m_Body.canUpdate = true;
	}
}
