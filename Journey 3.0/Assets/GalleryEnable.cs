using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalleryEnable : MonoBehaviour
{
    private void OnEnable()
    {
        GallerySaveSystem.SetGallery();
    }

    private void OnDisable()
    {
        GallerySaveSystem.SaveGallery();
    }
}
