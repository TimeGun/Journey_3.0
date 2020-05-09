using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCamera : MonoBehaviour
{
    public GameObject TargetCamera;
    public GameObject cameraTrigger;
    private bool playerInZone;


    void Update()
    {
        playerInZone = cameraTrigger.GetComponent<DetectPlayer>().PlayerInCollider;
        TargetCamera.SetActive(playerInZone);
        Debug.Log(TargetCamera.activeSelf);
    }

   
}
