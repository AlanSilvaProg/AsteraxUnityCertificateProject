using System;
using UnityEngine;

[Serializable]
public abstract class AchievementBase : ScriptableObject
{
    public string name;
    public string description;
    public int forceDisplayId;
    
    [HideInInspector] public bool completed = false;

    public abstract bool CheckResult();
}

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

[CreateAssetMenu]
public sealed class SimpleAchievement : AchievementBase
{
    public int levelRequired = 0;
    public int shotsFired = 0;
    public int score = 0;

    public override bool CheckResult()
    {
        completed = PlayerProgressRegistry.currentLevel >= levelRequired
                    && PlayerProgressRegistry.bulletsFired >= shotsFired
                    && PlayerProgressRegistry.currentScore >= score;
        return completed;
    }
}
