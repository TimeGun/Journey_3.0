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
    public float minDist;
    public float maxDist;
    private float _startFOV;
    private float _currentFov;
    public float targetFov;
    

    public enum DistanceType
    {
        CameraToPlayer,
        ObjectToPlayer
    }

    public DistanceType _distanceType;
    

    private float _startDist; //Get distance to targetobj when player enters collider
    

    private float currentDist;    // current dist from target obj
   
    void Start()
    {
        _startFOV = activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;    //get initial fov of target camera
    }

  
    void Update()
    {
        if (_distanceType == DistanceType.CameraToPlayer)
        {
            CameraToPlayerDist();
        }
        else if (_distanceType == DistanceType.ObjectToPlayer)
        {
           ObjectToPlayerDist();
        }
        /*playerEnter = Trigger.GetComponent<DetectPlayer>().PlayerEntered;        //bool set true first frame player enters the  trigger collider. Set in detect player script
        playerInZone = Trigger.GetComponent<DetectPlayer>().PlayerInCollider;        //bool set true when the player is in the trigger collider 
        currentDist = Vector3.Distance(Player.transform.position, TargetObj.transform.position);        //current distance between p[layer and target obj
        
        if (playerEnter)
        {
            _startDist = Vector3.Distance(Player.transform.position, TargetObj.transform.position);        //get start dist when player enters the collider
        }
        else if (playerInZone)
        {
            _currentFov = Map(currentDist, _startDist, 0, _startFOV, targetFov);     //Map Function
            //Debug.Log(_startDist);
            //Debug.Log(currentFov);
            activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = _currentFov;        //set active camera fov to currentfov value
        }
        else if (playerInZone == false)
        {
            activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = _startFOV;        //sets fov back to initial fov when the player exits the collider
        }*/
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

    void ObjectToPlayerDist()
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
                _currentFov = Map(currentDist, _startDist, 0, _startFOV, targetFov);     //Map Function
                //Debug.Log(_startDist);
                //Debug.Log(currentFov);
                activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = _currentFov;        //set active camera fov to currentfov value
            }
            else if (playerInZone == false)
            {
                activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = _startFOV;        //sets fov back to initial fov when the player exits the collider
            }
    }

    void CameraToPlayerDist()
    {
        float clampedFov;
        if (maxDist == 0)
        {
            Debug.LogError("Max distance is not set. Fov will return infinity");
        }

        currentDist = Vector3.Distance(Player.transform.position, TargetObj.transform.position);
        //Mathf.Clamp(currentDist, minDist, maxDist);
        //Debug.Log("Start Fov: " + _startFOV);
        //Debug.Log("Target Fov: " + targetFov);

        _currentFov = Map(currentDist, minDist, maxDist, _startFOV, targetFov);
        if (_startFOV > targetFov)     // if you want to widen the fov as the player approaches the camera
        {
            clampedFov = Mathf.Clamp(_currentFov, targetFov, _startFOV);
            Debug.Log(_currentFov);
        }
        else            //if you want to narrow fov as the player approaches the camera
        {
            clampedFov = Mathf.Clamp(_currentFov, _startFOV, targetFov);
        }
        activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = clampedFov;       
    }
}
