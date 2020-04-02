using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFollowPlayer : MonoBehaviour
{
    private Transform playerTransform;
    //public Transform player;
    public int framesToWait;

    public bool startFollowing;

    private bool stopFollowing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startFollowing)
        {
            StartCoroutine(FollowPlayer());
            startFollowing = false;
        }
    }


    IEnumerator FollowPlayer()
    {
        while (stopFollowing == false)
        {
            yield return new WaitForSeconds(Time.deltaTime * framesToWait);
            playerTransform = API.GlobalReferences.PlayerRef.transform;
            //playerTransform = player.transform;
            gameObject.transform.position = playerTransform.position;
        }
    }
        
}
