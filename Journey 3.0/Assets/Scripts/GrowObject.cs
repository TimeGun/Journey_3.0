using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GrowObject : MonoBehaviour, IInteractible, IRune
{
    [SerializeField] private ObjectsOnAltarPlate _objectsOnAltarPlate;

    [SerializeField] private VisualEffect[] firePits;

    [SerializeField] private Material _runeMat, _plateMat;

    [SerializeField] private Color _emissionColour;

    [SerializeField] private float _minEmissionStrength = 0f;
    [SerializeField] private float _maxEmissionStrength = 10f;

    private float _multiplierRune;
    private float _multiplierPlate;

    [SerializeField] private float _lerpSpeed = 5f;


    [SerializeField] private FadeLight _fadeLight;

    private Transform _chestHeight;

    [SerializeField] private GameObject _palceToStand;
    [SerializeField] private GameObject _objectToFollow;


    [SerializeField] private float armSeperationFloat = 0.314f;

    [SerializeField] private float _wallSeperationBuffer = 0.1f;
    [SerializeField] private float _wallDistanceCheck = 1.5f;

    [SerializeField] private LayerMask _wallMask;

    [SerializeField] private Light plateLight;
    [SerializeField] private float lightIntensity;

    [SerializeField] private AudioSource _backgroundSound;
    [SerializeField] private AudioSource _lightSound;

    void Start()
    {
        _multiplierRune = _maxEmissionStrength;
        _multiplierPlate = _minEmissionStrength;

        _runeMat.SetColor("_EmissiveColor", _emissionColour * _multiplierRune);
        _plateMat.SetColor("_EmissiveColor", _emissionColour * _multiplierPlate);
    }


    public bool isActive()
    {
        return true;
    }

    public void StartInteraction(Transform parent)
    {
        StartCoroutine(GrowObjectCoroutine(parent));
    }

    public void StopInteraction()
    {
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    IEnumerator GrowObjectCoroutine(Transform player)
    {
        LevelSelectEnabler.DisableButton();
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
            _objectToFollow.transform.position = Vector3.MoveTowards(_objectToFollow.transform.position,
                _palceToStand.transform.position, Time.deltaTime * 2f);

            if (Quaternion.Angle(_objectToFollow.transform.rotation,
                    Quaternion.LookRotation(
                        new Vector3(_palceToStand.transform.position.x, _objectToFollow.transform.position.y,
                            _palceToStand.transform.position.z) - _objectToFollow.transform.position,
                        Vector3.up)) > 5f)
            {
                _objectToFollow.transform.rotation =
                    Quaternion.Slerp(_objectToFollow.transform.rotation,
                        Quaternion.LookRotation(
                            new Vector3(_palceToStand.transform.position.x, _objectToFollow.transform.position.y,
                                _palceToStand.transform.position.z) - _objectToFollow.transform.position, Vector3.up),
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
            _objectToFollow.transform.rotation = Quaternion.Slerp(_objectToFollow.transform.rotation, targetRotation,
                Time.deltaTime * 10f);
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

                LeftArmIK.Instance.SetProceduralTargetAndHint(
                    leftRaycastHit.point + (leftRaycastHit.normal * _wallSeperationBuffer), leftRaycastHit.normal, 1f);
            }
        }


        _fadeLight.enabled = false;


        firePits[0].SetBool("RuneUsed", true);
        firePits[1].SetBool("RuneUsed", true);
        _backgroundSound.Play();


        while (_multiplierRune != _minEmissionStrength)
        {
            print("Running");
            _multiplierRune = Mathf.MoveTowards(_multiplierRune, _minEmissionStrength, Time.deltaTime * _lerpSpeed);
            _runeMat.SetColor("_EmissiveColor", _emissionColour * _multiplierRune);
            yield return new WaitForEndOfFrame();
        }
        
        _lightSound.Play();

        while (plateLight.intensity < lightIntensity)
        {
            plateLight.intensity = Mathf.Lerp(plateLight.intensity, lightIntensity, _lerpSpeed * Time.deltaTime * 100f);
            yield return new WaitForEndOfFrame();
        }


        for (int i = 0; i < _objectsOnAltarPlate.ItemsOnAltar.Count; i++)
        {
            ChangeSize changeSize = _objectsOnAltarPlate.ItemsOnAltar[i].GetComponent<ChangeSize>();

            if (changeSize != null)
            {
                changeSize.StartChangeSize();
            }
        }
        
        
        player.GetComponent<EmotionController>().SetTempColour(_emissionColour, 6f);


        while (_multiplierRune != _maxEmissionStrength)
        {
            print("Running");
            _multiplierRune = Mathf.MoveTowards(_multiplierRune, _maxEmissionStrength, Time.deltaTime * _lerpSpeed);
            _runeMat.SetColor("_EmissiveColor", _emissionColour * _multiplierRune);
            yield return new WaitForEndOfFrame();
        }

        while (plateLight.intensity > 0)
        {
            plateLight.intensity = Mathf.Lerp(plateLight.intensity, 0, _lerpSpeed * Time.deltaTime * 100f);
            yield return new WaitForEndOfFrame();
        }

        firePits[0].SetBool("RuneUsed", false);
        firePits[1].SetBool("RuneUsed", false);
        _backgroundSound.Stop();

        _fadeLight.enabled = true;

        proceduralArmPlacement.pause = false;

        interactWithObject.Cooldown = false;
        movement.StopRemoteControlledMovement();

        yield return null;
        LevelSelectEnabler.EnableButton();
    }
}