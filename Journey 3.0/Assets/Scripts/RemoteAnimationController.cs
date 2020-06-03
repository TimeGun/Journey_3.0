using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteAnimationController : MonoBehaviour
{
    private GameObject _player;

    private PlayerMovement _playerMovement;
    
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    public void RemotePickUpLow()
    {
        _playerMovement.PickUpLow();
    }

    public void PickUpHigh()
    {
        _playerMovement.PickUpHigh();
    }

    public void StartFormation()
    {
        _playerMovement.Anim.Play("Glyf-Formation");
    }

    public void AnimationPause(bool pause)
    {
        if (pause)
        {
            _playerMovement.Anim.speed = 0;
        }
        else
        {
            _playerMovement.Anim.speed = 1;
        }
    }
}
