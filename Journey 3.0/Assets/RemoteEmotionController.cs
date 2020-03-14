using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RemoteEmotionController : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private EmotionController _emotionController;


    void Start()
    {
        _player = GameObject.Find("Player");
        _emotionController = _player.GetComponent<EmotionController>();
    }

    public void ChangePlayerEmissionColour(string newColourString)
    {
        _emotionController.SetColour(ColourExtensions.ParseColor(newColourString));
    }

    public void ChangePlayerEmissionIntensity(float newIntensity)
    {
        _emotionController.SetEmissionIntensity(newIntensity);
    }
    
    
    
    
}

public static class ColourExtensions
{
    public static Color ParseColor(string aCol)
    {
        var strings = aCol.Split(',');
        Color output = Color.black;
        for (var i = 0; i < strings.Length; i++)
        {
            output[i] = float.Parse(strings[i]);
        }
        return output;
    }
}
