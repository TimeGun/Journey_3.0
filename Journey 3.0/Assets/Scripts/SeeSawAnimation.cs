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
        if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("CheckLaunch"))
        {
            ReleaseRock();
        }
    }


    public void SetAnimationBoolS(string boolName, bool value)
    {
        _anim.SetBool(boolName, value);
    }


    public void LaunchRock()
    {
        if (_seeSawPlacement.GetPlank() != null)
        {
            _seeSawPlacement.GetPlank().GetComponent<Rigidbody>().isKinematic = false;
            _seeSawPlacement.GetPlank().GetComponent<PickUpObject>().PlacedDown = false;
            _seeSawPlacement.GetPlank().GetComponent<Collider>().isTrigger = false;
        
            _seeSawPlacement.GetPlank().GetComponent<Rigidbody>().AddForce(launchDirection.transform.forward * launchForce, ForceMode.Impulse);
        
            _seeSawPlacement.SetPlank(null);
            _seeSawPlacement.SetPlankPlacedDown(false);
        }
    }


    public void ReleaseRock()
    {
        if (_seeSawPlacement.GetPlank() != null)
        {
            _seeSawPlacement.GetPlank().GetComponent<Rigidbody>().isKinematic = false;
            _seeSawPlacement.GetPlank().GetComponent<PickUpObject>().PlacedDown = false;
            _seeSawPlacement.GetPlank().GetComponent<Collider>().isTrigger = false;
            

            _seeSawPlacement.SetPlank(null);
            _seeSawPlacement.SetPlankPlacedDown(false);
        }
    }
}