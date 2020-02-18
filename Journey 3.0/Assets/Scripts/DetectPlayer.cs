using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    
    private bool _playerDetected;
    private bool _playerEntered;
    private bool _playerExited;
    private bool playerInCollider;
    private bool firstDetection;
    private int testInt = 0;

    private bool playerEnterBool;

    public bool PlayerDetected
    {
        get { return _playerDetected; }
    }

    public bool PlayerEntered
    {
        get { return _playerEntered; }
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
            _playerEntered = true;
            StartCoroutine(MakePlayerEnterFalse());
//            Debug.Log(_playerEntered);
            //playerEntered = false;

        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {            
            _playerExited = true;
            playerInCollider = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerInCollider = true;
           
        }
    }

    void NewZone()
    {
        if (_playerEntered && _playerExited)
        {
            //playerDetected = !playerDetected;
//            Debug.Log(gameObject + "" + playerDetected);
            _playerEntered = false;
            _playerExited = false;
        }
    }

    IEnumerator MakePlayerEnterFalse()
    {
        
        while (_playerEntered)
        {
            //Debug.Log("in coroutine");
            yield return new WaitForSeconds(Time.deltaTime);
            _playerEntered = false;
           
        }
    }
    
}
