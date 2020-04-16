using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    private ProgressionData _progressionData;

    public bool debug;

    [Range(0, 3)]public int debugInt;
    public bool debugBool;

    private void Start()
    {
        if (!debug)
        {
            _progressionData = SaveSystem.LoadProgressionData(); //try and load existing save data

            if (_progressionData == null) //no save data exist. Load start screen in section 0
            {
                SaveSystem.firstTimePlaying = true;
                ProgressionData _newProgressionData = new ProgressionData(0, false); //create the progression data
            
                SaveSystem.SaveProgress(_newProgressionData); //save the initial progression file
            
                SceneManagerScript.instance.StartCoroutine(SceneManagerScript.instance.StartGameLoad(_newProgressionData)); //StartLoad of base scene and then sections 0 and 1
            }
            else //save data exist. Load the last savepoint menu
            {
                SaveSystem.firstTimePlaying = false; //not the players first time playing
                
                SceneManagerScript.instance.StartCoroutine(SceneManagerScript.instance.StartGameLoad(_progressionData));
            }
        }
        else // debug all possible versions of the save file
        {
            ProgressionData _debugProgressionData = new ProgressionData(debugInt, debugBool); 
            
            SceneManagerScript.instance.StartCoroutine(SceneManagerScript.instance.StartGameLoad(_debugProgressionData));
        }
    }
}
