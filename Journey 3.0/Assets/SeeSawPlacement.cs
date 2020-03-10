using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawPlacement : MonoBehaviour, IPlaceableArea
{
    [SerializeField] private GameObject _boulder;

    [SerializeField] private GameObject _boulderPlacement;

    [SerializeField] private bool _boulderPlacedDown = false;

    [SerializeField] private bool playerInThisTrigger = false;

    public bool PlayerInThisTrigger
    {
        get => playerInThisTrigger;
        set => playerInThisTrigger = value;
    }

    public GameObject GetCenterObject()
    {
        return _boulderPlacement;
    }

    public GameObject GetPlank()
    {
        return _boulder;
    }

    public void SetPlank(GameObject value)
    {
        _boulder = value;
    }

    public bool GetPlankPlacedDown()
    {
        return _boulderPlacedDown;
    }

    public void SetPlankPlacedDown(bool value)
    {
        _boulderPlacedDown = value;
    }

    public bool AdjustPositionBool()
    {
        return false;
    }
}
