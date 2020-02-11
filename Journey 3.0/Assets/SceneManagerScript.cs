using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] public SceneBundleSO[] gameScenes;

    private SceneBundleSO currentBundle;

    public static SceneManagerScript instance;

    [SerializeField] private int maximumNumberOfSectionsLoaded = 3;

    private int maximumNumberOfScenesLoaded;

    private int sceneToUnloadIndex = 0;


    public static int bundleIndex = 0;

    public static bool loading = false;

    private bool baseSceneLoaded = false;
    
    
    void Start()
    {
        maximumNumberOfScenesLoaded = 2 + maximumNumberOfSectionsLoaded;
        instance = this;
        StartCoroutine(StartGameLoad());
    }

    IEnumerator StartGameLoad()
    {
        SceneManager.LoadSceneAsync("Base Scene", LoadSceneMode.Additive);
        SceneManager.sceneLoaded += LoadedBaseScene;
        
        yield return new WaitUntil(() => baseSceneLoaded == true);
        
        StartCoroutine(LoadBundle());
        yield return new WaitUntil(() => loading == false);
        StartCoroutine(LoadBundle());
        yield return new WaitUntil(() => loading == false);

        API.GlobalReferences.PlayerRef.GetComponent<PlayerMovement>().enabled = true;
    }

    void LoadedBaseScene(Scene scene, LoadSceneMode mode)
    {
        baseSceneLoaded = true;
    }

    public static IEnumerator LoadBundle()
    {
        yield return new WaitUntil(() => loading == false);

        loading = true;
        print("Bundle Loading");
        
        //Sets the current bundle to be the bundleIndex number
        instance.currentBundle = instance.gameScenes[bundleIndex];
        
        for (int i = 0; i < instance.currentBundle.scenes.Length; i++)
        {
            SceneManager.LoadSceneAsync(instance.currentBundle.scenes[i], LoadSceneMode.Additive);
        }

        SceneManager.sceneLoaded += instance.MergeScenes;
        
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
        print("Bundle Loaded");
        loading = false;
        bundleIndex++;


        if (SceneManager.sceneCount > maximumNumberOfScenesLoaded)
        {
            UnloadScenes();
        }
    }

    void UnloadScenes()
    {
        string sceneToUnloadName = "Section"+ sceneToUnloadIndex.ToString();
        
        SceneManager.UnloadSceneAsync(sceneToUnloadName);

        sceneToUnloadIndex++;
    }
}
