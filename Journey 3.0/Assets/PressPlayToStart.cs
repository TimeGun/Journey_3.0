using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PressPlayToStart : MonoBehaviour
{
    [SerializeField] private InputMaster _controls;

    [SerializeField] private InputSetUp _inputSetUp;
    
    public UnityEvent _event;

    [SerializeField] private GameObject _menu;
    [SerializeField] private CinemachineVirtualCamera _cam;
    
    
    void Start()
    {
        _controls = _inputSetUp.Controls;
        if (SceneManagerScript.instance.LoadSpecificBundle == true)
        {
            _menu.SetActive(false);
            _cam.Priority = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_controls.PlayerFreeMovement.StartButton.triggered)
        {
            _event.Invoke();
            Destroy(gameObject);
        }
    }
}
