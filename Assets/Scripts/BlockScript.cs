using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {
    public GameObject Buddy;

    private bool BeingMoved;
    private Vector2 velocity;

    private void FixedUpdate()
    {
        if (Buddy != null)
        {
            if (velocity.magnitude > 0.00 && !BeingMoved)
            {
                Buddy.GetComponent<Rigidbody2D>().velocity = velocity;
                Buddy.GetComponent<BlockScript>().BeingMoved = true;
            } else {
                Buddy.GetComponent<BlockScript>().BeingMoved = false;
            }     
        }
    }

    private void Update() {
        var rigidbody = GetComponent<Rigidbody2D>();
        velocity = rigidbody.velocity;
    }
}
