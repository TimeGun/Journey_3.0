using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCamera : MonoBehaviour
{
    public GameObject TargetCamera;
    public GameObject cameraTrigger;
    private bool playerInZone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInZone = cameraTrigger.GetComponent<DetectPlayer>().PlayerInCollider;
        TargetCamera.SetActive(playerInZone);
        Debug.Log(TargetCamera.activeSelf);
        
    }

   
}
