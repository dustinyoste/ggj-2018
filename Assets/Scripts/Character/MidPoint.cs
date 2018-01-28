using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPoint : MonoBehaviour
{
	public Transform m_Player1;
	public Transform m_Player2;

	void Update()
	{
		Vector3 middle = (m_Player1.position + m_Player2.position) * 0.5f;
		transform.position = new Vector3(
			middle.x,
			middle.y,
			transform.transform.position.z
		);

	}
}
