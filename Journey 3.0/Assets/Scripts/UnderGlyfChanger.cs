using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGlyfChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableObject()
    {
       API.GlobalReferences.UnderGlyf.SetActive(true);
    }

    public void DisableObject()
    {
       API.GlobalReferences.UnderGlyf.SetActive(false);
    }
}
