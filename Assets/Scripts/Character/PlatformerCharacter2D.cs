using Spine.Unity;
using UnityEngine;

[DisallowMultipleComponent]
public class PlatformerCharacter2D : MonoBehaviour
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
    
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true; 

    private bool m_Idling = true;
    public delegate void PlayerIdleHandler(bool isIdling);
    public event PlayerIdleHandler PlayerIdleEvent;
    public GameObject touchingObject;
    public bool isPusher;

    public float startingHealth = 10;
    private float health;
    public CheckPoint currentCheckPoint;

	[Header("Graphics")]
	public SkeletonAnimation skeletonAnimation;
    public bool isPuller;

	[Header("Animation")]
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string walkName = "walk";
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string runName = "run";
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string idleName = "idle";
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string jumpName = "jump";
	[SpineAnimation(dataField: "skeletonAnimation")]
	public string actionName = "action";


    private void Awake()
    {
        health = startingHealth;
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
    
        if (health < 0) {
            Die();
        }
    }

    public void Move(float move, bool jump, bool grabbing)
    {
		if (move != 0 || jump) {
			skeletonAnimation.loop = true;
			if(grabbing){
				skeletonAnimation.AnimationName = actionName;
			}else{
				skeletonAnimation.AnimationName = walkName;
			}
            if (m_Idling) {
                var idleEvent = PlayerIdleEvent;
                if (idleEvent != null) {
                    PlayerIdleEvent(false);
                    m_Idling = false;
                }
            }
		} else if (!m_Idling) {
			skeletonAnimation.loop = true;
			skeletonAnimation.AnimationName = idleName;
            var idleEvent = PlayerIdleEvent;
            if (idleEvent != null) {
                PlayerIdleEvent(true);
                m_Idling = true;
            }
        }

        if (m_Grounded || m_AirControl)
        {
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
			skeletonAnimation.loop = false;
			skeletonAnimation.AnimationName = jumpName;
        }

        // If the input is moving the player right and the player is facing left...
        var movingBlock = false;
        if (grabbing && touchingObject != null) {
            var blockScript = touchingObject.GetComponent<InteractiveBlock>();
            if (blockScript != null && !blockScript.IsLocked)
            {
                if (m_Rigidbody2D.velocity.x > 0)
                {
                    if (m_FacingRight && isPusher) {
                        movingBlock = true;
                    } else if (!m_FacingRight && isPuller) {
                        movingBlock = true;
                    }
                }
                else if (m_Rigidbody2D.velocity.x < 0) {
                    if (m_FacingRight && isPuller) {
                        movingBlock = true;
                    } else if (!m_FacingRight && isPusher) {
                        movingBlock = true;
                    }
                }
            }
        } else {
            if (move > 0 && !m_FacingRight) {
				skeletonAnimation.Skeleton.FlipX = !skeletonAnimation.Skeleton.FlipX;
				m_FacingRight = !m_FacingRight;
            } else if (move < 0 && m_FacingRight) {
				skeletonAnimation.Skeleton.FlipX = !skeletonAnimation.Skeleton.FlipX;
				m_FacingRight = !m_FacingRight;
            }
        }

        if (touchingObject != null) {
            if (movingBlock) {
                var blockBuddy = touchingObject.GetComponent<InteractiveBlock>().Buddy;
                if (blockBuddy && blockBuddy.GetComponent<InteractiveBlock>().IsLocked) {
                    return;
                }
                touchingObject.GetComponent<InteractiveBlock>().BeingMovedByPlayer(true);
                touchingObject.GetComponent<Rigidbody2D>().velocity = m_Rigidbody2D.velocity;
            } else {
                touchingObject.GetComponent<InteractiveBlock>().BeingMovedByPlayer(false);
            }
        }
    }

    private void Die() {
        health = startingHealth;
        transform.position = currentCheckPoint.transform.position;
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
    }

    public void TouchingObject(GameObject obj) {
        this.touchingObject = obj;
    }
}
