using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadVerticalSlice : MonoBehaviour
{
    [SerializeField] private SceneBundleSO sceneBundle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(EndVerticalSlice());
        }
    }

    IEnumerator EndVerticalSlice()
    {
        FadeToBlack.instance.SetBlack(true);
        
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
