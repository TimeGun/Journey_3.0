using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRotate : MonoBehaviour
{
    //I'm trying a thing, stop judging me Code Boys - Robyn 
    public Vector3 speed;

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime);
    }
}
