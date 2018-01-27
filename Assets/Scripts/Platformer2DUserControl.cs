using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[DisallowMultipleComponent]
[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public int player;

    private PlatformerCharacter2D m_Character;
    private bool jumping;
    public bool grabbing;
    
    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    private void Update()
    {
        // Read the inputs in Update so button presses aren't missed.
        if (!jumping) {
            jumping= CrossPlatformInputManager.GetButtonDown("Jump"+player);
        }
        if (!grabbing) {
            grabbing = CrossPlatformInputManager.GetButtonDown("Grab"+player);
        } else if (CrossPlatformInputManager.GetButtonUp("Grab"+player)) {
            grabbing = false;
        }
    }


    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal"+player);
        m_Character.Move(h, jumping, grabbing);
        jumping = false;
    }
}
