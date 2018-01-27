using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DotAction : MonoBehaviour, IAction
{
    public float MinKeyDownTime;
    public float MaxKeyDownTime;
    public bool HasRun { get; private set; }
    public bool IsSuccess { get; private set; }

    private bool _isRunning;
    private float _actionTime;
    private Color _defaultColor;

    void Start()
    {
        HasRun = false;
        IsSuccess = false;
        _isRunning = false;
        _actionTime = 0;
        _defaultColor = GetComponent<Renderer>().material.GetColor("_Color");
    }

    void Update()
    {
        if (_isRunning)
        {
            _actionTime += Time.deltaTime;
            CheckAction();
        }
    }
    
    public void StartAction()
    {
        HasRun = false;
        IsSuccess = false;
        _isRunning = true;
        _actionTime = 0;
    }

    public void CheckAction()
    {
        if (_actionTime > MaxKeyDownTime)
        {
            EndAction(false);
        }
    }

    public void EndAction(bool? result = null)
    {
        HasRun = true;
        _isRunning = false;

        if (result != null)
        {
            IsSuccess = (bool) result;
        }
        else if (_actionTime < MinKeyDownTime)
        {
            IsSuccess = false;
        }
        else if(_actionTime > MinKeyDownTime)
        {
            IsSuccess = true;
        }

        if (IsSuccess) 
            HandleSuccess();
        else 
            HandleFailure();
        
        _actionTime = 0;
    }

    public void Reset()
    {
        var renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", _defaultColor);
        Start();
    }

    void HandleSuccess()
    {
        var renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.green);
    }

    void HandleFailure()
    {
        var renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_Color", Color.red);
    }
}
