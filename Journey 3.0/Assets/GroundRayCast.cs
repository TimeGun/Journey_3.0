using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRayCast : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    [SerializeField] private float raycastsPerSecond;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator CheckFloor()
    {
        while (true)
        {
            Physics.Raycast()
            yield return new WaitForSeconds(1/raycastsPerSecond);
        }
    }
}
