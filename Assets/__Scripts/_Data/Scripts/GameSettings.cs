using System;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [Tooltip("Max Level that will be multiplie with the max of asteroids spawning")]
    [SerializeField] private int maxLevelMultiplier = 10;

    [Tooltip("All achievements to player unlock during gameplay")]
    [SerializeField] private AchievementBase[] _achievements;

    public int MaxLevelMultiplier => maxLevelMultiplier;
}

[Serializable]
public class AchievementBase : ScriptableObject
{
    public string name;
    public string description;
    
    [HideInInspector] public bool completed = false;

    public virtual bool CheckResult()
    {
        return false;
    }
}

[CreateAssetMenu]
public class AsteroidAchievement : AchievementBase
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
        return false;
    }
}

[CreateAssetMenu]
public class ShipAchievement : AchievementBase
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
        return false;
    }
}

[CreateAssetMenu]
public class SimpleAchievement : AchievementBase
{
    public int levelRequired = 0;
    public int shotsFired = 0;
    public int score = 0;

    public override bool CheckResult()
    {
        return false;
    }
}