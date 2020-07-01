using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyFollower : MonoBehaviour
{
    void Update()
    {
        transform.position = PlayerManager.FireflyRef.position;
    }
}