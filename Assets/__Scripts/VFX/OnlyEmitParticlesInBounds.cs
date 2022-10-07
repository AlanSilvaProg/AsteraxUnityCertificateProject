using System;
using System.Collections;
using System.Collections.Generic;
using ExtendedClasse;
using UnityEngine;

/// <summary>
/// This class stops any attached ParticleSystems from emitting particles if the
///  GameObject to which it is attached is out of bounds of the ScreenBounds.
/// </summary>
[RequireComponent( typeof(ParticleSystem) )]
public class OnlyEmitParticlesInBounds : MonoBehaviour {
    
    private ParticleSystem.EmissionModule emitter;

	// Use this for initialization
	void Start () {
        // Get the EmissionModule of the attached ParticleSystem
        emitter = GetComponent<ParticleSystem>().emission;
	}

	private void LateUpdate()
	{
		emitter.enabled = !transform.OutOfBounds();
	}
}
