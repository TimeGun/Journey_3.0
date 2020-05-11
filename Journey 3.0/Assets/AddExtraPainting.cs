using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddExtraPainting : MonoBehaviour
{
    [SerializeField] private InteractiblePainting _painting;

    [SerializeField] private int extraPaintingIndex;

    private void OnEnable()
    {
        _painting.OnStart += UnlockPainting;
    }



    private void OnDisable()
    {
        
    }
    


    public void UnlockPainting()
    {
        GallerySaveSystem.FoundPainting(extraPaintingIndex);
    }
}
