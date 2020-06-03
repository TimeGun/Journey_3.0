using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] public SceneBundleSO[] gameScenes;

    private SceneBundleSO currentBundle;

    public static SceneManagerScript instance;


    public static int bundleIndex = 0;

    public static bool loading = false;

    private bool baseSceneLoaded = false;

    [HideInInspector] public bool LoadSpecificBundle;

    [HideInInspector] public int sectionToLoad;

    private GameObject playerSpawn;

    public static event Action<bool> loadingUpdate;


    void Awake()
    {
        instance = this;
    }

    public IEnumerator StartGameLoad(ProgressionData progressionData)
    {
        yield return new WaitUntil(() => loading == false);
        

        GameObject torch = GameObject.Find("InteractibleTorch - Final");

        if (torch != null)
        {
            Destroy(torch);
        }

        for (int i = 1; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.name != "Base Scene")
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }


        if (!baseSceneLoaded)
        {
            SceneManager.LoadSceneAsync("Base Scene", LoadSceneMode.Additive);
            SceneManager.sceneLoaded += LoadedBaseScene;

            yield return new WaitUntil(() => baseSceneLoaded == true);
        }

        yield return new WaitUntil(() => SceneManager.sceneCount == 2);

        if (LoadSpecificBundle)
        {
            StartCoroutine(LoadBundle(LoadSpecificBundle));
            yield return new WaitUntil(() => loading == false);
        }
        else
        {
            instance.StartCoroutine(LoadSpecificSaveSection(progressionData));
            yield return new WaitUntil(() => loading == false);
        }
    }

    void LoadedBaseScene(Scene scene, LoadSceneMode mode)
    {
        baseSceneLoaded = true;
    }

    public static IEnumerator LoadBundle(bool debug)
    {
        if (debug)
        {
            if (instance.sectionToLoad != 0)
            {
                instance.currentBundle = instance.gameScenes[instance.sectionToLoad - 1];
                bundleIndex = instance.sectionToLoad - 1;

                yield return new WaitUntil(() => loading == false);

                loading = true;
                if(loadingUpdate != null)
                    loadingUpdate(loading);

                for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
                {
                    SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
                }


                SceneManager.sceneLoaded += instance.MergeScenes;

                yield return new WaitUntil(() => loading == false);

                instance.currentBundle = instance.gameScenes[instance.sectionToLoad];

                yield return new WaitUntil(() => loading == false);

                loading = true;
                if(loadingUpdate != null)
                    loadingUpdate(loading);

                for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
                {
                    SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
                }

                SceneManager.sceneLoaded += instance.MergeScenes;
            }
            else
            {
                bundleIndex = instance.sectionToLoad;
                instance.currentBundle = instance.gameScenes[instance.sectionToLoad];

                yield return new WaitUntil(() => loading == false);

                loading = true;
                if(loadingUpdate != null)
                    loadingUpdate(loading);

                for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
                {
                    SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
                }


                SceneManager.sceneLoaded += instance.MergeScenes;
            }
        }
        else
        {
            loading = true;
            if(loadingUpdate != null)
                loadingUpdate(loading);

            instance.currentBundle = instance.gameScenes[bundleIndex];

            for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
            {
                var asyncOp = SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
                
                SceneManager.sceneLoaded += instance.MergeScenes;

                asyncOp.allowSceneActivation = false; //< Deactivate the load of gameobjects on scene load

                if (asyncOp != null)
                {
                    while (!asyncOp.isDone)
                    {
                        // Check if the progress is less than 0.9 (if it's less, it means that we load gameobjects)
                        // Else, it means that we load something else.
                        if (asyncOp.progress >= 0.9f && !asyncOp.allowSceneActivation)
                        {
                            asyncOp.allowSceneActivation = true; //< Once everything is loaded, reactive this variable
                        }
                        else
                        {
                            // Scene is activated
                            yield return null; //< We still wait until the scene load is finished
                            //Then the cycle repeats for the remaining scenes in the bundle
                        }
                    }
                }
            }
        }
    }


    IEnumerator LoadSpecificSaveSection(ProgressionData data)
    {
        AssignMenuCamera(data.saveSectionIndex);
        MovePlayer(data.saveSectionIndex);
        SetDayOrNight(data.saveSectionIndex);
        AmbienceManager.instance.SetProfile(data.saveSectionIndex);


        switch (data.saveSectionIndex)
        {
            case 0:
                Debug.Log("Fist Section Load");
                bundleIndex = 0;
                MenuController.formationUse = true;
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);
                break;

            case 1:
                Debug.Log("Second Section Load");
                bundleIndex = 2;
                MenuController.formationUse = false;
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);

                break;

            case 2:
                Debug.Log("Third Section Load");
                bundleIndex = 5;
                MenuController.formationUse = false;
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);

                break;

            case 3:
                Debug.Log("Fourth Section Load");
                bundleIndex = 9;
                MenuController.formationUse = false;    
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);
                StartCoroutine(LoadBundle(false));
                yield return new WaitUntil(() => loading == false);

                break;
        }

        CinemachineBrain _brain = API.GlobalReferences.MainCamera.GetComponent<CinemachineBrain>();
        yield return new WaitUntil(() => !_brain.IsBlending);
        FadeToBlack.instance.SetBlack(false);
        MenuController.instance.OpenPauseMenu();
    }

    private void SetDayOrNight(int data)
    {
        if (data == 0 || data == 1)
        {
            LerpDayToNight.SetToDay();
        }
        else
        {
            LerpDayToNight.SetToNight();
        }
    }

    private void MovePlayer(int data)
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");

        if (data == 0)
        {
            API.GlobalReferences.PlayerRef.transform.position = playerSpawn.transform.GetChild(0).transform.position;

            API.GlobalReferences.PlayerRef.transform.rotation = playerSpawn.transform.GetChild(0).transform.rotation;
        }
        else if (data == 1)
        {
            API.GlobalReferences.PlayerRef.transform.position = playerSpawn.transform.GetChild(3).transform.position;

            API.GlobalReferences.PlayerRef.transform.rotation = playerSpawn.transform.GetChild(3).transform.rotation;
        }
        else if (data == 2)
        {
            API.GlobalReferences.PlayerRef.transform.position = playerSpawn.transform.GetChild(6).transform.position;

            API.GlobalReferences.PlayerRef.transform.rotation = playerSpawn.transform.GetChild(6).transform.rotation;
        }
        else if (data == 3)
        {
            API.GlobalReferences.PlayerRef.transform.position = playerSpawn.transform.GetChild(10).transform.position;

            API.GlobalReferences.PlayerRef.transform.rotation = playerSpawn.transform.GetChild(10).transform.rotation;
        }

        API.GlobalReferences.PlayerRef.GetComponent<ObjectDetection>().checkForObjects = true;
        AmbienceManager.FadeInMasterSound();
    }

    private void AssignMenuCamera(int data)
    {
        GameObject cameraParent = GameObject.FindWithTag("MenuCameras");

        for (int i = 0; i < cameraParent.transform.childCount; i++)
        {
            cameraParent.transform.GetChild(i).GetComponent<CinemachineVirtualCamera>().Priority = 0;
        }

        GameObject cameraToUse = cameraParent.transform.GetChild(data).gameObject;

        cameraToUse.SetActive(true);
        cameraToUse.GetComponent<CinemachineVirtualCamera>().Priority = 100;
    }


    private void AssignPlayerTransform()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        API.GlobalReferences.PlayerRef.transform.position =
            playerSpawn.transform.GetChild(sectionToLoad).transform.position;

        API.GlobalReferences.PlayerRef.transform.rotation =
            playerSpawn.transform.GetChild(sectionToLoad).transform.rotation;

        Invoke("EnablePlayerAndChangeSkybox", 2f);
    }

    void EnablePlayerAndChangeSkybox()
    {
        if (sectionToLoad >= 5)
        {
            LerpDayToNight.SetToNight();
        }

        API.GlobalReferences.PlayerRef.GetComponent<PlayerMovement>().enabled = true;
    }

    private void MergeScenes(Scene scene, LoadSceneMode mode)
    {
        
        if (LoadSpecificBundle)
        {
            if (scene.name != currentBundle.scenes[currentBundle.scenes.Length - 1])
                return;
            
            
            print("Called Merge");
            string sectionName = "Section" + bundleIndex.ToString();


            Scene section = SceneManager.CreateScene(sectionName);


            for (int i = 0; i < currentBundle.scenes.Length; i++)
            {
                SceneManager.MergeScenes(SceneManager.GetSceneByName(currentBundle.scenes[i]), section);
            }
            

            loading = false;
            if(loadingUpdate != null)
                loadingUpdate(loading);
            
            if (bundleIndex == sectionToLoad) AssignPlayerTransform();
            bundleIndex++;
        }
        else
        {
            if (scene.name != currentBundle.scenes[currentBundle.scenes.Length - 1])
                return;

            string sectionName = "Section" + bundleIndex.ToString();


            Scene section = SceneManager.CreateScene(sectionName);


            for (int i = 0; i < currentBundle.scenes.Length; i++)
            {
                SceneManager.MergeScenes(SceneManager.GetSceneByName(currentBundle.scenes[i]), section);
            }

            loading = false;
            
            if(loadingUpdate != null)
                loadingUpdate(loading);
            
            bundleIndex++;
        }
    }


    public static IEnumerator UnloadScenes(string[] scenesToUnload)
    {
        yield return new WaitUntil(() => !API.GlobalReferences.MainCamera.GetComponent<CinemachineBrain>().IsBlending);


        for (int i = 0; i < scenesToUnload.Length; i++)
        {
            Scene unloadableScene = SceneManager.GetSceneByName(scenesToUnload[i]);
            if (unloadableScene.isLoaded)
            {
                instance.MovePersistantObjects(unloadableScene);
                SceneManager.UnloadSceneAsync(scenesToUnload[i]);
            }
        }
    }

    void MovePersistantObjects(Scene sceneToCheck)
    {
        GameObject[] persistantGameObjects = sceneToCheck.GetRootGameObjects();

        List<PersistentObject> persistentObjects = new List<PersistentObject>();

        for (int i = 0; i < persistantGameObjects.Length; i++)
        {
            PersistentObject[] tempArray = persistantGameObjects[i].GetComponentsInChildren<PersistentObject>();

            if (tempArray.Length != 0)
            {
                foreach (var obj in tempArray)
                {
                    persistentObjects.Add(obj);
                }
            }
        }

        Scene sceneToMoveTo = SceneManager.GetSceneByName("Base Scene");

        for (int i = 0; i < persistentObjects.Count; i++)
        {
            if (persistentObjects[i].gameObject.scene != sceneToMoveTo)
            {
                SceneManager.MoveGameObjectToScene(persistentObjects[i].gameObject, sceneToMoveTo);
            }
        }
    }

    public static void RestartSectionFunction(int value)
    {
        instance.StartCoroutine(instance.RestartSection(value));
    }

    public IEnumerator RestartSection(int sectionToLoadInt)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name != "Manager Scene")
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }

        SceneManager.LoadSceneAsync("Base Scene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += LoadedBaseScene;

        yield return new WaitUntil(() => baseSceneLoaded == true);
        bundleIndex = sectionToLoadInt;
        sectionToLoad = sectionToLoadInt;
        LoadSpecificBundle = true;
        StartCoroutine(LoadBundle(LoadSpecificBundle));
        yield return new WaitUntil(() => loading == false);
    }


    public static List<GameObject> CheckSceneCompatibility(List<GameObject> gameObjects, string sceneName)
    {
        List<GameObject> objs = new List<GameObject>();

        foreach (GameObject obj in gameObjects)
        {
            //print("Sanity Check on: " + sceneName + " against: " + obj.scene.name);

            if (obj.scene.name == sceneName)
            {
                print("Hey HOO");
                objs.Add(obj);
            }
        }

        return objs;
    }
}