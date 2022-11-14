using System;
using UnityEngine;

[CreateAssetMenu]
public sealed class AsteroidAchievement : AchievementBase
{
    [Serializable]
    public class AsteroidPossibility
    {
        public int wrapShots = 0;
        public int compositionAsteroids = 0;
        public int singleAsteroids = 0;
        public SimpleAchievement[] simpleAchievements;
    }

    public AsteroidPossibility[] possibilitys;

    public override bool CheckResult()
    {
        foreach (var possibility in possibilitys)
        {
            if (PlayerProgressRegistry.bulletWrapsWithHit >= possibility.wrapShots
                && PlayerProgressRegistry.compositionAsteroidsDestroyed >= possibility.compositionAsteroids
                && PlayerProgressRegistry.singleAsteroidsDestroyed >= possibility.singleAsteroids)
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
