using System.Collections;
using UnityEngine;
using TMPro;


public class TextPopup : MonoBehaviour
{
    public static TextPopup instance;

    [SerializeField] private TMP_Text textUI;

    [SerializeField] private float timeToDisplayText;
    
    void Awake()
    {
        instance = this;
    }


    public IEnumerator DisplayText(string textToDisplay)
    {
        textUI.text = textToDisplay;
        
        yield return new WaitForSeconds(timeToDisplayText);

        textUI.text = "";
    }
}
