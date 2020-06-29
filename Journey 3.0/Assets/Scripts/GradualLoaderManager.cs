using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradualLoaderManager: MonoBehaviour
{

    public static bool bunchLoad = false;
    
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
        while (true)
        {
            if (bunchLoad)
            {
                while (_behaviours.Count > 0)
                {
                    GradualLoader _tempBehaviour = _behaviours.Dequeue();
                    print("Initialise: " + _tempBehaviour + "on frame: " + Time.frameCount);
                    _tempBehaviour.InitialiseThis();
                }
            }
            else
            {
                if (_behaviours.Count > 0)
                {
                
                    GradualLoader _tempBehaviour = _behaviours.Dequeue();
                    print("Initialise: " + _tempBehaviour + "on frame: " + Time.frameCount);
                    _tempBehaviour.InitialiseThis();
                }
            }

            
            
            yield return new WaitForEndOfFrame();
        }
    }
}

