using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBlock : MonoBehaviour, IOnUnlock
{

    private LockedComponent _lockedComponent;
    
    void Start()
    {
        _lockedComponent = GetComponent<LockedComponent>();
    }
    
    public void OnUnlock()
    {
        _lockedComponent.Unlock();
    }
}
