using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RestartTimeline : MonoBehaviour
{
    private PlayableDirector _timeline;
    // Start is called before the first frame update
    void Start()
    {
        _timeline = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _timeline.Stop();
            _timeline.Play();
        }
    }
}
