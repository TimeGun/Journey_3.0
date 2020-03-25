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

        if (LoadSpecificBundle)
        {
            StartCoroutine(LoadBundle(LoadSpecificBundle));
            yield return new WaitUntil(() => loading == false);

            API.GlobalReferences.PlayerRef.GetComponent<PlayerMovement>().enabled = true;
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
            yield return new WaitUntil(() => loading == false);
            
            loading = true;
            print("Bundle Loading");
            
            for (int i = 0; i < instance.gameScenes[instance.sectionToLoad].scenes.Length; i++)
            {
                SceneManager.LoadSceneAsync(instance.gameScenes[instance.sectionToLoad].scenes[i], LoadSceneMode.Additive);
            }

            SceneManager.sceneLoaded += instance.MergeScenes;
        }
        else
        {
            yield return new WaitUntil(() => loading == false);

            loading = true;
            print("Bundle Loading");
        
            //Sets the current bundle to be the bundleIndex number
            print(bundleIndex);
            if(instance.gameScenes.Length > bundleIndex)
                instance.currentBundle = instance.gameScenes[bundleIndex];
        
            for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
            {
                SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
            }

            SceneManager.sceneLoaded += instance.MergeScenes;
        }
    }

    private void MergeScenes(Scene scene, LoadSceneMode mode)
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
        
        print(SceneManager.sceneCount);

        print("Bundle Loaded");
        loading = false;
        bundleIndex++;
    }
    
    
    public static IEnumerator UnloadScenes(string[] scenesToUnload)
    {
        yield return new WaitUntil(() => !API.GlobalReferences.MainCamera.GetComponent<CinemachineBrain>().IsBlending);

        for (int i = 0; i < scenesToUnload.Length; i++) {
            SceneManager.UnloadSceneAsync(scenesToUnload[i]);
        }

        print("Scenes Unloaded");
    }
}


