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


    [SerializeField] float _growSpeed = 2f;

    public float _growthMultiplier = 2f;

    private bool changingSize;


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
    }

    public void StartChangeSize()
    {
        if (!changingSize)
        {
            StartCoroutine(ChangeSizeOfObject());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ChangeSizeOfObject());
        }
    }
    

    public IEnumerator ChangeSizeOfObject()
    {
        changingSize = true;
        
        Vector3 targetScale;
        

        if (_small)
        {
            targetScale = largeScale;

            if (changeMode)
            {
                PickUpObject pickUpObject = GetComponent<PickUpObject>();
                if (pickUpObject != null)
                {
                    if (pickUpObject.isActive())
                    {
                        pickUpObject.StopInteraction();
                    }

                    Destroy(pickUpObject);
                }
            }

            _small = false;
        }
        else
        {
            targetScale = smallScale;

            if (changeMode)
            {
                GetComponent<AudioSource>().Stop();
                PushObject pushObject = GetComponent<PushObject>();

                if (pushObject != null)
                {
                    if (pushObject.isActive())
                    {
                        pushObject.StopInteraction();
                    }


                    Destroy(pushObject);
                }
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }

            _small = true;
        }



        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime * _growSpeed);
            yield return new WaitForFixedUpdate();
        }

        if (changeMode && _small == false)
        {
            PushObject pusher = gameObject.AddComponent(typeof(PushObject)) as PushObject;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }else if (changeMode && _small)
        {
            PickUpObject picker = gameObject.AddComponent(typeof(PickUpObject)) as PickUpObject;
        }

        changingSize = false;
    }
}