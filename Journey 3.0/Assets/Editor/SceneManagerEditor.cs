using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneManagerScript))]
public class SceneManagerEditor : Editor
{

    int selectedSize = 1;
    string[] names = new string[] { "Firefly Field", "Cave Puzzle",
        "Altar Room 1", "Bridge Puzzle", "Sunset Corridor", "Altar Room 2",
        "Pillar Room Puzzle", "Gear Corridor", "Elevator", "Altar Room 3",
        "Final Paintings and Battle"};

    int[] indexNumbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var sceneManagerScript = target as SceneManagerScript;
 
        sceneManagerScript.LoadSpecificBundle = GUILayout.Toggle(sceneManagerScript.LoadSpecificBundle, "Load Specific Bundle");

        if (sceneManagerScript.LoadSpecificBundle)
            sceneManagerScript.sectionToLoad = EditorGUILayout.IntPopup(sceneManagerScript.sectionToLoad, names, indexNumbers);
                
                //"Section to Load:", sceneManagerScript.sectionToLoad , 0 , sceneManagerScript.gameScenes.Length - 1);
    }
}
