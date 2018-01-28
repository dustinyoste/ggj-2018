using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveThing : MonoBehaviour, IOnUnlock {

    void Start() {
    }

    public void OnUnlock() {
        transform.position = new Vector2(transform.position.x + 10, transform.position.y);
    }
}
