using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AchievementChecker : SingletonMonoBehaviour<AchievementChecker>
{
    private Queue<AchievementBase> unlockedScheduled = new Queue<AchievementBase>();
    private Coroutine currentRoutine;

    private void Awake()
    {
        //TODO Remove this temporary force reset
        ResetAllAchievements();
    }
    
    private IEnumerator WaitForPopup()
    {
        yield return new WaitUntil(() => !PopupManager.popupRunning);
        var unlocked = GetNextUnlockedScheduled();
        if (unlocked)
        {
            PopupManager.CallPopup(PopupIdentifier.UNLOCK_ACHIEVEMENT).SetInformations(unlocked);
        }
        else
        {
            currentRoutine = null;
        }

        yield return new WaitForEndOfFrame();
        yield return StartCoroutine(WaitForPopup());
    }

    private AchievementBase GetNextUnlockedScheduled() => unlockedScheduled.Count > 0 ? unlockedScheduled.Dequeue() : null;

    public void CheckForAchievementsUnlocked()
    {
        foreach (var achievement in GameManager.Instance.GameSettings.Achievements)
        {
            if (!achievement.completed && achievement.CheckResult())
            {
                ScheduleUnlockPopup(achievement);
            }
        }
    }

    private void ScheduleUnlockPopup(AchievementBase achievement)
    {
        unlockedScheduled.Enqueue(achievement);
        if (currentRoutine == null)
            currentRoutine = StartCoroutine(WaitForPopup());
    }

    private void ResetAllAchievements()
    {
        foreach (var achievement in GameManager.Instance.GameSettings.Achievements)
        {
            achievement.completed = false;
        }
    }
}
