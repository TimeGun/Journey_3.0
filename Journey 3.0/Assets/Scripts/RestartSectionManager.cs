using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSectionManager : MonoBehaviour
{
    /// <summary>
    /// Clean Loads the last saved progression point
    /// will take some time
    /// </summary>
    public static void RestartSection()
    {
        ProgressionData progressionData = SaveSystem.LoadProgressionData();
        
        if (progressionData == null) //no save data exist. Load start screen in section 0
        {
            SaveSystem.firstTimePlaying = true;
            ProgressionData _newProgressionData = new ProgressionData(0, false); //create the progression data
            
            SaveSystem.SaveProgress(_newProgressionData); //save the initial progression file
            
            SceneManagerScript.instance.StartCoroutine(SceneManagerScript.instance.StartGameLoad(_newProgressionData, true)); //StartLoad of base scene and then sections 0 and 1
        }
        else //save data exist. Load the last savepoint menu
        {
            SaveSystem.firstTimePlaying = false; //not the players first time playing
                
            SceneManagerScript.instance.StartCoroutine(SceneManagerScript.instance.StartGameLoad(progressionData, true));
        }
    }
}
