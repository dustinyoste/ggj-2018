using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    bool HasRun { get; }
    bool IsSuccess { get; }
    void StartAction();
    void CheckAction();
    void EndAction(bool? result = null);
    void Reset();
}
