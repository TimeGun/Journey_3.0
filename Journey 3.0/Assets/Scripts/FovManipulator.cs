using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FovManipulator : MonoBehaviour
{
    //Target Obejct for player to object Fov Lerp
    public GameObject TargetObj;

    //Player
    public GameObject Player;

    //Detect player to check enter and stay
    private DetectPlayer _detectPlayer;


    //The camera to manipulate
    public GameObject activeCamera;
    public CinemachineVirtualCamera activeCameraCam;


    private bool playerEnter;
    private bool playerExit;
    private bool playerInZone;


    // Max range for camera to player 
    public float minDist;
    public float maxDist;

    // Original Camera FOV
    private float _startFOV;

    // Fov to map to range
    private float _currentFov;


    //Max fov zoom set in inspector
    public float targetFov;


    public enum DistanceType
    {
        CameraToPlayer,
        ObjectToPlayer
    }

    public DistanceType _distanceType;


    private float _startDist; //Get distance to targetobj based on Collider Size


    private float currentDist; // current dist from target obj
    
    
    //speed to change Fov at
    [SerializeField] private float lerpSpeed = 4f;
    
    void Start()
    {
        _startFOV = activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens
            .FieldOfView; //get initial fov of target camera
        
        
        Player = API.GlobalReferences.PlayerRef;

        _detectPlayer = GetComponentInChildren<DetectPlayer>();

        SphereCollider col = _detectPlayer.GetComponent<SphereCollider>();


        float[] values = new []
        {
            _detectPlayer.transform.localScale.x, _detectPlayer.transform.localScale.y,
            _detectPlayer.transform.localScale.z
        };

        _startDist = col.radius * Mathf.Max(values);

        activeCameraCam = activeCamera.GetComponent<CinemachineVirtualCamera>();
    }


    void Update()
    {
        if (_distanceType == DistanceType.CameraToPlayer)
        {
            CameraToPlayerDist(); //Choose when fov will change based on the distance from the player to the camera
        }
        else if (_distanceType == DistanceType.ObjectToPlayer)
        {
            ObjectToPlayerDist(); //Choose when fov will change based on distance from player to a stationary target object in the scene
        }
    }
    


    void ObjectToPlayerDist()
    {
        if (_detectPlayer.PlayerInCollider)
        {
            currentDist =
                Vector3.Distance(Player.transform.position,
                    TargetObj.transform.position); //current distance between player and target obj

            _currentFov = Map(currentDist, _startDist, 0, _startFOV, targetFov); //Map Function

            activeCameraCam.m_Lens.FieldOfView = Mathf.Lerp(
                    activeCameraCam.m_Lens.FieldOfView, _currentFov,
                    Time.deltaTime * lerpSpeed)
                ; //set active camera fov to currentfov value
        }
        else if (_detectPlayer.PlayerInCollider == false)
        {
            activeCameraCam.m_Lens.FieldOfView = Mathf.Lerp(
                activeCameraCam.m_Lens.FieldOfView, _startFOV,
                Time.deltaTime * lerpSpeed);
            //sets fov back to initial fov when the player exits the collider
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
        _currentFov = Map(currentDist, minDist, maxDist, _startFOV, targetFov);
        if (_startFOV > targetFov) // if you want to widen the fov as the player approaches the camera
        {
            clampedFov = Mathf.Clamp(_currentFov, targetFov, _startFOV);
        }
        else //if you want to narrow fov as the player approaches the camera
        {
            clampedFov = Mathf.Clamp(_currentFov, _startFOV, targetFov);
        }

        activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = clampedFov;
    }


    public float Map(float a, float b, float c, float d, float e)
    {
        float cb = c - b; //cb is the first range which a is a part of
        float
            de = e - d; //de is the second range which the variable assigned to this function will be mapped to based off the a variable position in the first range
        float howFar = (a - b) / cb;
        return d + howFar * de;
    }
}