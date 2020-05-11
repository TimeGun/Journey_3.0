
using System;
using UnityEngine;


public class AssignPlayerProperty : MonoBehaviour
{
    private Transform _player;
    private void Start()
    {
        _player = API.GlobalReferences.PlayerRef.transform;
    }

    private void Update()
    {
        transform.position = _player.position + new Vector3(0, 0.5f, 0);
    }
}
