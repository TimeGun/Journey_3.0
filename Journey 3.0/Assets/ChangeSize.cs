using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    public bool changeMode;
    
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

            if (changeMode)
            {
                Destroy(GetComponent<PickUpObject>());
                PushObject pusher = gameObject.AddComponent(typeof(PushObject)) as PushObject;
            }
        }
        else
        {
            transform.localScale = smallScale;
            _small = true;
            
            if (changeMode)
            {
                Destroy(GetComponent<PushObject>());
                PickUpObject picker = gameObject.AddComponent(typeof(PickUpObject)) as PickUpObject;
            }
        }
    }
    
}
