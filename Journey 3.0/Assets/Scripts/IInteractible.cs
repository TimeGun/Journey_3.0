using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickup
{
    void GetPickedUp(Transform parent);

    void GetDropped();
}
