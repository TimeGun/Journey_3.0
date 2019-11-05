using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveGrass : MonoBehaviour
{
    public Material[] materials;
    public Transform player;
    private Vector3 position;
    private float radius;
    
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
            position.y += player.localScale.y/2f;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetVector("_position", position);
                radius = materials[i].GetFloat("_range");
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + (player.localScale.y/2f), transform.position.z), radius);
    }
}