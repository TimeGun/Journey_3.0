using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAchievement : MonoBehaviour
{
    public static LightningAchievement instance;

    [SerializeField] private AchievementSO lightningAchievementSo;

    private bool hitByLightning;

    public static void SetHit()
    {
        instance.hitByLightning = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            CheckAchievement();
        }
    }

    private void CheckAchievement()
    {
        if (!hitByLightning)
        {
            AchievementManager.instance.UnlockSteamAchievement(lightningAchievementSo);
            enabled = false;
        }
    }
}
