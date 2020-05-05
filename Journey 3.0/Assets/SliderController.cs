using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private GameObject gamepadImage, keyboardImage, gamepadText, keyboardText;

    public Scrollbar _scrollbar;
    
    public void ChangeSlide()
    {
        if (_scrollbar.value > 0.5)
        {
            gamepadImage.SetActive(false);
            keyboardImage.SetActive(true);
            gamepadText.SetActive(false);
            keyboardText.SetActive(true);
        }
        else
        {
            gamepadImage.SetActive(true);
            keyboardImage.SetActive(false);
            gamepadText.SetActive(true);
            keyboardText.SetActive(false);
        }
    }
}
