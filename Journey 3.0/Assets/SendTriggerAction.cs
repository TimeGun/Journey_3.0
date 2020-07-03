using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTriggerAction : MonoBehaviour
{
    [SerializeField] private CutsceneMover _mover;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            _mover.StartPlayerAction();
            gameObject.SetActive(false);
        }
    }
}
