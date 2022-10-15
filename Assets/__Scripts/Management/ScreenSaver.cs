using System;
using System.Collections;
using System.Collections.Generic;
using ExtendedClasse;
using UnityEngine;

public class ScreenSaver : MonoBehaviour
{
    [SerializeField] protected Rigidbody myRigidbody;

    public event Action Wrapped;
    
    private void OnValidate()
    {
        CheckReferences();
    }

    private void CheckReferences()
    {
        if (!myRigidbody)
        {
            myRigidbody = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        CheckReferences();
        if (myRigidbody.velocity != Vector3.zero)
        {
            transform.CheckLimits(Wrapped);
        }
    }
}
