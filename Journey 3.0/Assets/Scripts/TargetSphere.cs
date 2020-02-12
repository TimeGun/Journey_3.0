using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : MonoBehaviour
{
    public GameObject _player;
    public Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        if (_offset == Vector3.zero)
        {
            _offset = transform.position - _player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _player.transform.position + _offset;
        
    }
}
