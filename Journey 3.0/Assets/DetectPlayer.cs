using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    
    private bool playerDetected;
    private bool playerEntered;
    private bool playerExited;
    private bool firstDetection;
    private int testInt = 0;

    public bool PlayerDetected
    {
        get { return playerDetected; }
    }

    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerDetected);
        Debug.Log(testInt);
        //print(PlayerDetected);
        NewZone();
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerEntered = true;
            //playerDetected =! playerDetected;
            testInt++;

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerExited = true;
        }
    }

    void NewZone()
    {
        if (playerEntered && playerExited)
        {
            playerDetected = !playerDetected;
            playerEntered = false;
            playerExited = false;
        }
    }
}
