using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Object = UnityEngine.Object;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T _i;
    private static readonly Object lockState = new Object();

    //Singleton Reference
    public static T Instance
    {
        get
        {
            lock (lockState)
            {
                if (!_i)
                {
                    _i = FindObjectOfType<T>() ?? new GameObject().AddComponent<T>();
                }
            }

            return _i;
        }
    }

    /// <summary>
    /// Lean Shortcut to the instance
    /// </summary>
    public static T I => Instance;

    private void Awake()
    {
        if (_i != null)
        {
            Destroy(this);
            throw new WarningException($"Instance of GameManager Allready exists, make sure that has only one on the scene!!");
        }
    }
}
