using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IKSetting List", menuName = "ListOfIKSettings")]
public class ListOfIKSettings : ScriptableObject
{
    public IKSettings[] premadeIKSettings;
}
