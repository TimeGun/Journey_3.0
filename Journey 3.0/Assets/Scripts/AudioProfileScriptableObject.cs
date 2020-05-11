using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Profile", menuName = "AudioProfile")]
public class AudioProfileScriptableObject : ScriptableObject
{
    public float outsideVol;
    public float insideVol;
    public float mountainVol;
}
