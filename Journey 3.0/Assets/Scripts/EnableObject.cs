using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObject : MonoBehaviour
{
    public GameObject Target;
    public GameObject cameraTrigger;
    private bool playerInZone;

    private DetectPlayer detectPlayer;
    // Start is called before the first frame update
    void Start()
    {
        detectPlayer = cameraTrigger.GetComponent<DetectPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (detectPlayer.PlayerInCollider && Target != null && Target.activeSelf == false)
        {
            Target.gameObject.SetActive(true);
            //Target.GetComponent<DetectPlayer>().enabled = false;
            //Debug.Log("player oi");
        }
    }
}