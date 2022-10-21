public static class PlayerProgressRegistry
{
    private static int _bulletsFired;
    private static int _currentLevel;
    private static int _compositionAsteroidsDestroyed;
    private static int _singleAsteroidsDestroyed;
    private static int _lifesLosted;
    private static int _currentScore;
    private static int _shipWraps;
    private static int _shipHitCompositionAsteoids;
    private static int _shipHitSimpleAsteoids;
    private static int _bulletWraps;
    private static int _bulletWrapsWithHit;

    public static bool reachedNewHighScore = false;
    
    public static int bulletsFired
    {
        get { return _bulletsFired;}
        set { _bulletsFired = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int currentLevel
    {
        get { return _currentLevel;}
        set
        {
            _currentLevel = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();
            if (SaveGameManager.informationsToSave.maxLevelReached < _currentLevel)
            {
                SaveGameManager.informationsToSave.maxLevelReached = currentLevel;
                SaveGameManager.SaveGame();
            }
        }
    }
    public static int compositionAsteroidsDestroyed
    {
        get { return _compositionAsteroidsDestroyed;}
        set { _compositionAsteroidsDestroyed = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int singleAsteroidsDestroyed
    {
        get { return _singleAsteroidsDestroyed;}
        set { _singleAsteroidsDestroyed = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int lifesLosted
    {
        get { return _lifesLosted;}
        set { _lifesLosted = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int currentScore
    {
        get { return _currentScore;}
        set {
            _currentScore = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();
            if (SaveGameManager.informationsToSave.highScore < _currentScore)
            {
                SaveGameManager.informationsToSave.highScore = _currentScore;
                if (!reachedNewHighScore)
                {
                    if (value - 1 > 0)
                        AchievementChecker.Instance.ForceDisplay(1); // 1 == high score achievement configured on achievements folder
                    reachedNewHighScore = true;
                }
                SaveGameManager.SaveGame();
            }}
    }
    public static int shipWraps
    {
        get { return _shipWraps;}
        set { _shipWraps = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int shipHitCompositionAsteroids
    {
        get { return _shipHitCompositionAsteoids;}
        set { _shipHitCompositionAsteoids = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int shipHitSingleAsteroids
    {
        get { return _shipHitSimpleAsteoids;}
        set { _shipHitSimpleAsteoids = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int bulletWraps
    {
        get { return _bulletWraps;}
        set { _bulletWraps = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int bulletWrapsWithHit
    {
        get { return _bulletWrapsWithHit;}
        set { _bulletWrapsWithHit = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }

    public static void ResetProgress()
    {
        _bulletsFired = 0;
        _currentLevel = 0;
        _compositionAsteroidsDestroyed = 0;
        _singleAsteroidsDestroyed = 0;
        _lifesLosted = 0;
        _currentScore = 0;
        _shipWraps = 0;
        _shipHitCompositionAsteoids = 0;
        _shipHitSimpleAsteoids = 0;
        _bulletWraps = 0;
        _bulletWrapsWithHit = 0;

        reachedNewHighScore = false;
    }
}
