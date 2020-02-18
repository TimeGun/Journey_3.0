using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCameraRef : MonoBehaviour
{
    void Start()
    {
        API.GlobalReferences.MainCamera = gameObject;
    }
}
