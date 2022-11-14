using System;
using UnityEngine;


[CreateAssetMenu]
public sealed class ShipAchievement : AchievementBase
{
    [Serializable]
    public class ShipPossibility
    {
        public int wraps = 0;
        public int hitCompositionAsteroids = 0;
        public int hitSingleAsteroids = 0;
        public SimpleAchievement[] simpleAchievements;
    }

    public ShipPossibility[] possibilitys;

    public override bool CheckResult()
    {
        foreach (var possibility in possibilitys)
        {
            if (PlayerProgressRegistry.shipWraps >= possibility.wraps
                && PlayerProgressRegistry.shipHitCompositionAsteroids >= possibility.hitCompositionAsteroids
                && PlayerProgressRegistry.shipHitSingleAsteroids >= possibility.hitSingleAsteroids)
            {
                if (possibility.simpleAchievements.Length > 0)
                {
                    foreach (var simple in possibility.simpleAchievements)
                    {
                        if (!simple.CheckResult()) return false;
                    }
                }
                completed = true;
                return true;
            }
        }
        return false;
    }
}