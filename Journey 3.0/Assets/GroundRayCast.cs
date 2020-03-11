using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRayCast : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    [SerializeField] private float raycastsPerSecond;


    [SerializeField] private bool _standingOnSeesaw;
    
    private SeeSawAnimation _seeSawAnimation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckFloor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator CheckFloor()
    {
        while (true)
        {
            RaycastHit hit;

            
            Ray ray = new Ray(transform.position, Vector3.down);
            
            Debug.DrawLine(transform.position, transform.position + (Vector3.down * 0.2f),  Color.red);

            if (Physics.Raycast(ray, out hit, 0.2f, _mask))
            {
                if (hit.transform.gameObject.layer == 13 && _seeSawAnimation == null)
                {
                    _seeSawAnimation = hit.transform.GetComponentInParent<SeeSawAnimation>();
                }
            }
            else
            {
                if (_seeSawAnimation != null)
                {
                    _seeSawAnimation.SetAnimationBoolS("PlayerPresent", false);
                    _seeSawAnimation = null;
                }
            }

            if (_seeSawAnimation != null)
            {
                _seeSawAnimation.SetAnimationBoolS("PlayerPresent", true);
            }

            yield return new WaitForSeconds(1/raycastsPerSecond);
        }
    }
}
