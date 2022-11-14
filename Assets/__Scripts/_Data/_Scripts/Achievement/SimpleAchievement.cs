using UnityEngine;

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