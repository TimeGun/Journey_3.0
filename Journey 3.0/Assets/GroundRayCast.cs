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
            
            Debug.DrawLine(transform.position, transform.position + (Vector3.down * 5f),  Color.red);

            if (Physics.SphereCast(transform.position + (Vector3.up), 0.3f,  Vector3.down, out hit, 0.3f + 1f, _mask))
            {
                print("ayyy lamo");
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
