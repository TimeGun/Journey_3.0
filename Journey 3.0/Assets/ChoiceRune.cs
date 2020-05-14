using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChoiceRune : MonoBehaviour, IInteractible, IRune
{
    private Transform _chestHeight;

    [SerializeField] private float armSeperationFloat = 0.314f;

    [SerializeField] private float _wallSeperationBuffer;
    [SerializeField] private float _wallDistanceCheck = 1.5f;

    [SerializeField] private LayerMask _wallMask;
    
    [SerializeField] private GameObject _palceToStand;
    [SerializeField] private GameObject _objectToFollow;

    [SerializeField] private string _timelineObject = "";
    
    private PlayableDirector _timelineToPlay;

    [SerializeField] private AudioSource _source;

    [SerializeField] [Range(0, 2f)] private float delayTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator ChooseEnding(Transform player)
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
                
                LeftArmIK.Instance.SetProceduralTargetAndHint(
                    leftRaycastHit.point + (leftRaycastHit.normal * _wallSeperationBuffer), leftRaycastHit.normal, 1f);
            }
        }

        if (_timelineObject != "")
        {
            _timelineToPlay = GameObject.Find(_timelineObject).GetComponent<PlayableDirector>();
        }


        if (_timelineToPlay != null)
        {
            _timelineToPlay.Play();
            _source.PlayDelayed(delayTime);
            StartCoroutine(FadeToBlack());
        }

        yield return new WaitForSeconds(3f);

        proceduralArmPlacement.pause = false;

        interactWithObject.Cooldown = false;
        movement.StopRemoteControlledMovement();
        LevelSelectEnabler.EnableButton();
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public bool isActive()
    {
        return true;
    }

    public void StartInteraction(Transform parent)
    {
        print("Lego");
        StartCoroutine(ChooseEnding(parent));
    }

    public void StopInteraction()
    {
        throw new System.NotImplementedException();
    }
    
    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(((float)_timelineToPlay.duration) - 3f);
        Debug.Log("FADE OUT");
        global::FadeToBlack.instance.SetBlack(true);
        yield return new WaitForSeconds(6f);
        
        ProgressionData _resetData = new ProgressionData(0, false);
        
        SaveSystem.SaveProgress(_resetData);
        SceneManager.LoadScene("Manager Scene");
    }
}
