using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAchievement : MonoBehaviour
{
    public AchievementSO achievementToUnlock;

    public void UnlockAchievementSO()
    {
        if (achievementToUnlock != null)
            AchievementManager.instance.UnlockSteamAchievement(achievementToUnlock);
    }
}