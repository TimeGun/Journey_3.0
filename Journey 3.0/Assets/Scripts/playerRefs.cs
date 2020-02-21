using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRefs : MonoBehaviour
{
    private Vector3 _playerPos;
    
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerPos = transform.position;
        PlayerManager.PlayerPosRef = _playerPos;
//        Debug.Log(PlayerManager.PlayerPosRef);

    }
}
