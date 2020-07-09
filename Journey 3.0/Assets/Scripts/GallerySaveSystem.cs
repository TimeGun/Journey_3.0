﻿using System;
using System.Collections;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;

public class GallerySaveSystem : MonoBehaviour
{
    [SerializeField] private List<Painting> paintings = new List<Painting>();

    [SerializeField] public GameObject[] uiPaintings;
    
    public static GallerySaveSystem instance;

    [SerializeField] private AchievementSO galleryAchievement;
    
    void Awake()
    {
        instance = this;
        LoadGallery();
    }


    public static void SaveGallery()
    {
        print("Saved Paintings");

        bool allUnlocked = true;
        
        for (int i = 0; i < instance.paintings.Count; i++)
        {
            if (!instance.paintings[i]._found)
            {
                allUnlocked = false;
            }

            PlayerPrefs.SetInt(instance.paintings[i]._name, (instance.paintings[i]._found ? 1 : 0));
        }

        CheckGalleryAchievement(allUnlocked);
    }

    private static void CheckGalleryAchievement(bool allFound)
    {
        if (allFound)
        {
            AchievementManager.instance.UnlockSteamAchievement(instance.galleryAchievement);
        }
    }

    public void LoadGallery()
    {
        //Load the paintings from player prefs
        for (int i = 0; i < instance.paintings.Count; i++)
        {
            print("Loaded Paintings");
            instance.paintings[i]._found = (PlayerPrefs.GetInt(instance.paintings[i]._name, 0) != 0);
        }
    }

    public static void SetGallery()
    {
        for (int i = 0; i < instance.uiPaintings.Length; i++)
        {
            instance.uiPaintings[i].SetActive(instance.paintings[i]._found);
        }
    }

    private void OnDestroy()
    {
        SaveGallery();
    }


    public static void FoundPainting(int paintingIndex)
    {
        if (!instance.paintings[paintingIndex]._found)
        {
            instance.paintings[paintingIndex]._found = true;
            
            int numberOfPaintingsFound;

            if (SteamManager.Initialized)
            {
                SteamUserStats.GetStat("paintings_found", out numberOfPaintingsFound);
                print("The number of paintings found is: " + numberOfPaintingsFound + 1);
                SteamUserStats.SetStat("paintings_found", numberOfPaintingsFound + 1);
            }
        }

        SaveGallery();
    }
}

[Serializable]
public class Painting
{
    public string _name;
    public bool _found;

    public Painting(string name, bool found)
    {
        _name = name;
        _found = found;
    }
}
