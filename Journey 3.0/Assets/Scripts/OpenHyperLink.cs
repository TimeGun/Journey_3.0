using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHyperLink : MonoBehaviour
{
    public string hyperLink;
    
    public void OpenLink()
    {
        Application.OpenURL(hyperLink);
    }
}
