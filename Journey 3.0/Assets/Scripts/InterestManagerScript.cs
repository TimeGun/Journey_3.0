using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterestManagerScript : MonoBehaviour
{
    private Queue<LookAtObject> currentObjectsOfInterst = new Queue<LookAtObject>();

    public int maximumInterestCount;
    
    

    public void ResetList()
    {
        API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().ObjectsOfInterst.Clear();
        currentObjectsOfInterst.Clear();
    }

    public void LoadNewPointsOfInterest(Scene scene)
    {
        GameObject[] sceneOjbects = scene.GetRootGameObjects();
        

        for (int i = 0; i < sceneOjbects.Length; i++)
        {
            ObjectToLookAt[] tempObjs = sceneOjbects[i].GetComponentsInChildren<ObjectToLookAt>();

            if (tempObjs.Length > 0)
            {
                for (int j = 0; j < tempObjs.Length; j++)
                {
                    currentObjectsOfInterst.Enqueue(tempObjs[j].lookAtObject);
                }
            }
        }

        while (currentObjectsOfInterst.Count > maximumInterestCount)
        {
            currentObjectsOfInterst.Dequeue();
        }

        API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().ObjectsOfInterst.Clear();



        API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().ObjectsOfInterst =
            currentObjectsOfInterst.ToList();
    }
}
