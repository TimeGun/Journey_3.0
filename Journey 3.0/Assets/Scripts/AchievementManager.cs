using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class AchievementManager : MonoBehaviour
{

    public AchievementSO DebugTestingAchievementSo;
    
    public static AchievementManager instance;
    
    private bool unlockTest;

    private void Awake()
    {
        instance = this;
    }

    public void UnlockSteamAchievement(AchievementSO achievement)
    {
        if (SteamManager.Initialized)
        {
            TestSteamAchievement(achievement);

            if (!unlockTest)
            {
                print("Unlocked: " + achievement.Name);
                SteamUserStats.SetAchievement(achievement.AchievementID);
                SteamUserStats.StoreStats();
            }
        }
    }
    
    public void DEBUG_LockSteamAchievement(AchievementSO achievement)
    {
        if (SteamManager.Initialized)
        {
            TestSteamAchievement(achievement);

            if (unlockTest)
            {
                print("Locked: " + achievement.Name);
                SteamUserStats.ClearAchievement(achievement.AchievementID);
                SteamUserStats.StoreStats();
            }
        }
    }

    public void DEBUG_LockAllAchievements()
    {
        SteamUserStats.ResetAllStats(true);
    }

    public void TestSteamAchievement(AchievementSO achievement)
    {
        SteamUserStats.GetAchievement(achievement.AchievementID, out unlockTest);
    }
}
