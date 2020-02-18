using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishObject : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Mesh _newMesh;
    [SerializeField] private Material _newMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Squish()
    {
        _meshFilter.mesh = _newMesh;
        _meshRenderer.material = _newMaterial;
    }
}
