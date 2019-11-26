using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FovManipulator : MonoBehaviour
{
    public GameObject TargetObj;
    public GameObject Player;
    public GameObject Trigger;
    public GameObject activeCamera;
    private bool playerEnter;
    private bool playerExit;
    private bool playerInZone;
    private float startFOV;
    private float currentFov;
    public float targetFov;
    
    
    

    private float _startDist; //Get distance to targetobj when player enters collider

    private float currentDist;    // current dist from target obj
   
    void Start()
    {
        
        
        startFOV = activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;    //get initial fov of target camera
    }

  
    void Update()
    {
        playerEnter = Trigger.GetComponent<DetectPlayer>().PlayerEntered;        //bool set true first frame player enters the  trigger collider. Set in detect player script
        playerInZone = Trigger.GetComponent<DetectPlayer>().PlayerInCollider;        //bool set true when the player is in the trigger collider

       
        
        currentDist = Vector3.Distance(Player.transform.position, TargetObj.transform.position);        //current distance between p[layer and target obj
        
        if (playerEnter)
        {
            _startDist = Vector3.Distance(Player.transform.position, TargetObj.transform.position);        //get start dist when player enters the collider
        }
        else if (playerInZone)
        {
            currentFov = Map(currentDist, _startDist, 0, startFOV, targetFov);     //Map Function
            //Debug.Log(_startDist);
            //Debug.Log(currentFov);
            activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = currentFov;        //set active camera fov to currentfov value
        }
        else if (playerInZone == false)
        {
            activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = startFOV;        //sets fov back to initial fov when the player exits the collider
        }
     }
    
    public float DistCovered()
    {
        if (playerEnter)
        {
            Debug.LogError("Player is not in zone");
        }

        float tempDist;
        tempDist = _startDist - Vector3.Distance(Player.transform.position, TargetObj.transform.position);
        return tempDist;
    }
    
    public float Map(float a, float b, float c, float d, float e)
    {
        
        float cb = c - b;
        float de = e - d;
        float howFar = (a - b) / cb;
        return d + howFar * de;
        
        //float a = value you want mapped t
        
        
    }
}
