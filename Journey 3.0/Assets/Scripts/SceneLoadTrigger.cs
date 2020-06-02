using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private bool loadNextScene;
    
    [SerializeField] private string[] scenesToUnload;

    [SerializeField] private bool saveGame = false;
    [Range(0, 3)][SerializeField] private int saveSection;
    [SerializeField] private bool saveSectionNight;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            if (loadNextScene)
            {
                API.SceneManagerScript.StartCoroutine(SceneManagerScript.LoadBundle(false));
            }
            
            if (scenesToUnload.Length > 0)
            {
                API.SceneManagerScript.StartCoroutine(SceneManagerScript.UnloadScenes(scenesToUnload));
            }

            if (saveGame)
            {
                ProgressionData _overwriteData = new ProgressionData(saveSection, saveSectionNight);
                
                SaveSystem.SaveProgress(_overwriteData);
            }

            Destroy(gameObject);
        }
    }
}
