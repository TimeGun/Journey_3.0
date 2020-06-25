using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGlow : MonoBehaviour
{
    private ObjectDetection _objectDetection;
    

    void OnEnable()
    {
        _objectDetection = GetComponent<ObjectDetection>();
        //StartCoroutine(UpdateMaterialList());

       
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _objectDetection = null;
    }

    void Update()
    {
        SetGlow();
    }

    private void SetGlow()
    {
        for (int i = 0; i < _objectDetection.Items.Count; i++)
        {
            Material mat = _objectDetection.Items[i].GetComponentInChildren<Renderer>().material;
            
            if (mat != null && mat.HasProperty("_interactible"))
            {
                if (_objectDetection.Items[i].GetComponent<IInteractible>().isActive())
                {
                    mat.SetFloat("_interactible", 0);
                }
                else
                {
                    mat.SetFloat("_interactible", 1f);
                }
            }
        }
    }
}