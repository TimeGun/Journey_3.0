using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustBounceRotation : GradualLoader
{
    private GameObject _player;

    [SerializeField] private ChangeSize _changeSize;

    private Collider _collider;

    private PushObject _pushObject;
    public override void Awake()
    {
        print("Called Awake");
        base.Awake();
    }
    
    IEnumerator Start()
    {
        yield return new WaitUntil(() => initialised);
        
        _player = GameObject.Find("Player");
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initialised)
        {
            Vector3 playerAtThisHeight = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
            transform.rotation = Quaternion.LookRotation(playerAtThisHeight - transform.position, Vector3.up);
        
            if (!_changeSize.Small)
            {
                if (_pushObject != null)
                {
                    if (_pushObject.isActive())
                    {
                        _collider.isTrigger = false;
                    }
                    else
                    {
                        _collider.isTrigger = false;
                    }
                }
                else
                {
                    _collider.isTrigger = false;
                }
            }
            else
            {
                _collider.isTrigger = true;
            }
        }
    }

    public void AsignPushObject(PushObject value)
    {
        _pushObject = value;
    }
    
    public override void EnqueThis()
    {
        
        base.EnqueThis();
    }

    public override void InitialiseThis()
    {
        base.InitialiseThis();
    }
}
