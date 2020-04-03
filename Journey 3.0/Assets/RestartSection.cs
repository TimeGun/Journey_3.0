using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManagerScript.RestartSectionFunction(0);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManagerScript.RestartSectionFunction(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManagerScript.RestartSectionFunction(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManagerScript.RestartSectionFunction(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManagerScript.RestartSectionFunction(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManagerScript.RestartSectionFunction(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SceneManagerScript.RestartSectionFunction(6);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SceneManagerScript.RestartSectionFunction(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManagerScript.RestartSectionFunction(8);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManagerScript.RestartSectionFunction(9);
        }
        
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManagerScript.RestartSectionFunction(10);
        }
        
    }
}
