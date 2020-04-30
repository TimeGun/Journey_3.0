using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    [SerializeField] private GameObject gamepadImage, keyboardImage, gamepadText, keyboardText;

    public void ChangeSlide()
    {
        gamepadImage.SetActive(keyboardImage.activeSelf);
        keyboardImage.SetActive(!gamepadImage.activeSelf);
        gamepadText.SetActive(keyboardText.activeSelf);
        keyboardText.SetActive(!gamepadText.activeSelf);
    }
}
