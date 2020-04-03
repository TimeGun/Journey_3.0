using System.Collections;
using System.Collections.Generic;
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

    private GameObject [] playerSpawn;


    void Start()
    {
        instance = this;
        StartCoroutine(StartGameLoad());
    }

    IEnumerator StartGameLoad()
    {
        SceneManager.LoadSceneAsync("Base Scene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += LoadedBaseScene;

        yield return new WaitUntil(() => baseSceneLoaded == true);

        baseSceneLoaded = false;
        
        if (LoadSpecificBundle)
        {
            StartCoroutine(LoadBundle(LoadSpecificBundle));
            yield return new WaitUntil(() => loading == false);
        }
        else
        {
            StartCoroutine(LoadBundle(false));
            yield return new WaitUntil(() => loading == false);
            StartCoroutine(LoadBundle(false));
            yield return new WaitUntil(() => loading == false);

            API.GlobalReferences.PlayerRef.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void LoadedBaseScene(Scene scene, LoadSceneMode mode)
    {
        baseSceneLoaded = true;
    }

    public static IEnumerator LoadBundle(bool loadCertainSection)
    {
        if (loadCertainSection)
        {
            if (instance.sectionToLoad != 0)
            {
                instance.currentBundle = instance.gameScenes[instance.sectionToLoad - 1];
                bundleIndex = instance.sectionToLoad - 1;

                yield return new WaitUntil(() => loading == false);

                loading = true;

                for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
                {
                    SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
                }


                SceneManager.sceneLoaded += instance.MergeScenes;

                yield return new WaitUntil(() => loading == false);

                instance.currentBundle = instance.gameScenes[instance.sectionToLoad];

                yield return new WaitUntil(() => loading == false);

                loading = true;

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

                for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
                {
                    SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
                }


                SceneManager.sceneLoaded += instance.MergeScenes;
            }
        }
        else
        {
            yield return new WaitUntil(() => loading == false);

            loading = true;

            //Sets the current bundle to be the bundleIndex number
            if (instance.gameScenes.Length > bundleIndex)
                instance.currentBundle = instance.gameScenes[bundleIndex];

            for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
            {
                SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
            }

            SceneManager.sceneLoaded += instance.MergeScenes;
        }
    }

    private void AssignPlayerTransform()
    {
        playerSpawn = GameObject.FindGameObjectsWithTag("PlayerSpawn");
        API.GlobalReferences.PlayerRef.transform.position = playerSpawn[playerSpawn.Length-1].transform.position;

        API.GlobalReferences.PlayerRef.transform.rotation = playerSpawn[playerSpawn.Length-1].transform.rotation;
        
        Invoke("EnablePlayerAndChangeSkybox", 2f);
        
        
    }

    void EnablePlayerAndChangeSkybox()
    {
        if (sectionToLoad >= 4)
        {
            GameObject.Find("Skybox Switch").GetComponent<UnityEventTrigger>()._unityEventToTrigger.Invoke();
        }

        API.GlobalReferences.PlayerRef.GetComponent<PlayerMovement>().enabled = true;
    }

    private void MergeScenes(Scene scene, LoadSceneMode mode)
    {
        if (LoadSpecificBundle)
        {
            if (scene.name != currentBundle.scenes[currentBundle.scenes.Length -1])
                return;

            string sectionName = "Section" + bundleIndex.ToString();


            Scene section = SceneManager.CreateScene(sectionName);


            for (int i = 0; i < currentBundle.scenes.Length; i++)
            {
                SceneManager.MergeScenes(SceneManager.GetSceneByName(currentBundle.scenes[i]), section);
            }


            API.InterestManagerScript.LoadNewPointsOfInterest(section);


            loading = false;
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


            API.InterestManagerScript.LoadNewPointsOfInterest(section);


            loading = false;
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
                SceneManager.UnloadSceneAsync(scenesToUnload[i]);
            }
        }
    }

    public static void RestartSectionFunction(int value)
    {
        instance.StartCoroutine(instance.RestartSection(value));
    }

    public IEnumerator RestartSection(int sectionToLoadInt)
    {
        for(int i = 0; i < SceneManager.sceneCount; i++)
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
}