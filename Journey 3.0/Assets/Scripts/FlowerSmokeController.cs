using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FlowerSmokeController : MonoBehaviour
{
    public static FlowerSmokeController instance;

    [SerializeField] private VisualEffect _smokingFlowerVFX;

    private Coroutine _coroutine;
    
    void Start()
    {
        instance = this;
    }

    public void StartSmoking(float timeToSmokeFor)
    {
        StopAllCoroutines();
        _coroutine = StartCoroutine(FlowerSmoking(timeToSmokeFor));
    }

    private IEnumerator FlowerSmoking(float value)
    {
        _smokingFlowerVFX.SetBool("isSpawning", true);
        yield return new WaitForSeconds(value);
        
        _smokingFlowerVFX.SetBool("isSpawning", false);

    }
}
