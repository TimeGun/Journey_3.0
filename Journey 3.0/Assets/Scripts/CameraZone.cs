using System;
using System.Collections;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UIElements;

public class CameraZone : MonoBehaviour
{
    public GameObject cameraTrigger;

    public bool FindCam1;
    public bool FindCam2;

    public string Cam1Name;
    public string Cam2Name;

    private DetectPlayer detectPlayer;
    public int targetPriority1;

    public int targetPriority2;

    public GameObject targetCamera1;

    public GameObject targetCamera2;


    private CinemachineVirtualCameraBase targetCamera1Cam;
    private CinemachineVirtualCameraBase targetCamera2Cam;


    private Collider col;

    void Start()
    {
        // Find Cam allows camera assignment in multi-scenes
        if (FindCam1)
        {
            StartCoroutine(AssignCameraObject1(Cam1Name));
        }
        else if(targetCamera1 != null)
        {
            if (targetCamera1.GetComponent<CinemachineVirtualCameraBase>() == null)
            {
                Debug.LogWarning(targetCamera1 + " is not a Camera Object");
            }
            else
            {
                targetCamera1Cam = targetCamera1.GetComponent<CinemachineVirtualCameraBase>();
            }

        }

        if (FindCam2)
        {
            StartCoroutine(AssignCameraObject2(Cam2Name));
        }
        else if(targetCamera2 != null)
        {
            if (targetCamera2.GetComponent<CinemachineVirtualCameraBase>() == null)
            {
                Debug.LogWarning(targetCamera2 + " is not a Camera Object");
            }
            else
            {
                targetCamera2Cam = targetCamera2.GetComponent<CinemachineVirtualCameraBase>();
            }
        }

        //zones collider for debug
        if (cameraTrigger != null)
        {
            col = cameraTrigger.GetComponent<Collider>();
        }


        

        detectPlayer = GetComponent<DetectPlayer>();

        if (targetCamera1 != null)
        {
        }
    }

    IEnumerator AssignCameraObject1(string cameraObj)
    {
        yield return new WaitForEndOfFrame();

        while (targetCamera1 == null)
        {
            targetCamera1 = GameObject.Find(Cam1Name);
            yield return new WaitForEndOfFrame();
            print("If this is printing every frame, thats fairly bad: " + gameObject.name);
        }

        if (targetPriority1 == 0)
        {
            Debug.LogWarning("Target Priority for target camera 1 is not set");
        }

        targetCamera1Cam = targetCamera1.GetComponent<CinemachineVirtualCameraBase>();
    }

    IEnumerator AssignCameraObject2(string cameraObj)
    {
        yield return new WaitForEndOfFrame();

        while (targetCamera2 == null)
        {
            targetCamera2 = GameObject.Find(Cam2Name);
            yield return new WaitForEndOfFrame();
            print("If this is printing every frame, thats fairly bad: " + gameObject.name);
        }

        if (targetPriority2 == 0)
        {
            Debug.LogWarning("Target Priority for target camera 2 is not set");
        }
        
        targetCamera2Cam = targetCamera2.GetComponent<CinemachineVirtualCameraBase>();
    }


    // Update is called once per frame
    void Update()
    { 
        //if the player is within the trigger, set the new priorities 
        if (detectPlayer.PlayerInCollider)
        {
            if (targetCamera1Cam != null && targetCamera1Cam.Priority != targetPriority1)
            {
                targetCamera1Cam.Priority = targetPriority1;
            }

            if (targetCamera2Cam != null && targetCamera2Cam.Priority != targetPriority2)
            {
                targetCamera2Cam.Priority = targetPriority2;
            }

        }
    }


    private void OnDrawGizmos()
    {
#if UNITY_EDITOR

        if (Application.isPlaying && isActiveAndEnabled)
        {
            if (detectPlayer.PlayerInCollider)
            {
                Gizmos.color = new Color(1, 0, 0, 0.5f);
            }
            else
            {
                Gizmos.color = new Color(1, 0, 0, 0.05f);
            }


            if (cameraTrigger != null && col.GetType() == typeof(BoxCollider))
            {
                BoxCollider boxCol = (BoxCollider) col;
                Gizmos.DrawCube(cameraTrigger.transform.TransformPoint(boxCol.center),
                    Vector3.Scale(boxCol.size, cameraTrigger.transform.localScale));
            }
            else if (cameraTrigger != null && col.GetType() == typeof(SphereCollider))
            {
                SphereCollider sphereCol = (SphereCollider) col;
                Gizmos.DrawSphere(sphereCol.center, sphereCol.radius);
            }
        }
#endif
    }
}