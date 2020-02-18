using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayerRef : MonoBehaviour
{
    void Start()
    {
        API.GlobalReferences.PlayerRef = gameObject;
    }
}
