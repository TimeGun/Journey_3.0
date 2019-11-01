using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGrass : MonoBehaviour
{
    public Material[] materials;
    public Transform player;
    private Vector3 position;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateMaterials());
    }

    IEnumerator UpdateMaterials()
    {
        while (true)
        {
            position = player.transform.position;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetVector("_position", position);
            }

            yield return null;
        }
    }
}
