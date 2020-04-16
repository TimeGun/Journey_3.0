using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BluAIController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private Rig _headAim;

    [SerializeField] private Transform _aimTarget;

    [SerializeField] private float _updatesPerSecond = 4f;


    [SerializeField] private bool playerLookAt;

    [SerializeField] private GameObject _player;
    [SerializeField] private Vector3 offSet = new Vector3(0, 1, 0);

    [SerializeField] private float _lerpSpeed = 1f;

    private int flipInt = 1;
    private bool flipBool;


    [SerializeField] private float distanceCheck;
    [SerializeField] private float angleCheck;

    // Start is called before the first frame update
    void Start()
    {
        //_player = API.GlobalReferences.PlayerRef;

        if (_player == null)
        {
            _player = GameObject.Find("Player");
        }


        StartCoroutine(CheckDistanceAndAngleToPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && flipBool)
        {
            flipBool = false;
            flipInt = flipInt * -1;
            _animator.SetInteger("Switch", flipInt);
            

            print("jump");
        }
        else if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !flipBool)
        {
            flipBool = true;
        }

        if (playerLookAt)
        {
            _aimTarget.transform.position = _player.transform.position + offSet;
            _headAim.weight = Mathf.Lerp(_headAim.weight, 1f, Time.deltaTime * _lerpSpeed);
        }
        else
        {
            _animator.SetFloat("PlaybackSpeed", 1);
            _headAim.weight = Mathf.Lerp(_headAim.weight, 0f, Time.deltaTime * _lerpSpeed);
        }
    }


    IEnumerator CheckDistanceAndAngleToPlayer()
    {
        while (true)
        {
            Vector3 toPlayer = (_player.transform.position + offSet) - transform.position;
            
            if (Vector3.Angle(transform.forward, toPlayer) < angleCheck &&
                Vector3.Distance(transform.position, _player.transform.position + offSet) < distanceCheck)
            {
                playerLookAt = true;
            }
            else
            {
                playerLookAt = false;
            }
            _animator.SetBool("LookAtPlayer", playerLookAt);
            yield return new WaitForSeconds(1f / _updatesPerSecond);
        }
    }
}