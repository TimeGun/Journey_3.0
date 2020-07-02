using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class AchievementManager : MonoBehaviour
{

    public AchievementSO DebugTestingAchievementSo;
    
    public static AchievementManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void UnlockSteamAchievement(AchievementSO achievement)
    {
        bool unlocked;
        
        TestSteamAchievement(achievement, out unlocked);

        if (!unlocked)
        {
            SteamUserStats.SetAchievement(achievement.AchievementID);
            SteamUserStats.StoreStats();
        }
    }
    
    public void DEBUG_LockSteamAchievement(AchievementSO achievement)
    {
        bool unlocked;
        
        TestSteamAchievement(achievement, out unlocked);

        if (unlocked)
        {
            SteamUserStats.ClearAchievement(achievement.AchievementID);
            SteamUserStats.StoreStats();
        }
    }

    public void TestSteamAchievement(AchievementSO achievement, out bool unlockTest)
    {
        SteamUserStats.GetAchievement(achievement.AchievementID, out unlockTest);
    }
}
