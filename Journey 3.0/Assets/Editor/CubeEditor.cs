using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColourChanger))]
public class CubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColourChanger colourChanger = (ColourChanger)target;
        
        GUILayout.Label("Change the Colour of the Cube and Reset it");
        GUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Generate Colour"))
        {
            colourChanger.ChangeColour();
        }
        
        if (GUILayout.Button("Reset Colour"))
        {
            colourChanger.ResetColour();
        }
        
        GUILayout.EndHorizontal();
        
        
    }
}
