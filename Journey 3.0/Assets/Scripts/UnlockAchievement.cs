using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class UnlockAchievement : MonoBehaviour
{
    public AchievementSO achievementToUnlock;

    [SerializeField] private VisualEffect shower;

    private bool triggered;

    public void UnlockAchievementSO()
    {
        if (!triggered)
        {
            if (achievementToUnlock != null)
                AchievementManager.instance.UnlockSteamAchievement(achievementToUnlock);
            
            shower.SendEvent("MeteorShowerSpawn");
            triggered = true;
        }
    }
}