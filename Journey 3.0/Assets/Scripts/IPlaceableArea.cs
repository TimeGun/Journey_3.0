using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaceableArea
{
    GameObject GetCenterObject();

    GameObject GetPlank();


    void SetPlank(GameObject value);

    bool GetPlankPlacedDown();
    
    void SetPlankPlacedDown(bool value);

    bool AdjustPositionBool();

    System.Type GetType();
}
