using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAdjustmentDouble : MonoBehaviour
{
    [SerializeField] private BridgeSideTrigger _bridgeSideTrigger;

    [SerializeField] private bool _plankPlacedDown = false;
    
    
    public bool PlankPlacedDown
    {
        get => _plankPlacedDown;
        set => _plankPlacedDown = value;
    }

    public GameObject PlayerObject
    {
        get => _playerObject;
        set => _playerObject = value;
    }

    private GameObject _playerObject;

    private Collider col;
    
    void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
        col = GetComponent<Collider>();
    }

    void Update()
    {
        //if the player is in the placement area and a plank hasn't been placed yet
        if (_bridgeSideTrigger.PlayerInThisTrigger && !_bridgeSideTrigger.PlankPlaceDownBool)
        {
            AdjustPosition();
            col.isTrigger = false;
        }else if (!_bridgeSideTrigger.PlankPlaceDownBool)
        {
            col.isTrigger = false;
        }
        else
        {
            col.isTrigger = true;
        }
    }
    

    private void AdjustPosition()
    {
        Vector3 playerPos = _playerObject.transform.position;
        playerPos.y = 0;
        Vector3 playerPos2 = _playerObject.transform.position + (Vector3.Normalize(transform.right));
        playerPos2.y = 0;
        
        Vector3 thisPos = transform.position;
        thisPos.y = 0;
        Vector3 thisPos2 = transform.position + (Vector3.Normalize(transform.forward));
        thisPos2.y = 0;
        
        Vector2 pointOfIntersection = lineLineIntersection(new Vector2(thisPos.x, thisPos.z), new Vector2(thisPos2.x, thisPos2.z), new Vector2(playerPos.x, playerPos.z), new Vector2(playerPos2.x, playerPos2.z));

        transform.position = new Vector3(pointOfIntersection.x, transform.position.y, pointOfIntersection.y);
        
    }
    
    public static Vector2 lineLineIntersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D) 
    { 
        // Line AB represented as a1x + b1y = c1  
        float a1 = B.y - A.y; 
        float b1 = A.x - B.x; 
        float c1 = a1 * (A.x) + b1 * (A.y); 
  
        // Line CD represented as a2x + b2y = c2  
        float a2 = D.y - C.y; 
        float b2 = C.x - D.x; 
        float c2 = a2 * (C.x) + b2 * (C.y); 
  
        float determinant = a1 * b2 - a2 * b1; 
  
        if (determinant == 0) 
        { 
            // The lines are parallel. This is simplified  
            // by returning a pair of FLT_MAX
            return new Vector2(float.MaxValue, float.MaxValue); 
        } 
        else
        { 
            float x = (b2 * c1 - b1 * c2) / determinant; 
            float y = (a1 * c2 - a2 * c1) / determinant; 
            return new Vector2(x, y);
        } 
    }
}
