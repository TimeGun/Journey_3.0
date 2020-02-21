using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{

    [SerializeField] private GameObject _teleportExitLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TeleportToExit(other.transform.gameObject));
        }
    }

    private IEnumerator TeleportToExit(GameObject objectToTeleport)
    {
        PlayerMovement _playerMovement = objectToTeleport.GetComponent<PlayerMovement>();

        _playerMovement.enabled = false;
        objectToTeleport.transform.position = _teleportExitLocation.transform.position;
        
        yield return new WaitForEndOfFrame();
        _playerMovement.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1f, 0.4f);
        Gizmos.DrawCube(_teleportExitLocation.transform.position, GetComponent<Collider>().bounds.size);
        
        Gizmos.color = new Color(1, 0, 1f, 0.4f);
        Gizmos.DrawCube(transform.position, GetComponent<Collider>().bounds.size);
    }
}
