using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBounceRotation : MonoBehaviour
{
    private GameObject _player;
    void Start()
    {
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerAtThisHeight = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        transform.rotation = Quaternion.LookRotation(playerAtThisHeight - transform.position, Vector3.up);
    }
}
