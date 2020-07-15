using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetControlsSelected : MonoBehaviour
{
    public enum ControlsToShow
    {
        controller,
        keyboard
    }

    [SerializeField] private GameObject right;
    [SerializeField] private GameObject left;

    public ControlsToShow select;

    public void SetCorrectSelected()
    {
        if (select == ControlsToShow.controller)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(left);
        }
        else if(select == ControlsToShow.keyboard)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(right);
        }
    }

    
}
