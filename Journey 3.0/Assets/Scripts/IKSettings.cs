using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IKSetting", menuName = "IKSettings")]
public class IKSettings : ScriptableObject
{ 
        public Vector3 targetPos;
        public Quaternion targetRot;
        public Vector3 hintPos;
}
