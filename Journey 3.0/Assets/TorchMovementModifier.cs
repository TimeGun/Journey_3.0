using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TorchMovementModifier : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private VisualEffect _torch;

    private bool _moving;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.velocity.magnitude > 0.25f)
        {
            if (!_moving)
            {
                _moving = true;
                _torch.SetBool("IsMoving", _moving);
            }

            
        }
        else
        {
            if (_moving)
            {
                _moving = false;
                _torch.SetBool("IsMoving", _moving);
            }
        }
    }
}
