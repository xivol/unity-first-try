using UnityEngine;
using UnityEditor.Animations;
using System;
using System.Collections.Generic;

public class Core<T> : MonoBehaviour where T : MonoBehaviour
{
    
    #region Messages
    protected virtual void Awake()
    {
        //Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
        if (Application.isPlaying) Debug.Log("Awakening " + this.ScriptName());
    }

    protected virtual void Start()
    {
        if (Application.isPlaying) Debug.Log("Starting " + this.ScriptName());
    }

    protected virtual void OnEnable()
    {
        if (Application.isPlaying) Debug.Log("Enabling " + this.ScriptName());
    }

    protected virtual void OnDisable()
    {
        if (Application.isPlaying) Debug.Log("Disabling " + this.ScriptName());
    }

    protected virtual void OnDestroy()
    {
        if (Application.isPlaying) Debug.Log("Destroying " + this.ScriptName());
    }
    #endregion

    public virtual void Log(string Message)
    {
        Debug.Log(name + " logs \"" + Message +"\"");
    }
}

