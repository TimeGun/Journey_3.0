using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject Target;
    public GameObject cameraTrigger;
    private bool playerInZone;

    private DetectPlayer detectPlayer;
    void Start()
    {
        detectPlayer = cameraTrigger.GetComponent<DetectPlayer>();
    }

    void Update()
    {
        if (detectPlayer.PlayerInCollider && Target != null)
        {
            detectPlayer.enabled = false;
            Target.gameObject.SetActive(false);
        }
    }
}
