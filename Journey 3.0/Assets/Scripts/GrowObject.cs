using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowObject : MonoBehaviour, IInteractible, IRune
{
    [SerializeField] private ObjectsOnAltarPlate _objectsOnAltarPlate;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool isActive()
    {
        return true;
    }

    public void StartInteraction(Transform parent)
    {
        StartCoroutine(GrowObjectCoroutine(parent));
    }

    public void StopInteraction()
    {
        
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    IEnumerator GrowObjectCoroutine(Transform player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        InteractWithObject interactWithObject = player.GetComponent<InteractWithObject>();


        for (int i = 0; i < _objectsOnAltarPlate.ItemsOnAltar.Count; i++)
        {
            ChangeSize changeSize = _objectsOnAltarPlate.ItemsOnAltar[i].GetComponent<ChangeSize>();
            
            if (changeSize != null)
            {
                changeSize.StartChangeSize();
            }
        }


        interactWithObject.Cooldown = false;
        movement.enabled = true;
        
        
        yield return null;
    }
}
