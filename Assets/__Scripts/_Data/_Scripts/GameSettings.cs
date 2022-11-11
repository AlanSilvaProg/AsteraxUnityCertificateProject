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

    [Tooltip("The Customization information to get all data about the parts that you can equip on your ship")]
    [SerializeField] private CustomizationParts _customizationParts;

    public int MaxLevelMultiplier => maxLevelMultiplier;
    public AchievementBase[] Achievements => _achievements;
    public CustomizationParts CustomizationParts => _customizationParts;

}