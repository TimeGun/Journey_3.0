using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    private Vector3 smallScale;

    private Vector3 largeScale;

    private bool _small;

    public bool startSmall;

    private void Start()
    {
        if (startSmall)
        {
            smallScale = transform.localScale;
            largeScale = transform.localScale * 2;
            _small = true;
        }
        else
        {
            smallScale = transform.localScale / 2f;
            largeScale = transform.localScale;
            _small = false;
        }


    }

    public void ChangeSizeOfObject()
    {
        if (_small)
        {
            transform.localScale = largeScale;
            _small = false;
        }
        else
        {
            transform.localScale = smallScale;
            _small = true;
        }
    }
}
