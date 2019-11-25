using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCheck : MonoBehaviour
{
    public bool _grounded = false;

    private Collider _col;

    private RaycastHit _hit;

    [SerializeField] private LayerMask _mask;

    private Rigidbody _rb;


    void Start()
    {
        _col = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, out _hit, _col.bounds.extents.y + 0.1f, _mask))
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }

        Debug.Log("eoin is great!");

        if(GetComponent<PushObject>() == null)
        _rb.isKinematic = _grounded;
    }
}
