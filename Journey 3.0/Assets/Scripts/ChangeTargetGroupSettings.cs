using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeTargetGroupSettings : MonoBehaviour
{
    public CinemachineTargetGroup TG;
    
    [Tooltip("Index value of target")] public int targetIndex;
    public float weight1;
    public float weight2;
    
    private float weightValue;
    
    public GameObject cameraTrigger1;
    public GameObject cameraTrigger2;


    private DetectPlayer _trigger1;
    private DetectPlayer _trigger2;
    

    public float rateOfChange;


    void Start()
    {
        _trigger1 = cameraTrigger1.GetComponent<DetectPlayer>();
        _trigger2 = cameraTrigger2.GetComponent<DetectPlayer>();
    }

    void FixedUpdate()
    {
        if (_trigger1.PlayerEntered)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToValue(weight1));
        }
        else if (_trigger2.PlayerEntered)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToValue(weight2));
        }
    }

    IEnumerator MoveToValue(float newWeight)
    {
        while (TG.m_Targets[targetIndex].weight > newWeight + 0.01 ||
               TG.m_Targets[targetIndex].weight < newWeight - 0.01)
        {
            float tempWeight = Mathf.Lerp(TG.m_Targets[targetIndex].weight, newWeight,
                rateOfChange / (100f * Time.deltaTime));
            
            TG.m_Targets[targetIndex].weight = tempWeight;
            
            
            yield return new WaitForEndOfFrame();
        }
    }
}