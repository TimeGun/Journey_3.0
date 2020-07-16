using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAchievement : MonoBehaviour
{
    [SerializeField] private int maxFireflies;

    [SerializeField] private AchievementSO fireflyAchievementSo;
    public static FireflyAchievement instance;

    private int flyTakeoffCounter = 0;

    void Awake()
    {
        instance = this;
    }


    public void CheckAchievement()
    {
        flyTakeoffCounter++;

        if (flyTakeoffCounter == maxFireflies)
        {
            AchievementManager.instance.UnlockSteamAchievement(fireflyAchievementSo);
        }
    }

    public static void AddFirefly()
    {
        instance.CheckAchievement();
    }
}