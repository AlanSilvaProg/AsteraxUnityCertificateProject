using System;
using UnityEngine;

[Serializable]
public class PartUnlockCondition
{
    public PartType partType;
    public GameObject part;
    [SerializeField] private AchievementBase achievementNeeded;
    public bool unlocked => achievementNeeded.completed;
}
