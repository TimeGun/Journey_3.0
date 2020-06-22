using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlonks : MonoBehaviour
{
    [SerializeField] private GameObject[] fallingPlonks;
    [SerializeField] private GameObject[] interactiblePlonks;

    void SetPlonkPosition(GameObject plonk, int index)
    {
        plonk.transform.position = interactiblePlonks[index].transform.position;
    }

    void SetPlonkRotation(GameObject plonk, int index)
    {
        plonk.transform.rotation = interactiblePlonks[index].transform.rotation;
    }

    void GivePlonkSlightRotation(GameObject plonk)
    {
        plonk.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-0.5f, 2.5f), 0f, 0f), ForceMode.Impulse);
    }

    public void DropPlonks()
    {
        for (int i = 0; i < fallingPlonks.Length; i++)
        {
            SetPlonkPosition(fallingPlonks[i], i);
            SetPlonkRotation(fallingPlonks[i], i);
            
        }
        for (int i = 0; i < interactiblePlonks.Length; i++)
        {
            fallingPlonks[i].SetActive(true);
            GivePlonkSlightRotation(fallingPlonks[i]);
            interactiblePlonks[i].SetActive(false);
        }
        
        
        
    }
}
