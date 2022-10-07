using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class PlayerHitVfx : VfxBase
{
    [SerializeField] private ParticleSystem[] hitVariation;
    private int currentIndex = 0;

    protected override float GetDuration => hitVariation[currentIndex].main.duration;

    protected override void OnEnable()
    {
        if (doUnparent)
            transform.SetParent(null);
        currentIndex = Random.Range(0, hitVariation.Length);
        hitVariation[currentIndex].Play();
        
        StartCoroutine(ParticleDeath());
    }
}
