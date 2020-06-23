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

    //public int targetPriority3;
    public GameObject targetCamera1;

    public GameObject targetCamera2;


    // Start is called before the first frame update

    private Collider col;

    void Start()
    {
        // Find Cam allows camera assignment in multi-scenes
        if (FindCam1)
        {
            StartCoroutine(AssignCameraObject1(Cam1Name));
        }

        if (FindCam2)
        {
            StartCoroutine(AssignCameraObject2(Cam2Name));
        }

        //zones collider
        if (cameraTrigger != null)
        {
            col = cameraTrigger.GetComponent<Collider>();
        }


        if (targetCamera1.GetComponent<CinemachineVirtualCameraBase>() == null)
        {
            Debug.LogWarning(targetCamera1 + " is not a Camera Object");
        }

        if (targetCamera2.GetComponent<CinemachineVirtualCameraBase>() == null)
        {
            Debug.LogWarning(targetCamera2 + " is not a Camera Object");
        }

        detectPlayer = GetComponent<DetectPlayer>();
        //initialPriority3 = targetCamera3.GetComponent<CinemachineVirtualCameraBase>().Priority;
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
    }


    // Update is called once per frame
    void Update()
    { 
        //if the player is within the trigger, set the new priorities 
        if (detectPlayer.PlayerInCollider)
        {
            if (targetCamera1 != null && targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority != targetPriority1)
            {
                targetCamera1.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority1;
            }

            if (targetCamera2 != null && targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority != targetPriority2)
            {
                targetCamera2.GetComponent<CinemachineVirtualCameraBase>().Priority = targetPriority2;
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