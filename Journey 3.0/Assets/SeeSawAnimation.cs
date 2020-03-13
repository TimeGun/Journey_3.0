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
    [SerializeField] private GameObject launchDirection;

    [SerializeField] private float launchForce = 50f;

    private void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("Launch"))
        {
            ReleaseRock(true);
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("CheckLaunch"))
        {
            ReleaseRock(false);
        }
    }


    public void SetAnimationBoolS(string boolName, bool value)
    {
        _anim.SetBool(boolName, value);
    }


    public void LaunchRock()
    {
        _seeSawPlacement.GetPlank().GetComponent<MeshFilter>().mesh = mesh;
        _seeSawPlacement.GetPlank().GetComponent<Renderer>().material = material;

        _seeSawPlacement.GetPlank().GetComponent<Rigidbody>().AddForce(launchDirection.transform.forward * launchForce, ForceMode.Impulse);
    }


    public void ReleaseRock(bool launchRock)
    {
        if (_seeSawPlacement.GetPlank() != null)
        {
            _seeSawPlacement.GetPlank().GetComponent<Rigidbody>().isKinematic = false;
            _seeSawPlacement.GetPlank().GetComponent<PickUpObject>().PlacedDown = false;
            _seeSawPlacement.GetPlank().GetComponent<Collider>().isTrigger = false;
            
            if (launchRock)
                LaunchRock();

            _seeSawPlacement.SetPlank(null);
            _seeSawPlacement.SetPlankPlacedDown(false);
        }
    }
}