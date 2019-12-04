using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "New Scene Bundle", menuName = "SceneBundle")]
public class SceneBundleSO : ScriptableObject
{
    public string [] scenes;
}
