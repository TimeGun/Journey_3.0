using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawTrigger : MonoBehaviour
{
    [SerializeField] private SeeSawPlacement _seeSawPlacement;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _seeSawPlacement.PlayerInThisTrigger = true;
            other.SendMessage("ChangeInPlacementBool", true);
            other.SendMessage("SetPlacementArea", _seeSawPlacement);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _seeSawPlacement.PlayerInThisTrigger = false;
            other.SendMessage("ChangeInPlacementBool", false);
        }
    }
}
