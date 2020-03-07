using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PlankPlacement : MonoBehaviour, IPlaceableArea
{
    [SerializeField] private GameObject _centerOfGapObj;

    [SerializeField] private PositionAdjustment _positionAdjustment;

    [SerializeField] private GameObject _plank;
    

    private bool _plankIsPlacedDown;


    public GameObject GetCenterObject()
    {
        return _centerOfGapObj;
    }

    public GameObject GetPlank()
    {
        return _plank;
    }

    public void SetPlank(GameObject value)
    {
        _plank = value; 
    }

    public bool GetPlankPlacedDown()
    {
        return _plankIsPlacedDown;
    }

    public void SetPlankPlacedDown(bool value)
    {
        _plankIsPlacedDown = value;
    }

    public bool AdjustPositionBool()
    {
        return true;
    }
}
