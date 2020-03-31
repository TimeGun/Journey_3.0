using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCollisionDetection : MonoBehaviour
{

    bool collidingWithWall = false;

    [SerializeField] List<GameObject> collidingObjects = new List<GameObject>();

    public List<GameObject> CollidingObjects
    {
        get => collidingObjects;
    }

    [SerializeField] LayerMask mask;

    GameObject _player;

    [SerializeField] GameObject parentObject;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }

    public bool IsCollidingWithWall() {
        return !CollidingObjectsIsEmpty();
    }


    bool CollidingObjectsIsEmpty() {
        if (collidingObjects.Count == 0)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & mask) != 0 && other.gameObject != parentObject) {
            collidingObjects.Add(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (collidingObjects.Contains(other.gameObject))
        {
            collidingObjects.Remove(other.gameObject);
        }
    }

    private void Update()
    {
        Vector3 playerAtThisHeight = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        transform.rotation = Quaternion.LookRotation(playerAtThisHeight - transform.position, Vector3.up);
    }
}
