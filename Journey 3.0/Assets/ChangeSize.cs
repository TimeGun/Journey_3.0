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

    private Rigidbody _rb;

    private GravityCheck _gravityCheck;

    [SerializeField] float _growSpeed = 2f;


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

        _rb = GetComponent<Rigidbody>();
        _gravityCheck = GetComponent<GravityCheck>();
    }

    public IEnumerator ChangeSizeOfObject()
    {
        
        Vector3 targetScale;
        
        //_gravityCheck.enabled = false;

        if (_small)
        {
            targetScale = largeScale;

            if (changeMode)
            {
                Destroy(GetComponent<PickUpObject>());
                PushObject pusher = gameObject.AddComponent(typeof(PushObject)) as PushObject;
            }

            _small = false;
        }
        else
        {
            targetScale = smallScale;

            if (changeMode)
            {
                Destroy(GetComponent<PushObject>());
                PickUpObject picker = gameObject.AddComponent(typeof(PickUpObject)) as PickUpObject;
            }

            _small = true;
        }



        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime * _growSpeed);
            yield return new WaitForFixedUpdate();
        }

        //_gravityCheck.enabled = true;
    }
}