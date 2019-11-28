using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowObject : MonoBehaviour, IInteractible, IRune
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isActive()
    {
        return true;
    }

    public void StartInteraction(Transform parent)
    {
        
    }

    public void StopInteraction()
    {
        
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }
}
