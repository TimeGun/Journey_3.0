using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class InteractiblePainting : MonoBehaviour, IInteractible, IRune
{
    [SerializeField] private Material _plateMat, _frameMat;
    [SerializeField] private Animator _paintingAnimator;

    private bool paintingIsPlaying;

    [SerializeField] private string _paintingAnimationName;

    [SerializeField] private Color _emissionColour;

    [SerializeField] private float _minEmissionStrength;
    [SerializeField] private float _maxEmissionStrength;

    [SerializeField] private float _lerpSpeed = 1f;

    private float _multiplierPlate;
    private float _multiplierFrame;

    private Transform _chestHeight;

    [SerializeField] private float armSeperationFloat = 0.314f;

    [SerializeField] private float _wallSeperationBuffer;
    [SerializeField] private float _wallDistanceCheck = 1.5f;

    [SerializeField] private LayerMask _wallMask;
    
    [SerializeField] private float angleAdjustment1 = 90f;
    [SerializeField] private float angleAdjustment2 = -90f;

    [SerializeField] private float distanceToPlate = 0.5f;

    [SerializeField] private GameObject _palceToStand;
    [SerializeField] private GameObject _objectToFollow;


    // Start is called before the first frame update
    void Start()
    {
        _multiplierPlate = _maxEmissionStrength;
        _multiplierFrame = _minEmissionStrength;

        _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
        _frameMat.SetColor("_EmissiveColor", _emissionColour * _multiplierFrame);
        //StartCoroutine(PlayPainting());
    }

    IEnumerator PlayPainting(Transform player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        InteractWithObject interactWithObject = player.GetComponent<InteractWithObject>();

        ProceduralArmPlacement proceduralArmPlacement = player.GetComponent<ProceduralArmPlacement>();

        _chestHeight = player.GetChild(player.childCount - 1);

        proceduralArmPlacement.pause = true;

        _objectToFollow.transform.position = player.transform.position;
        _objectToFollow.transform.rotation = player.transform.rotation;
        
        yield return new WaitForEndOfFrame();
        movement.StartRemoteControlledMovement(_objectToFollow);

        
        
        
        
        //Walk to the place to stand
        while (_objectToFollow.transform.position != _palceToStand.transform.position)
        {
            _objectToFollow.transform.position = Vector3.MoveTowards(_objectToFollow.transform.position, _palceToStand.transform.position, Time.deltaTime *2f);

            if (Quaternion.Angle(_objectToFollow.transform.rotation,
                    Quaternion.LookRotation(new Vector3(_palceToStand.transform.position.x, _objectToFollow.transform.position.y, _palceToStand.transform.position.z) - _objectToFollow.transform.position,
                        Vector3.up)) > 5f)
            {
                _objectToFollow.transform.rotation = 
                    Quaternion.Slerp(_objectToFollow.transform.rotation,
                        Quaternion.LookRotation(new Vector3(_palceToStand.transform.position.x, _objectToFollow.transform.position.y, _palceToStand.transform.position.z) -_objectToFollow.transform.position, Vector3.up),
                        Time.deltaTime * 10f);
            }
            yield return new WaitForEndOfFrame();
        }
        
        
        Quaternion targetRotation =
            Quaternion.LookRotation(transform.position - player.transform.position, Vector3.up);

        targetRotation.eulerAngles =
            new Vector3(player.transform.rotation.x, targetRotation.eulerAngles.y, player.transform.rotation.z);

        

        while (Quaternion.Angle(player.transform.rotation, targetRotation) > 10f)
        {
            print(Quaternion.Angle(player.transform.rotation, targetRotation));
            _objectToFollow.transform.rotation = Quaternion.Slerp(_objectToFollow.transform.rotation, targetRotation, Time.deltaTime * 10f);
            yield return new WaitForEndOfFrame();
        }

        Ray leftArmRay = new Ray(_chestHeight.position - (transform.right * -armSeperationFloat), _chestHeight.forward);
        RaycastHit leftRaycastHit;


        if (Physics.Raycast(leftArmRay, out leftRaycastHit, _wallDistanceCheck, _wallMask))
        {
            if (!LeftArmIK.Instance.InUse)
            {
                print("set left arm");
                LeftArmIK.Instance.TempUse = true;

                Quaternion handRot = AdjustHandRotation(leftRaycastHit.normal);

                LeftArmIK.Instance.SetProceduralTargetAndHint(
                    leftRaycastHit.point + (leftRaycastHit.normal * _wallSeperationBuffer), handRot, 1f);
            }
        }


        while (_multiplierPlate != _minEmissionStrength)
        {
            print("Running");
            _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _minEmissionStrength, Time.deltaTime * _lerpSpeed);
            _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
            yield return new WaitForEndOfFrame();
        }

        while (_multiplierFrame != _maxEmissionStrength)
        {
            print("Running");
            _multiplierFrame = Mathf.MoveTowards(_multiplierFrame, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            _frameMat.SetColor("_EmissiveColor", _emissionColour * _multiplierFrame);
            yield return new WaitForEndOfFrame();
        }


        _paintingAnimator.Play(_paintingAnimationName);
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(_paintingAnimator.GetCurrentAnimatorStateInfo(0).length);
            
        while (_multiplierFrame != _minEmissionStrength)
        {
            print("Running");
            _multiplierFrame = Mathf.MoveTowards(_multiplierFrame, _minEmissionStrength, Time.deltaTime * _lerpSpeed);
            _frameMat.SetColor("_EmissiveColor", _emissionColour * _multiplierFrame);
            yield return new WaitForEndOfFrame();
        }

        while (_multiplierPlate != _maxEmissionStrength)
        {
            print("Running");
            _multiplierPlate = Mathf.MoveTowards(_multiplierPlate, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
            yield return new WaitForEndOfFrame();
        }

        proceduralArmPlacement.pause = false;

        interactWithObject.Cooldown = false;
        movement.StopRemoteControlledMovement();
    }


    public bool isActive()
    {
        return true;
    }

    public void StartInteraction(Transform parent)
    {
        StartCoroutine(PlayPainting(parent));
    }

    public void StopInteraction()
    {
        throw new System.NotImplementedException();
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }
    
    
    Quaternion AdjustHandRotation(Vector3 wallNormal)
    {
        Quaternion handRot = Quaternion.Inverse(Quaternion.LookRotation(wallNormal));
        
        handRot *= Quaternion.AngleAxis(angleAdjustment1, Vector3.right);
        handRot *= Quaternion.AngleAxis(angleAdjustment2, Vector3.up);

        return handRot;
    }
}