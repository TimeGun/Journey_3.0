using System;
using UnityEngine;

public class HitGroundKinematic : MonoBehaviour
{
    [SerializeField] private Rigidbody [] _rbs;
    // Start is called before the first frame update
    void Start()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    public void EnableRigidbodies()
    {
        foreach (var rb in _rbs)
        {
            rb.isKinematic = false;
        }

        Invoke("DisableRigidbodies", 6f);
    }

    public void DisableRigidbodies()
    {
        foreach (var rb in _rbs)
        {
            rb.isKinematic = true;
        }
    }
}
