using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FirePositioning : MonoBehaviour
{
    [SerializeField]
    private VisualEffect visualEffect;

    [SerializeField]
    private Vector3 fireTargetPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        visualEffect.SetVector3("Fire Pos", fireTargetPos);
    }
}
