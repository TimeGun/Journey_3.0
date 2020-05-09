using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradualLoaderManager: MonoBehaviour
{
    public static Queue<GradualLoader> _behaviours = new Queue<GradualLoader>();

    public static Queue<GradualLoader> Behaviours
    {
        get => _behaviours;
    }

    void Start()
    {
        StartCoroutine(InitiliseBehaviours());
    }

    public static IEnumerator InitiliseBehaviours()
    {
        print("Started Coroutine");
        while (true)
        {
            if (_behaviours.Count > 0)
            {
                
                GradualLoader _tempBehaviour = _behaviours.Dequeue();
                print("Initialise: " + _tempBehaviour + "on frame: " + Time.frameCount);
                _tempBehaviour.InitialiseThis();
            }
            
            yield return new WaitForEndOfFrame();
        }
    }
}