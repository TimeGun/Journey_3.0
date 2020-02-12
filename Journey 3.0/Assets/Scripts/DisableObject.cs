using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public GameObject Target;
    public GameObject cameraTrigger;
    private bool playerInZone;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("CameraTrigger6");
    }

    // Update is called once per frame
    void Update()
    {
        playerInZone = cameraTrigger.GetComponent<DetectPlayer>().PlayerInCollider;
        if (playerInZone)
        {
            Target.GetComponent<DetectPlayer>().enabled = false;
            Target.transform.position = new Vector3(0, 0, 0);
        }
        
       
    }
}
