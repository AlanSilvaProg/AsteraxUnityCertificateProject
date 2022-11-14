using System;
using UnityEngine;

public abstract class AchievementBase : ScriptableObject
{
    public string name;
    public string description;
    public int forceDisplayId;
    
    public bool completed = false;

    public abstract bool CheckResult();
}