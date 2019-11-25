using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSphere : MonoBehaviour
{
    public GameObject Player;
    public Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        if (_offset == Vector3.zero)
        {
            _offset = transform.position - Player.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + _offset;
    }
}
