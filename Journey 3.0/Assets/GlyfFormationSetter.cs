using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyfFormationSetter : MonoBehaviour
{

    public bool UseFormation;

    public Animator anim;

    public static GlyfFormationSetter instance;
    void Awake()
    {
        instance = this;
    }

    public static void CheckAnimation()
    {
        if (instance.UseFormation)
        {
            instance.anim.Play("Glyf-Formation");
        }
    }
}
