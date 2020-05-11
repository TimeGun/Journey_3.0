using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveMainCamera : MonoBehaviour
{

    private GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Main Camera");
        SceneManager.MoveGameObjectToScene(target,SceneManager.GetSceneByName("Section10") );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveCamera()
    {
        
        
    }
}
