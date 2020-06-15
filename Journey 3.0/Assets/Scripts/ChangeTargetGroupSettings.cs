using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeTargetGroupSettings : MonoBehaviour
{
    
    public CinemachineTargetGroup TG;
    public bool afterTrigger;
   [Tooltip("Index value of target")] public int targetIndex;
    public float weight1;
    public float weight2;    
    private float weightValue;
    public GameObject cameraTrigger1;
    public GameObject cameraTrigger2;
    private bool firstTrigger;
    private bool secondTrigger;

    public float rateOfChange;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        firstTrigger = cameraTrigger1.GetComponent<DetectPlayer>().PlayerEntered;   //Use this to trigger when  offset decreases
        secondTrigger = cameraTrigger2.GetComponent<DetectPlayer>().PlayerEntered;  //Use this to trigger when  offset increases

        
        if (firstTrigger)
        {
            afterTrigger = false;        //this is used to begin decreasing the value of the offset
            StopAllCoroutines();
            StartCoroutine(MoveToValue(weight1));
            
        }
        else if (secondTrigger)
        {
            afterTrigger = true;    //this is used to begin increasing the value of the offset
            StopAllCoroutines();
            StartCoroutine(MoveToValue(weight2));
        }
        
        if (afterTrigger)
                {
                    //weightValue = weight2;
                    
                    
                }
                else
                {
                    //weightValue = weight1;
                    
                }
        
        //TG.m_Targets[targetIndex].weight = weightValue;
    }

    IEnumerator MoveToValue(float newWeight)
    {
        Debug.Log("In Coroutine");

        while ( TG.m_Targets[targetIndex].weight > newWeight + 0.01 || TG.m_Targets[targetIndex].weight < newWeight - 0.01)
        {
            Debug.Log(newWeight);
            Debug.Log(TG.m_Targets[targetIndex].weight);
            float tempWeight = Mathf.Lerp(TG.m_Targets[targetIndex].weight, newWeight, rateOfChange/100 * Time.deltaTime);
            Debug.Log(tempWeight);
            TG.m_Targets[targetIndex].weight = tempWeight;
            yield return new WaitForSeconds(Time.deltaTime);

        }
    }
    
}
