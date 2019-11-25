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
    private bool playerInZone;
    private float startFOV;
    private float currentFov;
    public float targetFov;
    
    
    

    private float _startDist; //Get distance to targetobj when player enters collider

    private float currentDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInZone = Trigger.GetComponent<DetectPlayer>().PlayerEntered;
        currentFov = activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
        currentDist = Vector3.Distance(Player.transform.position, TargetObj.transform.position);
        if (playerInZone)
        {
            
            _startDist = Vector3.Distance(Player.transform.position, TargetObj.transform.position);
            startFOV = activeCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView;
            
        }

        currentFov = Map(currentDist, _startDist, 0, startFOV, targetFov);
    }
    
    public float DistCovered()
    {
        if (playerInZone)
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
    }
}
