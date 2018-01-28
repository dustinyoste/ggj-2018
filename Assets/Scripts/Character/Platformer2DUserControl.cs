using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[DisallowMultipleComponent]
[RequireComponent(typeof (PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public int player;

    private PlatformerCharacter2D m_Character;
    public bool grabbing;
    
    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
    }

    private void Update()
    {
        if (!grabbing) {
            grabbing = CrossPlatformInputManager.GetButtonDown("Interact"+player);
        } else if (CrossPlatformInputManager.GetButtonUp("Interact"+player)) {
            grabbing = false;
        }
    }


    private void FixedUpdate()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal"+player);
        m_Character.Move(h, grabbing);
    }
}
