using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILockableEntity
{
    bool Unlock();
    bool IsLocked { get; }
}
