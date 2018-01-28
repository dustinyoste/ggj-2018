using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour, IOnUnlock {
    public GameObject Buddy;

    private bool isBeingMovedByPlayer;
    private bool isBeingMovedByBuddy;
	public bool IsLocked = true;

    private Vector2 velocity;

    public void BeingMovedByBuddy(bool isBeingMoved) {
        isBeingMovedByBuddy = isBeingMoved;
    }

    public void BeingMovedByPlayer(bool isBeingMoved) {
        isBeingMovedByPlayer = isBeingMoved;
    }

    private void FixedUpdate()
    {
        if (isBeingMovedByBuddy || isBeingMovedByPlayer) {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        } else {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        }

        if (Buddy != null)
        {
            var velocity = GetComponent<Rigidbody2D>().velocity; 
            if (velocity.magnitude > 0.00 && !isBeingMovedByBuddy)
            {
                Buddy.GetComponent<Rigidbody2D>().velocity = velocity;
                Buddy.GetComponent<BlockScript>().BeingMovedByBuddy(true);
            } else {
                Buddy.GetComponent<BlockScript>().BeingMovedByBuddy(false);
            }     
        }
    }

    private void Update() {
        var rigidbody = GetComponent<Rigidbody2D>();
        velocity = rigidbody.velocity;
    }

	public void OnUnlock()
	{
        IsLocked = false;
    }
}
