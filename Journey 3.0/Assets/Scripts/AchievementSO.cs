using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
public class AchievementSO : ScriptableObject
{
    public string Name;
    public string Description;
    public string AchievementID;
}
