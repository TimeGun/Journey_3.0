using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyRef : MonoBehaviour
{
    private Vector3 _fireFlyRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _fireFlyRef = transform.position;
        PlayerManager.FireflyRef = _fireFlyRef;
    }
}
