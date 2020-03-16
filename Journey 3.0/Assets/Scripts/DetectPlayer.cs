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

    public bool PlayerExited
    {
        get { return _playerExited; }
        
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


    private IEnumerator OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && col.isTrigger == false)
        {
            _playerEntered = true;
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            _playerEntered = false;
//            StartCoroutine(MakePlayerEnterFalse());   
//            Debug.Log(_playerEntered);
            //playerEntered = false;

        }
    }

    private IEnumerator OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player")&& col.isTrigger == false)
        {            
            _playerExited = true;
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            _playerExited = false;
//            Debug.Log("Exited");
            playerInCollider = false;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player")&& col.isTrigger == false)
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
            Debug.Log("in coroutine");
            yield return new WaitForSeconds(Time.deltaTime);
            //yield return new WaitForEndOfFrame();
            _playerEntered = false;
        }
    }
    
}
