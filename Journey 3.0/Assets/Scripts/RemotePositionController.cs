using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class RemotePositionController : MonoBehaviour
{
    private GameObject _player;

    [SerializeField] private GameObject animatedPositionObject;
    
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    void Update()
    {
        
    }

    public void StartRemoteControl(GameObject objectToFollow)
    {
        _player.GetComponent<PlayerMovement>().StartRemoteControlledMovement(animatedPositionObject);
    }

    public void StopRemoteControl()
    {
        _player.GetComponent<PlayerMovement>().StopRemoteControlledMovement();
    }
}
