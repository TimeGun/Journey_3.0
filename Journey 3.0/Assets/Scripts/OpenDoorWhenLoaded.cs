using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoorWhenLoaded : MonoBehaviour
{
    [SerializeField] private string gameObjectName;
    [SerializeField] private string triggerName = "Elevator Doors & Terrain";

    [SerializeField] private GameObject doors;

    public void OpenElevatorDoors()
    {
        StartCoroutine(OpenElevatorWhenSceneIsLoaded());
    }

    private IEnumerator OpenElevatorWhenSceneIsLoaded()
    {
        yield return new WaitUntil(() => !SceneManagerScript.loading);
        yield return new WaitForEndOfFrame();
        
        GameObject terrain = GameObject.Find(gameObjectName);
        
        while (terrain == null)
        {
            print("tries");
            terrain = GameObject.Find(gameObjectName);
            yield return new WaitForEndOfFrame();
        }

        
        
        terrain.transform.GetChild(0).gameObject.SetActive(true);
        
        doors.GetComponent<Animator>().SetTrigger(triggerName);
    }
}
