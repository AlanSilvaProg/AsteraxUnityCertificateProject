using System.Collections;
using UnityEngine;

public class VfxBase : MonoBehaviour
{
    [SerializeField] private ParticleSystem myParticleSystem;
    [SerializeField] private VfxType vfxType;
    [SerializeField] protected bool doUnparent = true;
    protected virtual float GetDuration => myParticleSystem.main.duration;
    
    enum VfxType
    {
        EXPLOSION,
        PLAYERHIT
    }
    
    protected virtual void OnEnable()
    {
        if (doUnparent)
            transform.SetParent(null);
        myParticleSystem.Play();
        StartCoroutine(ParticleDeath());
    }

    protected virtual IEnumerator ParticleDeath()
    {
        yield return new WaitForSeconds(GetDuration);
        if (myParticleSystem)
            myParticleSystem.Stop();
        PutOnPool();
    }
    
    protected virtual void PutOnPool()
    {
        switch (vfxType)
        {
            case VfxType.EXPLOSION:
                GameManager.Instance.explosionVfxPoolSystem.PutObjectIntoPool(gameObject);
                break;
            case VfxType.PLAYERHIT:
                GameManager.Instance.playerHitVfxPoolSystem.PutObjectIntoPool(gameObject);
                break;
        }
    }
}
