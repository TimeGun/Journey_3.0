using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetOptions : MonoBehaviour
{
    [SerializeField] [Range(0, 2f)] private float _offsetMultiplier;

    public float OffsetMultiplier
    {
        get => _offsetMultiplier;
    }
}
