using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Action : MonoBehaviour, IAction
{
    public float MinKeyDownTime;
    public float MaxKeyDownTime;
    public bool HasRun { get; private set; }
    public bool IsSuccess { get; private set; }

    private bool _isRunning;
    private Color _defaultColor;
    
    protected float ActionTime;
    protected Transform ProgressTransform;
    protected Color DefaultProgressColor;

    void Start()
    {
        HasRun = false;
        IsSuccess = false;
        _isRunning = false;
        ActionTime = 0;
        _defaultColor = GetComponent<Renderer>().sharedMaterial.GetColor("_Color");
        ProgressTransform = transform.Find("ProgressContainer").gameObject.transform;
        DefaultProgressColor = ProgressTransform.Find("Progress").GetComponent<Renderer>().material.GetColor("_Color");
        ResetProgress();
    }

    void Update()
    {
        if (_isRunning)
        {
            ActionTime += Time.deltaTime;
            CheckAction();
            ShowProgress();
        }
    }
    
    public void StartAction()
    {
        HasRun = false;
        IsSuccess = false;
        _isRunning = true;
        ActionTime = 0;
    }

    public void CheckAction()
    {
        if (ActionTime > MaxKeyDownTime)
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
        else if (ActionTime < MinKeyDownTime)
        {
            IsSuccess = false;
        }
        else if(ActionTime > MinKeyDownTime)
        {
            IsSuccess = true;
        }

        if (IsSuccess) 
            HandleSuccess();
        else 
            HandleFailure();
        
        ActionTime = 0;
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

    protected virtual void ShowProgress() {}
    protected virtual void ResetProgress() {}
}
