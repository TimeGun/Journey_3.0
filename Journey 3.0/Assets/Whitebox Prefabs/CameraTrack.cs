using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    public Transform player;
    float value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        value = player.position.z - 67;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + (value * 0.4f)); 
    }
}
