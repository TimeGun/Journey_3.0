using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldInteractipleOnRune : MonoBehaviour, IInteractible, IRune
{
    private bool _itemOnRuneBoolBool;

    public bool ItemOnRuneBool
    {
        get => _itemOnRuneBoolBool;
        set => _itemOnRuneBoolBool = value;
    }

    [SerializeField] private GameObject _itemOnRune;

    public GameObject ItemOnRune
    {
        get => _itemOnRune;
        set => _itemOnRune = value;
    }


    [SerializeField] private Transform objectPlaceArea;

    public Transform ObjectPlaceArea
    {
        get => objectPlaceArea;
        set => objectPlaceArea = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isActive()
    {
        return true;
    }

    public void StartInteraction(Transform parent)
    {
        
    }

    public void StopInteraction()
    {
        
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }
}
