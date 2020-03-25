using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private string[] scenesToUnload;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            API.SceneManagerScript.StartCoroutine(SceneManagerScript.LoadBundle(false));

            if (scenesToUnload.Length > 0)
            {
                API.SceneManagerScript.StartCoroutine(SceneManagerScript.UnloadScenes(scenesToUnload));
            }

            Destroy(gameObject);
        }
    }
}
