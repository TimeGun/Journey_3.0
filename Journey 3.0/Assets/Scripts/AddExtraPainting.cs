using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Section 10, allows more than one painting to be unlocked in the Gallery for the broken painting
/// </summary>
public class AddExtraPainting : MonoBehaviour
{
    [SerializeField] private InteractiblePainting _painting;

    [SerializeField] private int extraPaintingIndex; //which painting index to bonus unlock
    
    
    
    private void OnEnable()
    {
        _painting.OnStart += UnlockPainting;
    }



    private void OnDisable()
    {
        _painting.OnStart -= UnlockPainting;
    }
    


    public void UnlockPainting()
    {
        GallerySaveSystem.FoundPainting(extraPaintingIndex);
    }
}
