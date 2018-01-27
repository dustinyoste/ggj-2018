using UnityEngine;

[DisallowMultipleComponent]
public class PlatformerCharacter2Doldscrap : MonoBehaviour
{
    [SerializeField] public float m_MaxSpeed = 10f;
    [SerializeField] public float m_JumpForce = 400f;
    [SerializeField] public bool m_AirControl = true; // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround; // A mask determining what is ground to the character

    private Transform m_GroundCheck;
    const float k_GroundedRadius = 1; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    private Animator m_Anim;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true; 

    private bool m_Idling = true;
    public delegate void PlayerIdleHandler(bool isIdling);
    public event PlayerIdleHandler PlayerIdleEvent;
    public GameObject touchingObject;
    public bool isPusher;
    public bool isPuller;
    public float pushPullMultiplier = 5;

    private void Awake()
    {
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        //m_Anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround); for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                m_Grounded = true;
            }
        }
        //m_Anim.SetBool("Ground", m_Grounded);

        // Set the vertical animation
        //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    public void Move(float move, bool jump, bool grabbing)
    {
        if (move != 0 || jump) {
            if (m_Idling) {
                var idleEvent = PlayerIdleEvent;
                if (idleEvent != null) {
                    PlayerIdleEvent(false);
                    m_Idling = false;
                }
            }
        } else if (!m_Idling) {
            var idleEvent = PlayerIdleEvent;
            if (idleEvent != null) {
                PlayerIdleEvent(true);
                m_Idling = true;
            }
        }

        if (m_Grounded || m_AirControl)
        {
            // The Speed animator parameter is set to the absolute value of the horizontal input.
            //m_Anim.SetFloat("Speed", Mathf.Abs(move));

            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // Stop the player from moving off screen
            var pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x <= 0.05f || pos.x >= 0.95f) {
                var correctMovement = pos.x <= 0.5f ? 0.1f : -0.1f;
                m_Rigidbody2D.velocity = new Vector2(correctMovement, m_Rigidbody2D.velocity.y);
            }
        }

        // If the player should jump...
        if (m_Grounded && jump) // && m_Anim.GetBool("Ground"))
        {
            //m_Anim.SetBool("Ground", false);
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce)); // Add a vertical force to the player.
        }

        var movingBlock = false;
        if (grabbing && touchingObject != null) {
            var blockScript = touchingObject.GetComponent<BlockScript>();
            if (blockScript != null && !blockScript.IsLocked) {
                if (m_Rigidbody2D.velocity.x > 0 && isPusher) {
                    touchingObject.GetComponent<Rigidbody2D>().velocity = m_Rigidbody2D.velocity;
                    movingBlock = true;
                } else if (m_Rigidbody2D.velocity.x < 0 && isPuller) {
                    touchingObject.GetComponent<Rigidbody2D>().velocity = m_Rigidbody2D.velocity;
                    movingBlock = true;
                }

                if (movingBlock) {
                    touchingObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                } else {
                    touchingObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                }
            }
        }

        // If the input is moving the player right and the player is facing left...
        if (!grabbing) { 
            if (move > 0 && !m_FacingRight) {
                Flip();
            } else if (move < 0 && m_FacingRight) {
                // ... flip the player.
                Flip();
            }
        }
    }

    public void TouchingObject(GameObject obj) {
        this.touchingObject = obj;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
