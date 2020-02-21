using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    [SerializeField] private GameObject _teleportExitLocation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportToExit(other.transform.root.gameObject);
        }
    }

    private void TeleportToExit(GameObject objectToTeleport)
    {
        objectToTeleport.transform.position = _teleportExitLocation.transform.position;
        print("called");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f, 0.4f);
        Gizmos.DrawSphere(_teleportExitLocation.transform.position, 10f);
        
        Gizmos.color = new Color(1, 0, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position, 10f);
    }
}
