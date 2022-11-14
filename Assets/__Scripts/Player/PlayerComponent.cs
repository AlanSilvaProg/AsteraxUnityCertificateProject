using System;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField] protected PlayerGeneral playerGeneral;

    protected virtual void OnValidate()
    {
        if (playerGeneral == null)
            playerGeneral = FindObjectOfType<PlayerGeneral>();
    }

    protected virtual void Awake()
    {
        if (playerGeneral == null)
            playerGeneral = FindObjectOfType<PlayerGeneral>();
    }

    /// <summary>
    /// Check if have any gameplay blocker or freezer
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanDoAction()
    {
        return playerGeneral.FreeToDoAction;
    }
}
