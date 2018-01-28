using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

    private Transform m_Link1;
    private Transform m_Link2;
    private LineRenderer m_LineRenderer;

    private void Awake()
    {
        m_Link1 = transform.GetChild(0);
        m_Link2 = transform.GetChild(1);
        m_LineRenderer = GetComponent<LineRenderer>();
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    void FixedUpdate() {
		m_LineRenderer.SetPosition(0, m_Link1.position);
		m_LineRenderer.SetPosition(1, m_Link2.position);
    }

}
