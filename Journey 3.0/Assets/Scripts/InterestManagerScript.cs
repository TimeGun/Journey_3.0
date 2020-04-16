using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterestManagerScript : MonoBehaviour
{
    private Queue<Transform> currentObjectsOfInterst = new Queue<Transform>();

    public int maximumInterestCount;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetList()
    {
        API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().TransformsOfInterst.Clear();
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
                    currentObjectsOfInterst.Enqueue(tempObjs[j].transform);
                }
            }
        }

        while (currentObjectsOfInterst.Count > maximumInterestCount)
        {
            currentObjectsOfInterst.Dequeue();
        }

        API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().TransformsOfInterst.Clear();



        API.GlobalReferences.PlayerRef.GetComponent<InterestFinder>().TransformsOfInterst =
            currentObjectsOfInterst.ToList();
    }
}
