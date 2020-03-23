using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AssignTargetGroup : MonoBehaviour
{
    public CinemachineTargetGroup TG;
    

    public bool assignPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        TG = GetComponent<CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (assignPlayer)
        {
            TG.AddMember(API.GlobalReferences.PlayerRef.transform, 1, 0);
            TG.RemoveMember(null);
            
        }
    }
}
