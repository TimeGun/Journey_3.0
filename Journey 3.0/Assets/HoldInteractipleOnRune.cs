using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInteractipleOnRune : MonoBehaviour, IInteractible, IRune
{
    private bool _itemOnRune;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject IInteractible.getGameObject()
    {
        throw new System.NotImplementedException();
    }

    public bool isActive()
    {
        throw new System.NotImplementedException();
    }

    public void StartInteraction(Transform parent)
    {
        throw new System.NotImplementedException();
    }

    public void StopInteraction()
    {
        throw new System.NotImplementedException();
    }

    GameObject IRune.getGameObject()
    {
        throw new System.NotImplementedException();
    }
}
