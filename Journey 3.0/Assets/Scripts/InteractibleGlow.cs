using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleGlow : MonoBehaviour
{
    private ObjectDetection _objectDetection;

    [SerializeField] float outlineStrength = 0.65f;

    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float updatesPerSecond = 10f;
    
    public List <Material> materials = new List<Material>();
    
    public List<GameObject> lerpObjects = new List<GameObject>();
    
    void OnEnable()
    {
        _objectDetection = GetComponent<ObjectDetection>();
        StartCoroutine(UpdateMaterialList());
        
        materials.Clear();
        lerpObjects.Clear();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _objectDetection = null;
        materials.Clear();
        lerpObjects.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (materials.Count > 0)
        {
            GlowOutlineGlow(materials.ToArray());
        }
        
        FadeOutMaterials();
    }

    void GlowOutlineGlow(Material[] outlineMaterials)
    {
        for (int i = 0; i < outlineMaterials.Length; i++)
        {
            if (outlineMaterials[i].GetFloat("_outlineStrength") != null)
            {
                float strength = outlineMaterials[i].GetFloat("_outlineStrength");
            
                outlineMaterials[i].SetFloat("_outlineStrength", Mathf.Lerp(strength, outlineStrength, Time.deltaTime * lerpSpeed));
            }
        }
        
    }

    void FadeOutMaterials()
    {
        for (int i = 0; i < lerpObjects.Count; i++)
        {
            print("here");
            Renderer rend = lerpObjects[i].GetComponentInChildren<Renderer>();
            
            float strength = rend.material.GetFloat("_outlineStrength");

            if (strength < 0.05f)
            {
                rend.material.SetFloat("_outlineStrength", 0);
                lerpObjects.Remove(lerpObjects[i]);
            }

            rend.material.SetFloat("_outlineStrength", Mathf.Lerp(strength, 0, Time.deltaTime * lerpSpeed));
        }
    }

    IEnumerator UpdateMaterialList()
    {
        List<GameObject> objectsToChangeMaterial = new List<GameObject>();
        
        while (true)
        {
            for (int i = 0; i < objectsToChangeMaterial.Count; i++)
            {
                if (!_objectDetection.Items.Contains(objectsToChangeMaterial[i]) || _objectDetection.Items[i].GetComponent<IInteractible>().isActive())
                {
                    lerpObjects.Add(objectsToChangeMaterial[i]);
                    print("added");
                    objectsToChangeMaterial.Remove(objectsToChangeMaterial[i]);
                }
            }
            
            for (int i = 0; i < _objectDetection.Items.Count; i++)
            {
                if (_objectDetection.Items[i] != null)
                {
                    if (_objectDetection.Items[i].GetComponent<IRune>() == null && !objectsToChangeMaterial.Contains(_objectDetection.Items[i]) && !_objectDetection.Items[i].GetComponent<IInteractible>().isActive())
                    {
                        objectsToChangeMaterial.Add(_objectDetection.Items[i]);

                        if (lerpObjects.Contains(_objectDetection.Items[i]))
                        {
                            lerpObjects.Remove(_objectDetection.Items[i]);
                        }
                    }
                }
            }
            
            materials.Clear();
            
            for (int i = 0; i < objectsToChangeMaterial.Count; i++)
            {
                if (objectsToChangeMaterial[i].GetComponentInChildren<Renderer>() != null && objectsToChangeMaterial[i].GetComponentInChildren<Renderer>().material != null)
                {
                    materials.Add(objectsToChangeMaterial[i].GetComponentInChildren<Renderer>().material);
                }
            }
            
            yield return new WaitForSeconds(1f/updatesPerSecond);
        }
    }
}
