using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LightningStrike : MonoBehaviour
{
    
    //spawn on player
    // show the preemtive strike 
    //strike
    //check if the player was hit
    //normal of the mesh for rigidbody force
    //push player down the slope

    [SerializeField] private float timeBeforeStrike;

    [SerializeField] private float forceToPushPlayer;

    private Vector3 pushDirection;

    [SerializeField] private VisualEffect preStrikeVFX;

    [SerializeField] private VisualEffect strikeVFX;

    [SerializeField] private bool playerInHitbox = false;

    private GameObject player;

    [SerializeField] private LayerMask mask; 
    
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(StrikeCoroutine());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerInHitbox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInHitbox = false;
        }
    }
    IEnumerator StrikeCoroutine()
    {
        preStrikeVFX.enabled = true;
        yield return new WaitForSeconds(timeBeforeStrike);
        preStrikeVFX.Stop();
        strikeVFX.enabled = true;
        strikeVFX.Play();

        if (playerInHitbox)
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            RaycastHit hit;

            if (Physics.Raycast( ray, out hit, 2f, mask))
            {
                pushDirection = Vector3.Cross(hit.normal, transform.right);
                print(pushDirection);
            }
            
            player.GetComponent<ReceiveImpact>().AddImpact(pushDirection , forceToPushPlayer);
            
            
        }

        

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if(pushDirection != Vector3.zero)
            Gizmos.DrawLine(transform.position, transform.position + (pushDirection * 10f));
    }
}
