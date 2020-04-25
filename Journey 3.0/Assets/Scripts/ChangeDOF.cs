using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ChangeDOF : MonoBehaviour
{
    
    public bool switchDOF;
    DepthOfField DOF;

    public float nearBlurStart;
    public float nearBlurEnd;
    public float farFocusStart;
    public float farFocusEnd;

    public float rateOfChange;

    private float initialNearBlurStart;
    private float initialNearBlurEnd;
    private float initialFarFocusStart;
    private float initialFarFocusEnd;

    private Volume volume;
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();
        

        
        


    }

    // Update is called once per frame
    void Update()
    {
        if (switchDOF)
        {

            //StartCoroutine(SwitchDepthOfField());

        }
        //Debug.Log("DOF: " + DOF.active);
    }

    public void StartSwitchDepthOfField(string newFloatString)
    {
        
        ArrayList noiseArrayList = new ArrayList();
        noiseArrayList = ParseSomething.ParseFloats(newFloatString);
        StartCoroutine(SwitchDepthOfField((float)noiseArrayList[0] , (float)noiseArrayList[1], (float)noiseArrayList[2], (float)noiseArrayList[3]));
    }
    
    public void StartRevertSwitchDepthOfField(float farFocusEnd)
    {
        StartCoroutine(RevertDepthOfField(farFocusEnd));
    }

    public void ActivateDepthOfField()
    {
//        Debug.Log("Actiavted");
        DepthOfField tempDof;

        if (volume.profile.TryGet<DepthOfField>(out tempDof))
        {
            DOF = tempDof;
//            Debug.Log("TempDOF" + tempDof.active);
//            Debug.Log("DOF1: " + DOF.active);
        }
        DOF.active = true;
//        Debug.Log("DOF: " + DOF.active);
        initialNearBlurStart = DOF.nearFocusStart.value;
        initialNearBlurEnd = DOF.nearFocusEnd.value;
        initialFarFocusStart = DOF.farFocusStart.value;
        initialFarFocusEnd = DOF.farFocusEnd.value;
    }

    public void DisableDepthOfField()
    {
        DOF.active = false;
    }
    
    
    
    IEnumerator SwitchDepthOfField(float nearBlurStart, float nearBlurEnd, float farFocusStart, float farFocusEnd)
    {
        while ( DOF.farFocusEnd.value < farFocusEnd - 0.05 || DOF.farFocusEnd.value > farFocusEnd + 0.05)
        {
            DOF.nearFocusStart.value = Mathf.Lerp(DOF.nearFocusStart.value, nearBlurStart, rateOfChange/100); 
            DOF.nearFocusEnd.value = Mathf.Lerp(DOF.nearFocusEnd.value, nearBlurEnd,  rateOfChange/100); 
            DOF.farFocusStart.value = Mathf.Lerp(DOF.farFocusStart.value, farFocusStart,  rateOfChange/100); 
            DOF.farFocusEnd.value = Mathf.Lerp(DOF.farFocusEnd.value, farFocusEnd,  rateOfChange/100);
            yield return new WaitForSeconds(Time.deltaTime);
//            Debug.Log("in depth of field"); 
        }
        
    }

    IEnumerator RevertDepthOfField(float farFocusEnd)
    {
        while (DOF.farFocusEnd.value < initialFarFocusEnd - 0.05 || DOF.farFocusEnd.value > farFocusEnd + 0.05 )
        {
            DOF.nearFocusStart.value = Mathf.Lerp(DOF.nearFocusStart.value, initialNearBlurStart, rateOfChange/100); 
            DOF.nearFocusEnd.value = Mathf.Lerp(DOF.nearFocusEnd.value, initialNearBlurEnd,  rateOfChange/100); 
            DOF.farFocusStart.value = Mathf.Lerp(DOF.farFocusStart.value, initialFarFocusStart,  rateOfChange/100); 
            DOF.farFocusEnd.value = Mathf.Lerp(DOF.farFocusEnd.value, initialFarFocusEnd,  rateOfChange/100);
            yield return new WaitForSeconds(Time.deltaTime);
//            Debug.Log("in revert depth of field"); 
        }
    }
}
