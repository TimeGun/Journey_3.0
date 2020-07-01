using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyRef : MonoBehaviour
{
    void Start()
    {
        PlayerManager.FireflyRef = transform;
    }

}
