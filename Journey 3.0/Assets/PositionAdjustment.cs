using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAdjustment : MonoBehaviour
{
    [SerializeField] private PlayerInPlacementArea _playerInPlacementArea;

    [SerializeField] private bool plankPlacedDown = false;

    private GameObject _playerObject;
    
    void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        AdjustPosition();
        //if the player is in the placement area and a plank hasnt been placed yet
        if (_playerInPlacementArea.PlayerInTrigger && !plankPlacedDown)
        {
            AdjustPosition();
        }
    }

    private void AdjustPosition()
    {
        Vector3 playerPos = _playerObject.transform.position;
        playerPos.y = 0;
        Vector3 playerPos2 = _playerObject.transform.position + Vector3.Normalize(transform.right);
        playerPos2.y = 0;

        Debug.DrawLine(playerPos, playerPos2, Color.red);

        Vector3 thisPos = transform.position;
        thisPos.y = 0;
        Vector3 thisPos2 = transform.position + Vector3.Normalize(transform.forward);
        thisPos2.y = 0;
        
        Debug.DrawLine(thisPos, thisPos2, Color.red);

        Vector2 pointOfIntersection = LineIntersection(thisPos, thisPos2, playerPos, playerPos2);
        
        transform.position = new Vector3(pointOfIntersection.x, transform.position.y, pointOfIntersection.y);
        
        print(pointOfIntersection);
    }
    
    
    public static Vector2 LineIntersection( Vector2 p1,Vector2 p2, Vector2 p3, Vector2 p4)
    {
 
        float Ax,Bx,Cx,Ay,By,Cy,d,e,f,num/*,offset*/;
 
        float x1lo,x1hi,y1lo,y1hi;
 
   
 
        Ax = p2.x-p1.x;
 
        Bx = p3.x-p4.x;
 
   
 
        // X bound box test/
 
        if(Ax<0) {
 
            x1lo=p2.x; x1hi=p1.x;
 
        } else {
 
            x1hi=p2.x; x1lo=p1.x;
 
        }
 
   
 
        if(Bx>0) {
 
            if(x1hi < p4.x || p3.x < x1lo) return new Vector2();
 
        } else {
 
            if(x1hi < p3.x || p4.x < x1lo) return new Vector2();
 
        }
 
   
 
        Ay = p2.y-p1.y;
 
        By = p3.y-p4.y;
 
   
 
        // Y bound box test//
 
        if(Ay<0) {                  
 
            y1lo=p2.y; y1hi=p1.y;
 
        } else {
 
            y1hi=p2.y; y1lo=p1.y;
 
        }
 
   
 
        if(By>0) {
 
            if(y1hi < p4.y || p3.y < y1lo) return new Vector2();
 
        } else {
 
            if(y1hi < p3.y || p4.y < y1lo) return new Vector2();
 
        }
 
   
 
        Cx = p1.x-p3.x;
 
        Cy = p1.y-p3.y;
 
        d = By*Cx - Bx*Cy;  // alpha numerator//
 
        f = Ay*Bx - Ax*By;  // both denominator//
 
   
 
        // alpha tests//
 
        if(f>0) {
 
            if(d<0 || d>f) return new Vector2();
 
        } else {
 
            if(d>0 || d<f) return new Vector2();
 
        }
 
   
 
        e = Ax*Cy - Ay*Cx;  // beta numerator//
 
   
 
        // beta tests //
 
        if(f>0) {                          
 
            if(e<0 || e>f) return new Vector2();
 
        } else {
 
            if(e>0 || e<f) return new Vector2();
 
        }
 
   
 
        // check if they are parallel
 
        if(f==0) return new Vector2();
       
        // compute intersection coordinates //
 
        num = d*Ax; // numerator //
 
//    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;   // round direction //
 
//    intersection.x = p1.x + (num+offset) / f;
        Vector2 intersection = new Vector2();
        
        intersection.x = p1.x + num / f;
 
   
 
        num = d*Ay;
 
//    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;
 
//    intersection.y = p1.y + (num+offset) / f;
        intersection.y = p1.y + num / f;
 
   
 
        return intersection;
 
    }
}
