using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRayCast : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    [SerializeField] private float raycastsPerSecond;


    [SerializeField] private bool _standingOnSeesaw;

    [SerializeField] private bool _upHigh;
    
    [SerializeField] private SeeSawAnimation _seeSawAnimation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckFloor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeHigh(bool value)
    {
        _upHigh = value;
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
                if (hit.transform.gameObject.layer == 8)
                {
                    ChangeHigh(false);
                    
                    if (_seeSawAnimation != null)
                    {
                        print("sent message");
                        _seeSawAnimation.SetAnimationBoolS("PlayerPresent", false);
                        _seeSawAnimation = null;
                    }
                }

                if (hit.transform.gameObject.layer == 13 && _seeSawAnimation == null)
                {
                    _seeSawAnimation = hit.transform.GetComponentInParent<SeeSawAnimation>();
                }

                if (hit.transform.gameObject.layer == 15)
                {
                    ChangeHigh(true);
                }
            }
            else
            {
                if (_seeSawAnimation != null)
                {
                    print("sent message");
                    _seeSawAnimation.SetAnimationBoolS("PlayerPresent", false);
                    _seeSawAnimation = null;
                }
            }
            
            if (_seeSawAnimation != null)
            {
                _seeSawAnimation.SetAnimationBoolS("PlayerPresent", true);
                
                if (_upHigh)
                {
                    _seeSawAnimation.SetAnimationBoolS("Height", true);
                    _upHigh = false;
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    _seeSawAnimation.SetAnimationBoolS("Height", false);
                }
            }

            

            yield return new WaitForSeconds(1f/raycastsPerSecond);
        }
    }
}
