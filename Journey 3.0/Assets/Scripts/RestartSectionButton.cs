using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartSectionButton : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    
    public void RestartSection()
    {
        StartCoroutine("StartRestart");
    }

    IEnumerator StartRestart()
    {
        UI.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;
        
        FadeToBlack.instance.SetBlack(true);
        AmbienceManager.FadeOutMasterSound();
        yield return new WaitForSeconds(1f);
        
        RestartSectionManager.RestartSection();
    }
}
