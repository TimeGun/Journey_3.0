using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeSawAnimation : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    [SerializeField] private SeeSawPlacement _seeSawPlacement;

    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;


    public void SetAnimationBoolS(string boolName, bool value)
    {
        _anim.SetBool(boolName, value);
    }


    public void LaunchRock()
    {
        if (_seeSawPlacement.GetPlank() != null)
        {
            _seeSawPlacement.GetPlank().GetComponent<MeshFilter>().mesh = mesh;
            _seeSawPlacement.GetPlank().GetComponent<Renderer>().material = material;
        }
    }
}
