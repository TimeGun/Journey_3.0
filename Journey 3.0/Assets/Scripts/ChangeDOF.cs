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


    
    
    // Start is called before the first frame update
    void Start()
    {
        Volume volume = GetComponent<Volume>();
        DepthOfField tempDof;

        if (volume.profile.TryGet<DepthOfField>(out tempDof))
        {
            DOF = tempDof;
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (switchDOF)
        {
            
            
            DOF.nearFocusStart.value = Mathf.Lerp(DOF.nearFocusStart.value, nearBlurStart, rateOfChange/100); 
            DOF.nearFocusEnd.value = Mathf.Lerp(DOF.nearFocusEnd.value, nearBlurEnd,  rateOfChange/100); 
            DOF.farFocusStart.value = Mathf.Lerp(DOF.farFocusStart.value, farFocusStart,  rateOfChange/100); 
            DOF.farFocusEnd.value = Mathf.Lerp(DOF.farFocusEnd.value, farFocusEnd,  rateOfChange/100); 
        }
        
    }

   
}
