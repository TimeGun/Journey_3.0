using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ColourChanger : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;

    public void ChangeColour()
    {
        renderer.sharedMaterial.color = Random.ColorHSV();
    }

    public void ResetColour()
    {
        renderer.sharedMaterial.color = Color.white;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColour();
        }
    }
}
