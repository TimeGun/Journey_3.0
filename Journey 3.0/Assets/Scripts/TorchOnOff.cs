using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TorchOnOff : MonoBehaviour
{
    public bool isLit;
    
    private PickUpObject _pickUpObject;
    [SerializeField] private VisualEffect visualEffect;
    // Start is called before the first frame update
    void Start()
    {
        _pickUpObject = GetComponent<PickUpObject>();
    }

    // Update is called once per frame
    void Update()
    {
        isLit = _pickUpObject.Carried;
        visualEffect.enabled = isLit;
    }
}
