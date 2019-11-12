using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    
    private bool playerDetected;
    private bool playerEntered;
    private bool playerExited;
    private bool playerInCollider;
    private bool firstDetection;
    private int testInt = 0;

    public bool PlayerDetected
    {
        get { return playerDetected; }
    }

    public bool PlayerEntered
    {
        get { return playerEntered; }
    }

    public bool PlayerInCollider
    {
        get { return playerInCollider; }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
//        Debug.Log(playerDetected);
//        Debug.Log(testInt);
        //print(PlayerDetected);
        NewZone();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerEntered = true;                       
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {            
            playerExited = true;
            playerInCollider = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInCollider = true;
           Debug.Log("Player in " + gameObject);
        }
    }

    void NewZone()
    {
        if (playerEntered && playerExited)
        {
            //playerDetected = !playerDetected;
//            Debug.Log(gameObject + "" + playerDetected);
            playerEntered = false;
            playerExited = false;
        }
    }
}
