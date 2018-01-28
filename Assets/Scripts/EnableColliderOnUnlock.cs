using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableColliderOnUnlock : MonoBehaviour, IOnUnlock {

	Collider2D m_Body;

	void Start()
	{
		m_Body = GetComponent<Collider2D>();
	}

	public void OnUnlock()
	{
		m_Body.enabled = true;
	}
}
