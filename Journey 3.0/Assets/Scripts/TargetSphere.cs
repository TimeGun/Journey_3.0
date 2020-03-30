using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : MonoBehaviour
{
    public GameObject _player;
    public Vector3 _offset;

    private bool changeOffset;
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
        /*if (SceneManagerScript.bundleIndex == 3 && changeOffset == false)
        {
            _offset.z = _offset.z * -1;
            changeOffset = true;
        }*/
        transform.position = _player.transform.position + _offset;
        
    }
}
