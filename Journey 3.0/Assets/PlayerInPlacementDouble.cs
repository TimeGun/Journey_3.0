using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInPlacementDouble : MonoBehaviour
{
    private bool playerInSideTrigger;

    private bool playerInMiddleTrigger;
    


    public bool PlayerInSideTrigger
    {
        get => playerInSideTrigger;
        set => playerInSideTrigger = value;
    }

    public bool PlayerInMiddleTrigger
    {
        get => playerInMiddleTrigger;
        set => playerInMiddleTrigger = value;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
