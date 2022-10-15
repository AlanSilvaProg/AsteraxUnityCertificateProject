using System;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSettings : ScriptableObject
{
    [Tooltip("Max Level that will be multiplie with the max of asteroids spawning")]
    [SerializeField] private int maxLevelMultiplier = 10;

    [Tooltip("All achievements to player unlock during gameplay")]
    [SerializeField] private AchievementBase[] _achievements;

    public int MaxLevelMultiplier => maxLevelMultiplier;
    public AchievementBase[] Achievements => _achievements;
}