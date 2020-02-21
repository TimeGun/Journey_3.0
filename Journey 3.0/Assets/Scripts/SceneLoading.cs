using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public SceneBundleSO sceneBundle;
    
    void Awake()
    {
        for (int i = 0; i < sceneBundle.scenes.Length; i++)
        {
            SceneManager.LoadSceneAsync(sceneBundle.scenes[i], LoadSceneMode.Additive);
        }
    }

    void Start()
    {
        
    }
}
