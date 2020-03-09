using System.Collections;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    public bool changeMode;

    private Vector3 smallScale;

    private Vector3 largeScale;

    [SerializeField] private bool _small;

    public bool Small
    {
        get => _small;
    }

    public bool startSmall;

    private Rigidbody _rb;

    private GravityCheck _gravityCheck;

    [SerializeField] float _growSpeed = 2f;

    public float _growthMultiplier = 2f;



    private void Start()
    {
        if (startSmall)
        {
            smallScale = transform.localScale;
            largeScale = transform.localScale * _growthMultiplier;
            _small = true;
        }
        else
        {
            smallScale = transform.localScale / _growthMultiplier;
            largeScale = transform.localScale;
            _small = false;
        }

        _rb = GetComponent<Rigidbody>();
        _gravityCheck = GetComponent<GravityCheck>();
    }

    public IEnumerator ChangeSizeOfObject()
    {
        
        Vector3 targetScale;
        
        _gravityCheck.enabled = true;

        if (_small)
        {
            targetScale = largeScale;

            if (changeMode)
            {
                Destroy(GetComponent<PickUpObject>());
                PushObject pusher = gameObject.AddComponent(typeof(PushObject)) as PushObject;
            }

            _small = false;
        }
        else
        {
            targetScale = smallScale;

            if (changeMode)
            {
                Destroy(GetComponent<PushObject>());
                PickUpObject picker = gameObject.AddComponent(typeof(PickUpObject)) as PickUpObject;
            }

            _small = true;
        }



        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime * _growSpeed);
            yield return new WaitForFixedUpdate();
        }

        _gravityCheck.enabled = true;
    }
}