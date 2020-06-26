using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneMover : MonoBehaviour
{
    public GameObject goalObject;
    public GameObject moverObject;

    public float moveSpeed = 2f;
    public float turnSpeed = 10f;

    public bool unfreezeOnActionComplete = false;

    public enum PlayerAction
    {
        TurnToFace,
        MoveToLocation
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPlayerAction();
        }
    }

    public PlayerAction playerAction;

    public void StartPlayerAction()
    {
        if (LevelSelectEnabler.instance != null)
        {
            LevelSelectEnabler.DisableButton();
        }

        
        switch (playerAction)
        {
            case PlayerAction.MoveToLocation:
                StartCoroutine(MoveToLocation());
                break;
            case PlayerAction.TurnToFace:
                StartCoroutine(TurnToFace());
                break;
            default:
                Debug.LogWarning("Action not assigned");
                LevelSelectEnabler.EnableButton();
                break;
        }
    }

    IEnumerator MoveToLocation()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        moverObject.transform.position = player.transform.position;
        moverObject.transform.rotation = player.transform.rotation;
        
        movement.StartRemoteControlledMovement(moverObject);
        
        while (moverObject.transform.position != goalObject.transform.position)
        {
            moverObject.transform.position = Vector3.MoveTowards(moverObject.transform.position, goalObject.transform.position, Time.deltaTime * moveSpeed);

            if (Quaternion.Angle(moverObject.transform.rotation,
                    Quaternion.LookRotation(new Vector3(goalObject.transform.position.x, goalObject.transform.position.y, goalObject.transform.position.z) - moverObject.transform.position,
                        Vector3.up)) > 1f)
            {
                moverObject.transform.rotation = 
                    Quaternion.Slerp(moverObject.transform.rotation,
                        Quaternion.LookRotation(new Vector3(goalObject.transform.position.x, moverObject.transform.position.y, goalObject.transform.position.z) -moverObject.transform.position, Vector3.up),
                        Time.deltaTime * turnSpeed);
            }
            yield return new WaitForEndOfFrame();
        }

        if (LevelSelectEnabler.instance != null)
        {
            LevelSelectEnabler.EnableButton();
        }
        
        if (unfreezeOnActionComplete)
        {
            movement.StopRemoteControlledMovement();
        }
        
        Debug.Log("Action Complete");
    }
    
    IEnumerator TurnToFace()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        moverObject.transform.position = player.transform.position;
        moverObject.transform.rotation = player.transform.rotation;
        
        movement.StartRemoteControlledMovement(moverObject);

        
        Quaternion targetRotation =
            Quaternion.LookRotation(goalObject.transform.position - player.transform.position, Vector3.up);

        targetRotation.eulerAngles =
            new Vector3(player.transform.rotation.x, targetRotation.eulerAngles.y, player.transform.rotation.z);
        
        
        
        while (Quaternion.Angle(player.transform.rotation, targetRotation) > 10f)
        {
            moverObject.transform.rotation = Quaternion.Slerp(moverObject.transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            yield return new WaitForEndOfFrame();
        }

        if (LevelSelectEnabler.instance != null)
        {
            LevelSelectEnabler.EnableButton();
        }

        if (unfreezeOnActionComplete)
        {
            movement.StopRemoteControlledMovement();
        }

        Debug.Log("Action Complete");
    }
}
