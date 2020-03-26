using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TorchOnOff : MonoBehaviour
{
    public bool isLit;
    
    private PickUpObject _pickUpObject;
    [SerializeField] private GameObject visualEffect;
    [SerializeField] private GameObject pointLight;
 
    void Start()
    {
        _pickUpObject = GetComponent<PickUpObject>();
    }


    void Update()
    {
        isLit = _pickUpObject.Carried;
        visualEffect.SetActive(isLit);
        pointLight.SetActive(isLit);
    }
}
