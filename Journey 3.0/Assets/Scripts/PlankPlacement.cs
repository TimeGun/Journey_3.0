using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankPlacement : MonoBehaviour
{
    [SerializeField] private GameObject _centerOfGapObj;

    [SerializeField] private PositionAdjustment _positionAdjustment;

    [SerializeField] private GameObject _plank;
    

    public GameObject Plank
    {
        get => _plank;
        set => _plank = value;
    }


    public GameObject CenterOfGapObj
    {
        get => _centerOfGapObj;
        set => _centerOfGapObj = value;
    }

    public bool PlankIsPlaceDown
    {
        get => _plankIsPlacedDown;
        set => _plankIsPlacedDown = value;
    }

    private bool _plankIsPlacedDown;

    public void UpdatePositionAdjustBool()
    {
        _positionAdjustment.PlankPlacedDown = _plankIsPlacedDown;
    }
}
