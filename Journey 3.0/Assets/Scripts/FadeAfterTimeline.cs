using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FadeAfterTimeline : MonoBehaviour
{
    
    public PlayableDirector playableDirector;
    public GameObject cameraTrigger;
    private bool _playerEntered;
    private double _timelineLength;
    // Start is called before the first frame update
    void Start()
    {
        _timelineLength = playableDirector.duration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cameraTrigger != null)
        {
            _playerEntered = cameraTrigger.GetComponent<DetectPlayer>().PlayerEntered;    //Use a coroutine to check if the player has entered every x amount of frames

        }

        if (_playerEntered)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(((float)_timelineLength) - 1f);
        Debug.Log("FADE OUT");
        global::FadeToBlack.instance.SetBlack(true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Manager Scene");
    }
}
