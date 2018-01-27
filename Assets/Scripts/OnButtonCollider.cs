using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonCollider : MonoBehaviour
{
    public GameObject UnlockComponent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
       UnlockComponent.GetComponent<IOnUnlock>().OnUnlock();
    }
}
