using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AchievementManager))]
public class AchievementManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AchievementManager achievementManager = (AchievementManager)target;
        
        GUILayout.Label("Test Unlocking and Locking Achievements");
        
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Unlock Test Achievement"))
        {
            achievementManager.UnlockSteamAchievement(achievementManager.DebugTestingAchievementSo);
        }
        
        if (GUILayout.Button("Lock Test Achievement"))
        {
            achievementManager.DEBUG_LockSteamAchievement(achievementManager.DebugTestingAchievementSo);
        }
        
        GUILayout.EndHorizontal();
    }
}
