using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PathOffsetChangeBasedOnPlayer : MonoBehaviour
{

    private Vector3 playerForward;
    public GameObject targetCamera;
    public GameObject player;
    public float startOffset;
    public float endOffset;
    private float adjustedOffset;
    public int checksPerSecond;
    private CinemachineVirtualCamera cameraBase;
    private float angleBetweenPlayerAndCamera;
    public bool testScene;
    private CinemachineTrackedDolly TD;
    
    public float rateOfChange;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraBase = targetCamera.GetComponent<CinemachineVirtualCamera>();
        TD = cameraBase.GetCinemachineComponent<CinemachineTrackedDolly>();
        StartCoroutine(CheckAngle());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (testScene != true)
        {
            player = API.GlobalReferences.PlayerRef;            
        }
        
    }



    IEnumerator CheckAngle()
    {
        
        if (targetCamera.activeSelf)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            while (true)
            {
                angleBetweenPlayerAndCamera = Vector3.Angle(player.transform.forward,
                    targetCamera.transform.forward);
                if (angleBetweenPlayerAndCamera >= 90f)
                {
                    adjustedOffset = endOffset;
                }
                else
                {
                    adjustedOffset = startOffset;
                }
                Debug.Log(adjustedOffset);
                float tempOffset = Mathf.Lerp(TD.m_AutoDolly.m_PositionOffset, adjustedOffset,
                    (1f / rateOfChange) * Time.deltaTime);
                TD.m_AutoDolly.m_PositionOffset = tempOffset;
                
                yield return new WaitForSeconds(1f / checksPerSecond);

            }
        }
    }
}
