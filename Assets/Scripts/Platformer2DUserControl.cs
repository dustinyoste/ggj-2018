using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public int player;

    private PlatformerCharacter2D m_Character;
    private bool m_Jump;

    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump"+player);
        }
    }


    private void FixedUpdate()
    {
        bool crouch = false;//Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal"+player);
        m_Character.Move(h, m_Jump);
        m_Jump = false;
    }
}
