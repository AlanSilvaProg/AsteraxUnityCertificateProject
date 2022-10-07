using UnityEngine;

public class GameSettings : ScriptableObject
{
    [Tooltip("Max Level that will be multiplie with the max of asteroids spawning")]
    [SerializeField] private int maxLevelMultiplier = 10;

    public int MaxLevelMultiplier => maxLevelMultiplier;
}
