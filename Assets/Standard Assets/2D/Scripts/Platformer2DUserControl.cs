using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        public int player;
        public string jump;
        public string horizontal;

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
                m_Jump = CrossPlatformInputManager.GetButtonDown(jump);
            }
        }


        private void FixedUpdate()
        {
            //Debug.Log("m_jump");
            //Debug.Log(m_Jump);
            // Read the inputs.
            bool crouch = false;//Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis(horizontal);
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
