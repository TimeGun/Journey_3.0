using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneManagerScript))]
public class SceneManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var sceneManagerScript = target as SceneManagerScript;
 
        sceneManagerScript.LoadSpecificBundle = GUILayout.Toggle(sceneManagerScript.LoadSpecificBundle, "Load Specific Bundle");
     
        if(sceneManagerScript.LoadSpecificBundle)
            sceneManagerScript.sectionToLoad = EditorGUILayout.IntSlider("Section to Load:", sceneManagerScript.sectionToLoad , 0 , sceneManagerScript.gameScenes.Length - 1);
    }
}
