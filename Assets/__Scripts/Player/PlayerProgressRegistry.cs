public static class PlayerProgressRegistry
{
    private static int _bulletsFired;
    private static int _currentLevel;
    private static int _compositionAsteroidsDestroyed;
    private static int _singleAsteroidsDestroyed;
    private static int _lifesLosted;
    private static int _shipWraps;
    private static int _bulletWraps;
    private static int _bulletWrapsWithHit;
    
    public static int bulletsFired
    {
        get { return _bulletsFired;}
        set { _bulletsFired = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
    }
    public static int currentLevel
    {
        get { return _currentLevel;}
        set { _currentLevel = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
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
    public static int shipWraps
    {
        get { return _shipWraps;}
        set { _shipWraps = value; AchievementChecker.Instance.CheckForAchievementsUnlocked();}
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
}
