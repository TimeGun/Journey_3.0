using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            Color col = right.GetComponent<Image>().color;
            right.GetComponent<Image>().color = new Color(col.r, col.g, col.b, 0);
            left.GetComponent<Image>().color = new Color(col.r, col.g, col.b, 1);
            StartCoroutine(ChangeSelectedUI(left));
        }
        else if(select == ControlsToShow.keyboard)
        {
            Color col = left.GetComponent<Image>().color;
            left.GetComponent<Image>().color = new Color(col.r, col.g, col.b, 0);
            right.GetComponent<Image>().color = new Color(col.r, col.g, col.b, 1);
            StartCoroutine(ChangeSelectedUI(right));
        }
    }

    public IEnumerator ChangeSelectedUI(GameObject newSelected)
    {
        yield return new WaitForEndOfFrame();
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(newSelected);
    }

    public void ResetColours()
    {
        Color col = left.GetComponent<Image>().color;
        left.GetComponent<Image>().color = new Color(col.r, col.g, col.b, 0);
        right.GetComponent<Image>().color = new Color(col.r, col.g, col.b, 1);
    }
}
